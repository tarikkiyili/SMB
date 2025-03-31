using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MakePersistent : MonoBehaviour
{
    public AudioSource mainMenuMusic;
    public AudioSource inGameMusic;
    public AudioSource levelCompleteAudio;
    public AudioSource gameOverAudio;
    public AudioSource clickSound;
    public AudioSource jumpSound;

    private static MakePersistent musicInstance;
    private bool isMusicManager => mainMenuMusic != null || inGameMusic != null;
    private void Awake()
    {
        if (!isMusicManager) return;

        if (musicInstance == null)
        {
            musicInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (musicInstance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (!isMusicManager) return;

        SceneManager.sceneLoaded += OnSceneLoaded;
        HandleMusic(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleMusic(scene.buildIndex);
    }

    private void HandleMusic(int sceneIndex)
    {
        if (sceneIndex == 0 || sceneIndex == 1)
        {
            if (!mainMenuMusic.isPlaying)
            {
                inGameMusic.Stop();
                mainMenuMusic.Play();
            }
        }
        else
        {
            if (!inGameMusic.isPlaying)
            {
                mainMenuMusic.Stop();
                inGameMusic.Play();
            }
        }
    }

    public void PlayJumpSound()
    {
        if (jumpSound != null)
            jumpSound.Play();
    }

}