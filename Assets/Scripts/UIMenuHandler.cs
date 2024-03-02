using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

public static class Datas
{
    private static string _name = "-1";

    public static string name { 
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

    public void OnClickStart()
    {
        Datas.name = nameField.text;
        SceneManager.LoadScene(1);
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
