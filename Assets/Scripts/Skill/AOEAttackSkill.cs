using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/AOEAttack")]
public class AOEAttackSkill : Skill
{
    private void Awake()
    {
        skillTarget = SkillTarget.EnemyAOE;
    }

    public override void Activate(int atk, Team targets, Hero self)
    {
        int damage = atk * power / 100;

        foreach (Hero target in targets.heroes)
        {
            // Skip if dead
            if (target.isDead) continue;

            // Hitcheck based on accuracy
            if (HitCheck(accuracy))
            {
                // Apply damage to the enemy
                target.applyDamage(damage, this);

                // Show floating damage number
                DamagePopup.CreateDamage(target.transform.position, damage);
            }
            else // Miss
            {
                // Show miss text
                DamagePopup.CreateText(target.transform.position, "Miss");
            }
        }
    }
}
