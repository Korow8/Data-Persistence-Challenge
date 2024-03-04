using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class Highscore
{
    // Les objets de type Dictionnaire ne sont pas sérialisable, donc on utilise une classe spéciale permettant de contourner cette limitation
    // Todo : Mettre les deux classe en privé et trouver une solution pour les classes exterieur de recuperer les données
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> keys = new List<TKey>();

        [SerializeField]
        private List<TValue> values = new List<TValue>();

        // save the dictionary to lists
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        // load dictionary from lists
        public void OnAfterDeserialize()
        {
            this.Clear();

            if (keys.Count != values.Count)
                throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

            for (int i = 0; i < keys.Count; i++)
                this.Add(keys[i], values[i]);
        }
    }

    [Serializable] public class HighScoreDatas : SerializableDictionary<string, int> { };
    public HighScoreDatas _highScoreDatas;

    // Singleton object
    private static Highscore _instance;
    public static Highscore Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Highscore();
            }
            
            return _instance;
        }
        private set => _instance = value;
    }

    private string path = Application.persistentDataPath + "/highscore.json";

    public void SaveDatas()
    {
        if (_highScoreDatas != null)
        {
            string json = JsonUtility.ToJson(_highScoreDatas);
            File.WriteAllText(path, json);
        }
    }

    public void LoadDatas()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreDatas data = JsonUtility.FromJson<HighScoreDatas>(json);
            _highScoreDatas = data;
        }

        if (_highScoreDatas == null)
        {
            _highScoreDatas = new HighScoreDatas();
        }
    }

    public void AddHighScoreData(string name, int value)
    {
        // Load the data from file
        if (_highScoreDatas == null)
        {
            LoadDatas();
        }

        // Check if duplicate
        if (_highScoreDatas.ContainsKey(name))
        {
            // If duplicate, update the value if the current value is greater than the one savec
            if(_highScoreDatas[name] < value)
            {
                _highScoreDatas[name] = value;
            }
        }
        else
        {
            _highScoreDatas.Add(name, value);
        }

        // Rewrite the highscore file
        SaveDatas();
    }

    public string GetBestScore()
    {
        LoadDatas();

        // Sort the dictionnary by descending, then get the first key who is the max value
        string name = _highScoreDatas.OrderByDescending(x => x.Value).First().Key;

        return name + " : " + _highScoreDatas[name];
    }

    public string GetAllScore()
    {
        string scores = "-1";
        if(_highScoreDatas != null)
        {
            scores = "";
            foreach (KeyValuePair<string, int> data in Highscore.Instance._highScoreDatas.OrderByDescending(key => key.Value))
            {
                scores += data.Key + " : " + data.Value + "<br>";
            }
        }
        else
        {
            Debug.Log("Warning : Attempting to GetAllScore from null dictionnary");
        }

        return scores;
    }
}
