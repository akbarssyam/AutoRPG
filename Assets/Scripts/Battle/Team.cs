using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Team : MonoBehaviour
{
    public Hero[] heroes;

    public static Team CalculateBattleOrder(Team team1, Team team2)
    {
        Team battleOrder = new Team();
        battleOrder.heroes = team1.heroes.Concat(team2.heroes).OrderByDescending(x => x.Agi).ToArray();
        return battleOrder;
    }

    public Hero GetLowestHPMember()
    {
        Hero returnHero = null;
        float hp, lowestHPPct = 100f;

        foreach(Hero hero in heroes)
        {
            hp = (float)hero.CurHp * 100 / hero.MaxHp;
            if (hp < lowestHPPct && !hero.isDead)
            {
                returnHero = hero;
                lowestHPPct = hp;
            }
        }

        if (returnHero == null) returnHero = GetRandomHero();

        return returnHero;
    }

    public Hero GetRandomHero()
    {
        int index = Random.Range(0, heroes.Length);

        if (heroes[index].isDead) return GetRandomHero();
        else return heroes[index];
    }

    public Hero ActiveHero(int turn)
    {
        return heroes[turn % heroes.Length];
    }

    public void MarkAlliesOrEnemies(AllyOrEnemy aoe)
    {
        foreach (Hero hero in heroes)
        {
            hero.allyOrEnemy = aoe;
            hero.team = this;
        }
    }
}
