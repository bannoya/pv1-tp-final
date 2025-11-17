using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI")]
    public Slider healthBar;   
    public float smoothSpeed = 5f;  

    private float targetHealth; 

    AudioManagerLevel1 audioManager;

    [Header("Regeneración")]
    public bool autoRegen = true;       
    public int regenAmount = 2;         
    public float regenInterval = 1f;    
    public float regenDelay = 3f;       

    private Coroutine regenCoroutine;
    private float lastDamageTime;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerLevel1>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        targetHealth = currentHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Update()
    {
        // Animación suave (Lerp)
        if (healthBar != null)
        {
            healthBar.value = Mathf.Lerp(healthBar.value, targetHealth, Time.deltaTime * smoothSpeed);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        targetHealth = currentHealth;

        if (currentHealth < 0) currentHealth = 0;

        lastDamageTime = Time.time; 

        audioManager.PlaySFX(audioManager.damage);

        if (regenCoroutine == null)
            regenCoroutine = StartCoroutine(LifeRegen());

        if (currentHealth <= 0)
            Die();
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        targetHealth = currentHealth;

        Debug.Log("Jugador curado. Vida actual: " + currentHealth);
    }
    private IEnumerator LifeRegen()
    {
        while (autoRegen)
        {
            
            if (Time.time - lastDamageTime >= regenDelay)
            {
                if (currentHealth < maxHealth)
                {
                    currentHealth += regenAmount;
                    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                    targetHealth = currentHealth;
                }
            }

            yield return new WaitForSeconds(regenInterval);
        }
    }
    void Die()
    {
        Debug.Log("PLAYER MUERTO");
    }
}
