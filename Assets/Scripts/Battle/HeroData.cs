using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HeroData", menuName = "Hero/HeroData")]
public class HeroData : ScriptableObject
{
    public new string name;
    public int id;

    public int hp;
    public int atk;
    public int agi;

    public Skill attackSkill;
    public Skill skill;

    public Sprite heroSprite;
}
