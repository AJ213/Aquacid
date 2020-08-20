using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] List<Animator> orbAnimators = null;
    [SerializeField] Health playerHealth = null;
    [SerializeField] int currentOrb = 3;
    void Update()
    {
        if(currentOrb - playerHealth.CurrentHealth >= 1)
        {
            for(int i = 1; i <= currentOrb - playerHealth.CurrentHealth; i++)
            {
                BreakHealthOrb();
            }
        }
    }

    void BreakHealthOrb() // Later needs to be changed to gain/lose orb, but for now losing is okay
    {
        if(currentOrb <= 0)
        {
            return;
        }

        orbAnimators[currentOrb-1].SetTrigger("Broken");
        currentOrb--;
    }
}
