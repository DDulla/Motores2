using UnityEngine;

public class ObstacleBlue : MonoBehaviour
{
    [SerializeField] private Color obstacleColor = Color.blue;
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damage = 1;

    public SpriteRenderer spriteRenderer;
    private bool movingToB = true;

    private void Awake()
    {
        spriteRenderer.color = obstacleColor;
    }

    private void Update()
    {
        Vector3 target = movingToB ? pointB : pointA;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            movingToB = !movingToB;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null && player.GetComponent<SpriteRenderer>().color != obstacleColor)
            {
                player.TakeDamage(damage);
            }
        }
    }
}