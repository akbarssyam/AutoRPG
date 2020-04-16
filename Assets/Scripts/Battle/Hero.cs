using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public int CurHp { get; set; }
    public int MaxHp { get; set; }
    public int Atk { get; set; }
    public float Accuracy { get; set; }
    public bool isDead { get; set; } = false;

    public event Action<float> OnHealthPctChanged = delegate { };

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        MaxHp = 100;
        Atk = 50;
        Accuracy = 0.75f;

        CurHp = MaxHp;
    }

    // ----------------------
    // --- Public Methods ---
    // ----------------------

    public void Attack(Hero enemy)
    {
        StartCoroutine(_Attack(enemy));
    }

    IEnumerator _Attack(Hero enemy)
    {
        // Play attacking animation
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(0.5f);

        // Hitcheck based on accuracy
        if (HitCheck(Accuracy))
        {
            // Apply damage to the enemy
            enemy.applyDamage(Atk);

            // Show floating damage number
            DamagePopup.Create(enemy.transform.position, Atk);
        }
        else // Miss
        {
            // Show miss text
            DamagePopup.Create(enemy.transform.position, "Miss");
        }

    }


    // -----------------------
    // --- Private Methods ---
    // -----------------------

    void applyDamage(int damage)
    {
        // Apply the damage received
        CurHp -= damage;

        // Update the UI and health bar
        UpdateUI();

        // Check if hero died
        if (CurHp <= 0) isDead = true;
    }

    void UpdateUI()
    {
        float currentHealthPct = (float)CurHp / (float)MaxHp;
        OnHealthPctChanged(currentHealthPct);
    }

    bool HitCheck(float accuracy)
    {
        return (UnityEngine.Random.Range(0f, 1f) <= accuracy);
    }
}
