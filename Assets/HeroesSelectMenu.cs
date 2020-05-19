using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HeroesSelectMenu : Singleton<HeroesSelectMenu>
{
    public HeroesSelect_MiniPortrait[] allyMiniPortraits;
    public HeroesSelect_MiniPortrait[] enemyMiniPortraits;

    // Hero Portrait (Center)
    public Image heroImage;
    public TextMeshProUGUI heroName;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillDescription;

    int[] allyHeroes;
    int[] enemyHeroes;
    int selectedHero = 0;
    AllyOrEnemy selectedHeroAlliance = AllyOrEnemy.Ally;

    int getHeroID()
    {
        if (selectedHeroAlliance == AllyOrEnemy.Ally)
        {
            return allyHeroes[selectedHero];
        }
        else
        {
            return enemyHeroes[selectedHero];
        }
    }

    void setHeroID(int id)
    {
        if (selectedHeroAlliance == AllyOrEnemy.Ally)
        {
            allyHeroes[selectedHero] = id;
        }
        else
        {
            enemyHeroes[selectedHero] = id;
        }
    }

    public void LoadMenu()
    {
        allyHeroes = Settings.i.data.allyHeroes;
        enemyHeroes = Settings.i.data.enemyHeroes;

        for (int i=0; i < allyMiniPortraits.Length; i++)
        {
            allyMiniPortraits[i].ChangePortrait(HeroesList.i.GetHero(allyHeroes[i]).heroSprite);
        }

        for (int i = 0; i < enemyMiniPortraits.Length; i++)
        {
            enemyMiniPortraits[i].ChangePortrait(HeroesList.i.GetHero(enemyHeroes[i]).heroSprite);
        }

        ChangeHeroPortrait();
    }

    public void ChangeHeroPortrait()
    {
        ChangeHeroPortrait(HeroesList.i.GetHero(getHeroID()));
    }

    public void ChangeHeroPortrait(HeroData hero)
    {
        heroImage.sprite = hero.heroSprite;
        heroName.text = hero.name;
        skillName.text = hero.skill.name;
        skillDescription.text = hero.skill.description;

        if (selectedHeroAlliance == AllyOrEnemy.Ally)
        {
            allyHeroes[selectedHero] = hero.id;
            allyMiniPortraits[selectedHero].ChangePortrait(hero.heroSprite);
        }
        else if (selectedHeroAlliance == AllyOrEnemy.Enemy)
        {
            enemyHeroes[selectedHero] = hero.id;
            enemyMiniPortraits[selectedHero].ChangePortrait(hero.heroSprite);
        }
    }

    public void NextHeroID()
    {
        int id = getHeroID() + 1;
        if (id > HeroesList.i.Length()) id = 1;

        setHeroID(id);

        ChangeHeroPortrait();
    }

    public void PrevHeroID()
    {
        int id = getHeroID() - 1;
        if (id < 1) id = HeroesList.i.Length();

        setHeroID(id);

        ChangeHeroPortrait();
    }

    public void ChangeSelectedHero(int hero, AllyOrEnemy aoe)
    {
        selectedHero = hero;
        selectedHeroAlliance = aoe;

        ChangeHeroPortrait();
    }

    public void PlayGame()
    {
        // Save new battle settings
        Settings.i.UpdateHeroes(allyHeroes, enemyHeroes);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
