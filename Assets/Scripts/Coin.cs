using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int points = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AddScore(points); 
            }
            Destroy(gameObject); 
        }
    }
}