using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagProjectile : Projectile
{
    [Header("Adjustable")]
    [SerializeField] float rightTime = 3;
    [SerializeField] float rightZigZagAngle;
    [SerializeField] float leftTime = 3;
    [SerializeField] float leftZigZagAngle;
    [SerializeField] float readjustTimes = 100;

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
        /*Vector2 oldPlayerDirection = CalculateDirection(this.transform.position, direction);
        oldPlayerDirection.Normalize();*/
        rightZigZagAngle = (-rightZigZagAngle + CalculateAngle(direction)) * Mathf.Deg2Rad;
        leftZigZagAngle = (leftZigZagAngle + CalculateAngle(direction)) * Mathf.Deg2Rad;
        for (int i = 1; i <= readjustTimes; i++)
        {
            

            direction = CalculateDirection(this.transform.position, new Vector2(this.transform.position.x + Mathf.Cos(rightZigZagAngle), this.transform.position.y + Mathf.Sin(rightZigZagAngle)));
            rb.velocity = Vector3.zero;
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(leftTime);

            direction = CalculateDirection(this.transform.position, new Vector2(this.transform.position.x + Mathf.Cos(leftZigZagAngle), this.transform.position.y + Mathf.Sin(leftZigZagAngle)));
            rb.velocity = Vector3.zero;
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(rightTime);
        }
    }
}
