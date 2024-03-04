using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// General datas stored between scenes
public static class Datas
{
    private static string _name = "-1";

    public static string Name {
        get => _name;
        set {
            _name = value != "" ? value : "Player";
        }
    }
}

public class UIMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private TMPro.TMP_InputField nameField;

    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private TMPro.TextMeshProUGUI highScoreText;

    private void Awake() {
        bestScoreText.text = "Best Score : " + Highscore.Instance.GetBestScore();
    }

    public void OnClickStart() {
        Datas.Name = nameField.text;
        SceneManager.LoadScene(1);
    }

    public void OnClickHighscore() {
        highScoreText.text = Highscore.Instance.GetAllScore();
        menuPanel.SetActive(false);
        highScorePanel.SetActive(true);
    }

    public void OnClickBack() {
        highScorePanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void OnClickExit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
}
