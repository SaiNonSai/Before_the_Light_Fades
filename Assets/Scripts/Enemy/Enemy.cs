using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int xpReward = 100;

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
