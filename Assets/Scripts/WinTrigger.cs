using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            gameManager.Win();
        }
    }
}
