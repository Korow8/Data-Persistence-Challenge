using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Datas
{
    private static string _name = "-1";

    public static string Name { 
        get => _name;
        set
        {
            _name = value != "" ? value : "Player";
        } 
    }
}

public class UIMenuHandler : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField nameField;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject highScorePanel;

    private void Awake()
    {
        bestScoreText.text = "Best Score : " + Highscore.Instance.GetBestScore();
    }

    public void OnClickStart()
    {
        Datas.Name = nameField.text;
        SceneManager.LoadScene(1);
    }

    public void OnClickHighscore()
    {
        /*if(Highscore.Instance._highScoreDatas != null)
        {
            foreach (KeyValuePair<string, int> data in Highscore.Instance._highScoreDatas)
            {
                Debug.Log(data.Key + " : " + data.Value);
            }
        }*/
        menuPanel.SetActive(false);
        highScorePanel.SetActive(true); 
    }
    
    public void OnClickBack()
    {
        highScorePanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
}
