using UnityEngine;

public class SafeZoneTrigger : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Уведомляем GameManager, что игрок покинул безопасную зону
            gameManager.OnPlayerExitSafeZone();
        }
    }
}
