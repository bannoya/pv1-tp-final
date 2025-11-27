using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BossHealth : MonoBehaviour
{
    public float maxHP = 500f;
    public float hp;
    private float targetHealth;
    public float smoothSpeed = 5f;

    public bool isAlive = true;
    public Slider healthBar;
    private float lastDamageTime;

    void Start()
    {
        hp = maxHP;
        targetHealth = hp;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHP;
            healthBar.value = hp;
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


    public void TakeDamage(float dmg)
    {
        

        hp -= dmg;               
        if (hp < 0) hp = 0;

        targetHealth = hp;       
        lastDamageTime = Time.time;

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
        Debug.Log("Boss muerto");
        if (healthBar != null)
            healthBar.gameObject.SetActive(false);
        Destroy(gameObject,0.5f);
        SceneManager.LoadScene("Win");
    }
}
