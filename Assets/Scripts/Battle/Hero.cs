using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public int CurHp { get; set; }
    public int MaxHp { get; set; }
    public int Atk { get; set; }
    public bool isDead { get; set; } = false;

    public Skill attackSkill;
    public Skill skill;

    public event Action<float> OnHealthPctChanged = delegate { };

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        StatusSetup();
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

        // Check skill activation
        if (skill != null && SkillActivationCheck(skill))
        {
            skill.Activate(Atk, new Hero[] { enemy });
        }
        else
        {
            attackSkill.Activate(Atk, new Hero[] { enemy });
        }

    }

    public void applyDamage(int damage)
    {
        // Apply the damage received
        CurHp -= damage;

        // Update the UI and health bar
        UpdateUI();

        // Check if hero died
        if (CurHp <= 0) isDead = true;
    }


    // -----------------------
    // --- Private Methods ---
    // -----------------------

    void StatusSetup()
    {
        MaxHp = 100;
        Atk = 20;

        CurHp = MaxHp;
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

    bool SkillActivationCheck(Skill skill)
    {
        return (UnityEngine.Random.Range(0f, 100f) <= skill.actChance);
    }
}
