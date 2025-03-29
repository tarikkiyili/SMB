using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // public GameObject gameOverPanel;
    // public GameObject levelCompletedPanel;
    public GameObject pausePanel;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
                ResumeGame();
            else
                PauseGame();
        }
    }
    // Aşağıdaki fonksiyonların her birisi pause menüsü ve içerisindeki seçenekleri açıp kapatma. 
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // public void GameOver()
    // {
    //     StartCoroutine(FadeIn(gameOverPanel, 1f));
    // }

    // public void LevelCompleted()
    // {
    //     StartCoroutine(FadeIn(levelCompletedPanel, 1f));
    // }

    // Game over veya Level completed olması halinde animasyonlu bir şekilde menünün gelme efekti.
    private IEnumerator FadeIn(GameObject panel, float delay)
    {
        yield return new WaitForSeconds(delay);

        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        panel.SetActive(true);
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
    }
}
