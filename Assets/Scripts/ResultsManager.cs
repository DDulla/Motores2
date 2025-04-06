using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsManager : MonoBehaviour
{
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text scoreText;

    private float elapsedTime;
    private int score;

    public void SetGameData(float time, int points)
    {
        elapsedTime = time;
        score = points;
    }

    public void ShowResults(string result)
    {
        resultText.text = result;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timeText.text = "Time: " + minutes + ":" + seconds;

        scoreText.text = "Score: " + score;

        resultsPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}