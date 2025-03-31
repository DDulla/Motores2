using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void ExitGame()
    {
        Application.Quit(); 
    }
}