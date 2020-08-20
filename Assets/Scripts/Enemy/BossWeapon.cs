using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] float[] weaponTimers = default;
    [SerializeField] int currentWeapon = default;
    [SerializeField] BossAdjustedWeapon[] enemyWeapons = default;
    [SerializeField] bool waiting = true;
    GameObject player = null;
    public void Start()
    {
        currentWeapon = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SwitchTimer(weaponTimers[currentWeapon]));
    }

    public void Update()
    {
        if(!waiting)
        {
            StartCoroutine(SwitchTimer(weaponTimers[currentWeapon]));
            currentWeapon++;
            
            if (currentWeapon >= weaponTimers.Length)
            {
                currentWeapon = 0;
            }
        }
        if(enemyWeapons[currentWeapon].isReadyToFire)
        {
            enemyWeapons[currentWeapon].FireProjectile(player.transform.position);
        }
    }

    IEnumerator SwitchTimer(float time)
    {
        waiting = true;
        yield return new WaitForSeconds(time);
        waiting = false;
    }
}
