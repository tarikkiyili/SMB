using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [System.Serializable]
    public class LevelButton
    {
        public string levelName;
        public string unlockKey;
        public Button button;
        public GameObject lockIcon;
    }

    public LevelButton[] levelButtons;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateLevelButtons();
    }

public void UpdateLevelButtons()
{
    if (levelButtons == null || levelButtons.Length == 0)
        return;

    foreach (var lvl in levelButtons)
    {
        bool isUnlocked = lvl.unlockKey == "" || PlayerPrefs.GetInt(lvl.unlockKey, 0) == 1;

        lvl.lockIcon.SetActive(!isUnlocked);
        lvl.button.interactable = isUnlocked;
        lvl.button.image.color = isUnlocked ? Color.white : Color.gray;
    }
}

    public void LoadGameScene() => SceneManager.LoadScene("Levels");
    public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");
    public void LoadLevel(string levelName) => SceneManager.LoadScene(levelName);
    public void ExitGame()
    {
        Application.Quit();
        PlayerPrefs.DeleteAll(); // Level save'ini silmek için
        Debug.Log("Oyun Kapatıldı!");
    }
}
