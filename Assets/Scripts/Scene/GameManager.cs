using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject pausePanel;
    public GameObject timerText;
    public TextMeshProUGUI finishTimeText;

    public AudioSource levelCompleteAudio;
    public AudioSource gameOverAudio;
    public AudioSource inGameAudio;
    private static bool isMusicPlaying = false;

    private Player player;

    private void Awake()
    {
        Instance = this;

        GameObject mainMenuAudioObj = GameObject.Find("MainMenu");
        AudioSource menuAudio = mainMenuAudioObj.GetComponent<AudioSource>();
        if (menuAudio.isPlaying)
            menuAudio.Stop();
    }
    void Start()
    {
        Time.timeScale = 1f;
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.GetComponent<Player>();
        if (!isMusicPlaying)
        {
            inGameAudio.loop = true;
            inGameAudio.Play();
            isMusicPlaying = true;
        }
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

        if (IsPlayerDead() && !gameOverPanel.activeSelf)
            GameOver();
    }

    private bool IsPlayerDead()
    {
        return player != null && player.stateMachine.currentState == player.deathState;
    }
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        timerText.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        timerText.SetActive(true);
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
        gameOverAudio.Play();
        StartCoroutine(StopTimeAfterDelay(2.5f));
        timerText.SetActive(false);
        StartCoroutine(FadeIn(gameOverPanel, 0.25f));
    }

    public void LevelCompleted()
    {
        levelCompleteAudio.Play();
        LevelTimer timer = FindFirstObjectByType<LevelTimer>();
        timer.StopTimer();
        float time = timer?.GetElapsedTime() ?? 0f;
        finishTimeText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(time / 60f), Mathf.FloorToInt(time % 60f));
        timerText.SetActive(false);
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
            LoadGameScene();
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
