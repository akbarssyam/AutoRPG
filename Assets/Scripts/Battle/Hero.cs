using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroState
{
    Idle,
    Attacking
}

public enum AllyOrEnemy
{
    Ally,
    Enemy
}

public class Hero : MonoBehaviour
{
    public int CurHp { get; set; }
    public int MaxHp { get; set; }
    public int Atk { get; set; }
    public int Agi { get; set; }
    public bool isDead { get; set; } = false;
    public AllyOrEnemy allyOrEnemy { get; set; }

    public Skill attackSkill { get; set; }
    public Skill skill { get; set; }

    [HideInInspector]
    public Team team;

    public HeroData heroData;

    public Animator skillAnimator;
    public HeroState heroState = HeroState.Idle;

    public event Action<float> OnHealthPctChanged = delegate { };

    Animator animator;
    AudioSource audioSource;
    AnimatorOverrideController skillAOC;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        skillAOC = new AnimatorOverrideController(skillAnimator.runtimeAnimatorController);
        skillAnimator.runtimeAnimatorController = skillAOC;
    }

    // ----------------------
    // --- Public Methods ---
    // ----------------------

    public void StatusSetup()
    {
        MaxHp = heroData.hp;
        Atk = heroData.atk;
        Agi = heroData.agi;

        CurHp = MaxHp;

        GetComponent<SpriteRenderer>().sprite = heroData.heroSprite;
        skill = heroData.skill;
        attackSkill = heroData.attackSkill;
    }

    public void Action(Team enemy)
    {
        heroState = HeroState.Attacking;
        StartCoroutine(_Action(enemy));
    }

    IEnumerator _Action(Team enemies)
    {
        Team targets;
        if (skill.skillTarget == SkillTarget.Enemy || skill.skillTarget == SkillTarget.EnemyAOE)
            targets = enemies;
        else
            targets = this.team;

        // Check skill activation
        bool activateSkill = skill != null && skill.ActivationCheck(targets);

        // Play skill effect animation
        if (activateSkill)
        {
            skillAOC["SkillIdle"] = skill.animationClip;
            skillAnimator.SetTrigger("ActivateSkill");

            if (skill.castingAudio != null)
            {
                audioSource.clip = skill.castingAudio;
                audioSource.Play();
            }

            yield return new WaitForSeconds(skill.animationClip.length);
        }

        // Play attacking animation
        if (skill.skillTarget == SkillTarget.Enemy || !activateSkill)
            animator.SetTrigger("attack");

        yield return new WaitForSeconds(0.5f);

        if (activateSkill)
        {
            skill.Activate(Atk, targets, this);
        }
        else
        {
            attackSkill.Activate(Atk, enemies, this);
        }

        yield return new WaitForSeconds(0.5f);

        heroState = HeroState.Idle;
    }

    public void applyDamage(int damage, Skill skill)
    {
        // Apply the damage received
        CurHp -= damage;

        // Update the UI and health bar
        UpdateUI();

        // Check if hero died
        if (CurHp <= 0) {
            isDead = true;
            GetComponent<SpriteRenderer>().sprite = GameAssets.i.tombstone;
        }

        // Check for health overflow
        if (CurHp > MaxHp) CurHp = MaxHp;

        // Play hit effect
        skillAOC["SkillIdle"] = skill.hitEffectClip;
        skillAnimator.SetTrigger("ActivateSkill");

        // Play audio
        if (skill.hitEffectAudio != null)
        {
            audioSource.clip = skill.hitEffectAudio;
            audioSource.Play();
        }
    }


    // -----------------------
    // --- Private Methods ---
    // -----------------------


    void UpdateUI()
    {
        float currentHealthPct = (float)CurHp / (float)MaxHp;
        OnHealthPctChanged(currentHealthPct);
    }

}
