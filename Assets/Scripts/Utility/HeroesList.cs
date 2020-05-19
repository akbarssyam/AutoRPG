using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesList: Singleton<HeroesList>
{
    private Dictionary<int, HeroData> m_HeroMap = new Dictionary<int, HeroData>();

    private void Awake()
    {
        //foreach (HeroData hero in Resources.FindObjectsOfTypeAll(typeof(HeroData)) as HeroData[])
        foreach (HeroData hero in Resources.LoadAll<HeroData>("ScriptableObjects/Heroes") as HeroData[])
        {
            Debug.Log($"Adding Hero ID {hero.id}");
            m_HeroMap.Add(hero.id, hero);
        }
    }

    public int Length()
    {
        return m_HeroMap.Count;
    }

    public HeroData GetHero(int id)
    {
        return m_HeroMap[id];
    }
}
