using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Leech")]
public class Leech : Skill
{
    public int healPower;

    private void Awake()
    {
        skillTarget = SkillTarget.Enemy;
    }

    public override void Activate(int atk, Team targets, Hero self)
    {
        Hero target = targets.GetRandomHero();
        int damage = atk * power / 100;
        int healDamage = damage * healPower / 100;

        // Hitcheck based on accuracy
        if (HitCheck(accuracy))
        {
            // Apply damage to the enemy
            target.applyDamage(damage, this);

            // Show floating damage number
            DamagePopup.CreateDamage(target.transform.position, damage);

            // Apply the heal to self
            self.applyDamage(-healDamage, this);

            DamagePopup.CreateHeal(self.transform.position, healDamage);
        }
        else // Miss
        {
            // Show miss text
            DamagePopup.CreateText(target.transform.position, "Miss");
        }
    }
}
