using UnityEngine;

public class PlayerHealth : MonoBehaviour

{
    public int maxHealth = 100;
    public int currentHealth;

    AudioManagerLevel1 audioManager;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player recibió daño. Vida actual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        audioManager.PlaySFX(audioManager.damage);
    }

    void Die()
    {
        Debug.Log("PLAYER MUERTO");
        // Podés desactivar el objeto o hacer lo que quieras
        // gameObject.SetActive(false);
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerLevel1>();
    }
}
