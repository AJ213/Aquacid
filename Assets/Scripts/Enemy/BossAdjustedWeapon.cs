using UnityEngine;

public class BossAdjustedWeapon : EnemyWeapon
{
    public bool isReadyToFire = false;
    new void Update()
    {
        if (isReloading)
        {
            isReadyToFire = false;
            return;
        }

        if (currentAmmo <= 0)
        {
            isReadyToFire = false;
            StartCoroutine(Reload());
            return;
        }

        if (player == null)
        {
            isReadyToFire = false;
            return;
        }

        if (Vector2.Distance(this.transform.position, player.transform.position) > detectRange)
        {
            isReadyToFire = false;
            return;
        }

        isReadyToFire = true;
    }
}
