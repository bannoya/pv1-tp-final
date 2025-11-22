using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHP = 500f;
    public float hp;
    public bool isAlive = true;

    void Start()
    {
        hp = maxHP;
    }

    public void TakeDamage(float dmg)
    {
        if (!isAlive) return;

        hp -= dmg;

        if (hp <= 0f)
        {
            hp = 0f;
            isAlive = false;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss muerto");
        // animación, sonido, desactivar IA, etc.
    }
}
