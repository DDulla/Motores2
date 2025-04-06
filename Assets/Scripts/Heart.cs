using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private int healAmount = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Heal(healAmount); 
            }
            Destroy(gameObject); 
        }
    }
}