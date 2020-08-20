using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    [Header("Adjustable")]
    [SerializeField] float startHomingDelay = 3;
    [SerializeField] float readjustAimTime = 3;
    [SerializeField] int readjustAimAmount = 100;
    [SerializeField] Color activeColor = default;
    [SerializeField] Color deactivedColor = default;
    
    public override void ConstructStatistics(Vector2 direction, GameObject firingSource)
    {
        this.direction = direction;
        this.firingSource = firingSource;

        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(this.direction * speed, ForceMode2D.Impulse);
        this.transform.localScale = this.transform.localScale * size;
        this.GetComponent<SpriteRenderer>().material.color = deactivedColor;
        

        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        yield return new WaitForSeconds(startHomingDelay);
        this.GetComponent<SpriteRenderer>().material.color = activeColor;
        for (int i = 1; i <= readjustAimAmount; i++)
        {
            yield return new WaitForSeconds(readjustAimTime);
            if(player != null)
            {
                direction = CalculateDirection(this.transform.position, player.transform.position);
                rb.velocity = Vector3.zero;
                rb.AddForce(direction * speed, ForceMode2D.Impulse);
            }
        }
        this.GetComponent<SpriteRenderer>().material.color = deactivedColor;
    }
}
