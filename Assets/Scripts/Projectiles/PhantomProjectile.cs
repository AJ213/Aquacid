using System.Collections;
using UnityEngine;

public class PhantomProjectile : Projectile
{
    [Header("Adjustable")]
    [SerializeField] float turnAroundTime = 10;
    [SerializeField] float invisTime = 1;
    [Header("No touch")]
    [SerializeField] Animator animator = null;
    public override void ConstructStatistics(Vector2 direction, GameObject firingSource)
    {
        this.direction = direction;
        this.firingSource = firingSource;

        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(this.direction * speed, ForceMode2D.Impulse);
        this.transform.localScale = this.transform.localScale * size;
        

        StartCoroutine(Loop());
    }
    IEnumerator Loop()
    {
        yield return new WaitForSeconds((turnAroundTime/2));
        animator.SetTrigger("invis");
        yield return new WaitForSeconds(invisTime);
        animator.SetTrigger("reappear");
        yield return new WaitForSeconds((turnAroundTime / 2 - invisTime));
        rb.AddForce(-this.direction * speed * 2, ForceMode2D.Impulse);

        yield return new WaitForSeconds((turnAroundTime / 2));
        animator.SetTrigger("invis");
        yield return new WaitForSeconds(invisTime);
        animator.SetTrigger("reappear");
        yield return new WaitForSeconds((turnAroundTime / 2 - invisTime));
        Destroy(this.gameObject);
    }
}
