using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int xpReward = 100;
    public float moveSpeed = 2f;

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Enemy: No player found with tag 'Player'");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Move toward the player
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Die();
        }
    }

    void Die()
    {
        PlayerXP playerXP = GameObject.FindWithTag("Player")?.GetComponent<PlayerXP>();
        if (playerXP != null)
        {
            playerXP.AddXP(xpReward);
        }

        Destroy(gameObject);
    }
}
