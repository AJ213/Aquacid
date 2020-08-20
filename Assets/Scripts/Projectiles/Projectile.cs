
//using System.Numerics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] protected float speed = 2;
    [SerializeField] protected float size = 1;
    [SerializeField] protected float damage = 1;
    [SerializeField] protected float despawnRange = 50;

    [Header("Statistics")]
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected GameObject firingSource = null;
    [SerializeField] protected Rigidbody2D rb;
    protected GameObject player = null;
    public void ConstructAttributes(float speed, float size, float damage)
    {
        this.speed = speed;
        this.size = size;
        this.damage = damage;
    }
    public virtual void ConstructStatistics(Vector2 direction, GameObject firingSource)
    {
        this.direction = direction;
        this.firingSource = firingSource;

        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(this.direction * speed, ForceMode2D.Impulse);
        this.transform.localScale = this.transform.localScale * size;
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!firingSource.CompareTag(collision.gameObject.tag))
        {
            if(collision.gameObject.GetComponent<Health>() != null)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            
            Destroy(this.gameObject);
        }
    }

    public static Vector2 CalculateDirection(Vector2 currentLocation, Vector2 target)
    {
        Vector2 targetDirection = new Vector2(target.x - currentLocation.x, target.y - currentLocation.y);
        targetDirection.Normalize();
        return targetDirection;
    }

    public static float CalculateAngle(Vector2 unitCircleLocation)
    {
        float angle = Mathf.Atan2(unitCircleLocation.y, unitCircleLocation.x) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }

    private void Update()
    {
        if(player == null)
        {
            return;
        }
        if(Vector2.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) > despawnRange)
        {
            Destroy(this.gameObject);
        }     
    }
}
