using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/SingleTargetSelf/Heal")]
public class Heal : Skill
{
    private void Awake()
    {
        skillTarget = SkillTarget.Self;
    }

    public override void Activate(int atk, Team targets, Hero self)
    {
        Hero target = targets.GetLowestHPMember();
        int healDamage = atk * power / 100;

        // Heal target HP
        // Reusing attack method by making it negative
        target.applyDamage(-healDamage, this);

        // Show floating damage number
        DamagePopup.CreateHeal(target.transform.position, healDamage);
    }

    public override bool ActivationCheck(Team targets)
    {
        Hero target = targets.GetLowestHPMember();
        return (UnityEngine.Random.Range(0f, 100f) <= actChance &&
                target.CurHp < target.MaxHp);
    }
}
