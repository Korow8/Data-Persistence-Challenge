using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainHandler : MonoBehaviour
{
    public void OnClickBack() {
        SceneManager.LoadScene(0);
    }
}
