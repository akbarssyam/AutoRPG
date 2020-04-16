using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/SingleTargetAttack")]
public class SingleTargetAttackSkill : Skill
{
    public override void Activate(int atk, Hero[] targets)
    {
        Hero target = targets[0];
        int damage = atk * power / 100;

        // Hitcheck based on accuracy
        if (HitCheck(accuracy))
        {
            // Apply damage to the enemy
            target.applyDamage(damage);

            // Show floating damage number
            DamagePopup.Create(target.transform.position, damage);
        }
        else // Miss
        {
            // Show miss text
            DamagePopup.Create(target.transform.position, "Miss");
        }
    }
}
