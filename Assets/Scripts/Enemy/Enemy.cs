using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Hide me")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerController player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            
            this.GetComponent<Health>().TakeDamage(player.swordDamage);
        }
    }

    private void OnDestroy()
    {
        PlayerController.kills += (int)this.gameObject.GetComponent<Health>().MaxHealth;
    }
}
