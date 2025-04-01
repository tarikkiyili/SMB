using UnityEngine;
using UnityEngine.UI;

public class LevelLockController : MonoBehaviour
{
    public string levelKeyName;
    public GameObject lockIcon;
    public Button levelButton;

    void Start()
    {
        int isUnlocked = PlayerPrefs.GetInt(levelKeyName, 0);
        if (isUnlocked == 1)
        {
            lockIcon.SetActive(false);
            levelButton.interactable = true;
            levelButton.image.color = Color.white;
        }
        else
        {
            lockIcon.SetActive(true);
            levelButton.interactable = false;
        }
    }
}
