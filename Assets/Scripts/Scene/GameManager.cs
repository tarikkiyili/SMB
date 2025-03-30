using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject pausePanel;

    private Player player;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1f;

        // Oyuncuyu sahnede bul ve referans al
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.GetComponent<Player>();
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

        // Oyuncu öldüyse Game Over yap
        if (IsPlayerDead() && !gameOverPanel.activeSelf)
        {
            GameOver();
        }
    }

    private bool IsPlayerDead()
    {
        return player != null && player.stateMachine.currentState == player.deathState;
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

    public void GameOver()
    {
        StartCoroutine(StopTimeAfterDelay(2.5f));
        StartCoroutine(FadeIn(gameOverPanel, 0.25f));
    }

    public void LevelCompleted()
    {
        Time.timeScale = 0.1f;
        StartCoroutine(StopTimeAfterDelay(1f));
        StartCoroutine(FadeIn(levelCompletedPanel, 0.25f));
    }

    public void LoadNextLevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        // Eğer sahne Level0'dan (index 2) küçükse level sayılmaz, geç
        if (buildIndex < 2)
        {
            Debug.LogWarning("Şu an bir level sahnesinde değilsin!");
            return;
        }

        // Şu anki level index'i (level dizisindeki sıra, 0 tabanlı)
        int currentLevel = buildIndex - 2;

        // Toplam level sayısı
        int totalLevels = SceneManager.sceneCountInBuildSettings - 2;

        if (currentLevel + 1 < totalLevels)
        {
            // Sonraki levelin gerçek sahne index'ini hesapla ve yükle
            int nextBuildIndex = buildIndex + 1;
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextBuildIndex);
        }
        else
        {
            Debug.Log("Son leveldi! Level bölümüne dönülüyor...");
            LoadGameScene(); // istersen buraya yönlendirme yap
        }
    }

    // Game over veya Level completed olması halinde animasyonlu bir şekilde menünün gelme efekti.
    private IEnumerator FadeIn(GameObject panel, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        panel.SetActive(true);
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
    }
    private IEnumerator StopTimeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 0f;
    }
}
