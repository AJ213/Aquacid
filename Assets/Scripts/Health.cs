using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Adjustable")]
    [SerializeField] private float maxHealth = 1;
    [SerializeField] float invulnerabilityTime = 0.5f;

    [Header("For Testing")]
    [SerializeField] AudioSource hurtSound = null;
    [SerializeField] private float currentHealth = 1;
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    
    [SerializeField] bool isinvulnerable = false;
    
    public UnityEvent invulnerable;

    void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        if (isinvulnerable)
        {
            return;
        }

        currentHealth -= damage;
        hurtSound.Play();
        PlayerController.stabCount++;
        StartCoroutine(Invulnerable());

        if(currentHealth <= 0)
        {
            Destroy(this.gameObject, 0.25f);
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }    
    }

    IEnumerator Invulnerable()
    {
        isinvulnerable = true;
        invulnerable.Invoke();
        yield return new WaitForSeconds(invulnerabilityTime);
        isinvulnerable = false;
    }
}
