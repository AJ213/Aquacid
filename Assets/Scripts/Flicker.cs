using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Flicker : MonoBehaviour
{ 
    [Header("Adjustable")]
    [SerializeField] Color hurtColor = default;
    [SerializeField] float duration = 0.5f;

    [Header("No touch")]
    [SerializeField] Color normalColor = default;
    [SerializeField] Renderer spirteRenderer = null;

    private void Awake()
    {
        spirteRenderer = this.GetComponent<SpriteRenderer>();
        normalColor = spirteRenderer.material.color;
        this.GetComponent<Health>().invulnerable.AddListener(AnimateFlicker);
    }

    void AnimateFlicker()
    {
        StartCoroutine(Animate());
    }
    

    IEnumerator Animate()
    {
        spirteRenderer.material.color = hurtColor;

        yield return new WaitForSeconds(duration);

        spirteRenderer.material.color = normalColor;
    }

    void Update()
    {

    }
}
