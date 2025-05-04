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
    public TextMeshProUGUI starConditionText;

    [Header("Star UI Elements")]
    public GameObject FillStar1;
    public GameObject FillStar2;
    public GameObject FillStar3;
    public GameObject EmptyStar1;
    public GameObject EmptyStar2;
    public GameObject EmptyStar3;

    [Header("Star Thresholds")]
    public float threeStarThreshold = 5f;
    public float twoStarThreshold = 10f;


    private Player player;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1f;
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
        MakePersistent manager = FindAnyObjectByType<MakePersistent>();
        manager.gameOverAudio.Play();

        StartCoroutine(StopTimeAfterDelay(2.5f));
        timerText.SetActive(false);
        StartCoroutine(FadeIn(gameOverPanel, 0.25f));
    }

    public void LevelCompleted()
    {
        MakePersistent manager = FindAnyObjectByType<MakePersistent>();
        manager.levelCompleteAudio.Play();

        LevelTimer timer = FindFirstObjectByType<LevelTimer>();
        timer.StopTimer();
        float time = timer?.GetElapsedTime() ?? 0f;
        finishTimeText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(time / 60f), Mathf.FloorToInt(time % 60f));
        timerText.SetActive(false);
        Time.timeScale = 0.1f;
        int minutes3 = Mathf.FloorToInt(threeStarThreshold / 60f);
        int seconds3 = Mathf.FloorToInt(threeStarThreshold % 60f);

        int minutes2 = Mathf.FloorToInt(twoStarThreshold / 60f);
        int seconds2 = Mathf.FloorToInt(twoStarThreshold % 60f);

        starConditionText.text = $"{minutes3:00}:{seconds3:00}\n" +
                                $"{minutes2:00}:{seconds2:00}";

        StartCoroutine(StopTimeAfterDelay(1f));
        StartCoroutine(FadeIn(levelCompletedPanel, 0.25f));

        int starCount = 1;
        if (time <= threeStarThreshold)
            starCount = 3;
        else if (time <= twoStarThreshold)
            starCount = 2;

        UpdateStars(starCount);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Completed", 1);
        PlayerPrefs.Save();
    }

    public void LoadNextLevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (buildIndex < 2)
            return;

        int currentLevel = buildIndex - 2;
        int totalLevels = SceneManager.sceneCountInBuildSettings - 2;

        if (currentLevel + 1 < totalLevels)
        {
            int nextBuildIndex = buildIndex + 1;
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextBuildIndex);
        }
        else
            LoadGameScene();
    }
    private void UpdateStars(int starCount)
    {
        EmptyStar1.SetActive(true);
        EmptyStar2.SetActive(true);
        EmptyStar3.SetActive(true);

        FillStar1.SetActive(starCount >= 1);
        FillStar2.SetActive(starCount >= 2);
        FillStar3.SetActive(starCount >= 3);
    }

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
