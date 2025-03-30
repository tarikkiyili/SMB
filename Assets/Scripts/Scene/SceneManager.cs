using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public AudioSource mainMenuAudio;
    private static bool isMusicPlaying = false;

    void Awake()
    {
        Instance = this;

        GameObject inGameAudioObj = GameObject.Find("InGame");
        AudioSource gameAudio = inGameAudioObj.GetComponent<AudioSource>();
        if (gameAudio.isPlaying)
            gameAudio.Stop();
    }
    void Start()
    {
        if (!isMusicPlaying)
        {
            mainMenuAudio.loop = true;
            mainMenuAudio.Play();
            isMusicPlaying = true;
        }
    }

    // Ana menü içerisindeki tuşların fonksiyonları

    public void LoadGameScene() => SceneManager.LoadScene("Levels");
    public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");
    public void LoadLevel1() => SceneManager.LoadScene("Level1");
    public void LoadLevel2() => SceneManager.LoadScene("Level2");
    public void LoadLevel3() => SceneManager.LoadScene("Level3");

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Oyun Kapatıldı!");
    }
}
