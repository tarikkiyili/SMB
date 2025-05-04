using UnityEngine;

public class Exit : MonoBehaviour
{
    private GameObject player;
    private float detectionRange = .5f;
    private bool levelComplete = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (!levelComplete)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= detectionRange)
            {
                levelComplete = true;
                GameManager.Instance.LevelCompleted();
            }
        }
    }
}
