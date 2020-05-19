using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroesSelect_MiniPortrait : MonoBehaviour
{
    public Image heroPortrait;
    public Image border;
    public int id;
    public AllyOrEnemy alliance;

    public void ChangePortrait (Sprite sprite)
    {
        heroPortrait.sprite = sprite;
    }

    public void SelectedHero()
    {
        HeroesSelectMenu.i.ChangeSelectedHero(id, alliance);
    }
}
