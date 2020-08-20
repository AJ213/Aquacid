using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAdjustedWeapon : EnemyWeapon
{
    public bool isReadyToFire = false;
    // Update is called once per frame
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
