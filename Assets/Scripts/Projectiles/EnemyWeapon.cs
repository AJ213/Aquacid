using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Bullet Attributes")]
    [SerializeField] float bulletSpeed = 1;
    [SerializeField] float bulletSize = 1;
    [SerializeField] float bulletDamage = 1;
    [Header("Gun Attributes")]
    [SerializeField] [Tooltip("from 0-100 percent (in decimal)")] float bulletInaccuracyPercent = 1; // from 0 to 1
    [SerializeField]  float bulletsPerShot = 1; 
    [SerializeField] [Tooltip("from 0-380 degrees")] float bulletsArc = 10; // from 0 - 360 in degrees
    [SerializeField] protected float detectRange = 10;
    [SerializeField] [Tooltip("Time to refuel ammo")] float reloadTime = 3;
    [SerializeField] [Tooltip("Time in-between shots, best if its 0 if maxammo is 1")] float shootingTime = 0.1f;
    [SerializeField] int maxAmmo = 1;
    [Header("Bullet Type")]
    [SerializeField] bool randomizedType = false;
    [SerializeField] BulletType bulletType;
    [Header("For Testing")]
    [SerializeField] protected bool isReloading = false;
    [SerializeField] protected int currentAmmo = 1;

    enum BulletType
    {
        Projectile,
        PhantomProjectile,
        HomingProjectile,
        ZigZagProjectile
    }

    protected GameObject player = null;
    
    protected IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    protected IEnumerator ShotDelay()
    {
        isReloading = true;
        yield return new WaitForSeconds(shootingTime);
        isReloading = false;
    }

    public void FireProjectile(Vector2 target)
    {
        Vector2 targetDirection = Projectile.CalculateDirection(this.transform.position, target);
        float angle = Projectile.CalculateAngle(targetDirection);

        for (int i = 1; i <= bulletsPerShot; i++)
        {
            if(randomizedType)
            {
                bulletType = (BulletType)Random.Range(0, System.Enum.GetNames(typeof(BulletType)).Length);
            }
            GameObject bullet = (GameObject)Instantiate(Resources.Load(bulletType.ToString()), this.transform, true);


            bullet.transform.position = this.transform.position;
            bullet.GetComponent<Projectile>().ConstructAttributes(bulletSpeed, bulletSize, bulletDamage);

            float adjustedAngle = angle - (bulletsArc / 2) + i*(bulletsArc / (bulletsPerShot+1));
            float inaccuracyAngle = bulletInaccuracyPercent * 180 * ((Random.Range(0, 2) * 2) - 1 );
            float finalAngle = ((Random.Range(0f, 1f)*inaccuracyAngle) + adjustedAngle) * Mathf.Deg2Rad;

            targetDirection = new Vector2(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle));

            bullet.GetComponent<Projectile>().ConstructStatistics(targetDirection, this.gameObject);

        }
        StartCoroutine(ShotDelay());
        currentAmmo--;
    }

    void Awake()
    {
        currentAmmo = maxAmmo;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        if (isReloading)
        {
            return;
        }
            
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if(player == null)
        {
            return;
        }

        if (Vector2.Distance(this.transform.position, player.transform.position) > detectRange)
        {
            return;
        }

        FireProjectile(player.transform.position);
    }
}
