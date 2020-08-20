using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AheadPlayer : MonoBehaviour
{
    [SerializeField] float unitsAhead = 1;

    [SerializeField] Rigidbody2D playerRb = null;

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = playerRb.velocity.normalized * unitsAhead;
    }
}
