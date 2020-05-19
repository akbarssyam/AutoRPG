using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

enum BattleState
{
    Waiting,
    HeroAction,
    BattleWin,
    BattleLose,
    BattleEnd
}

public class BattleManager : MonoBehaviour
{
    public Team allies;
    public Team enemies;
    public GameObject battleEndCanvas;
    public TextMeshProUGUI battleEndMessage;

    Team battleOrder;
    int currentTurn = 0;
    int allyNum = 3, enemyNum = 3;

    BattleState currentState = BattleState.Waiting;

    // Start is called before the first frame update
    void Start()
    {
        // Load Hero data from settings
        initHeroData();

        // Initiate battle order
        initBattleOrder();

        Invoke("startBattle", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case BattleState.HeroAction:
                heroAction();
                break;
            case BattleState.BattleWin:
                EndScreen("VICTORY");
                break;
            case BattleState.BattleLose:
                EndScreen("DEFEAT");
                break;
        }
    }

    // ----------------------
    // --- Public Methods ---
    // ----------------------


    // -----------------------
    // --- Private Methods ---
    // -----------------------

    void initHeroData()
    {
        for (int i = 0; i < allyNum; i++)
        {
            allies.heroes[i].heroData = HeroesList.i.GetHero(Settings.i.data.allyHeroes[i]);
            allies.heroes[i].StatusSetup();
        }

        for (int i = 0; i < enemyNum; i++)
        {
            enemies.heroes[i].heroData = HeroesList.i.GetHero(Settings.i.data.enemyHeroes[i]);
            enemies.heroes[i].StatusSetup();
        }
    }

    void initBattleOrder()
    {
        allies.MarkAlliesOrEnemies(AllyOrEnemy.Ally);
        enemies.MarkAlliesOrEnemies(AllyOrEnemy.Enemy);

        battleOrder = Team.CalculateBattleOrder(allies, enemies);
    }

    void startBattle()
    {
        currentState = BattleState.HeroAction;
    }

    void nextTurn()
    {
        currentTurn = (currentTurn + 1) % battleOrder.heroes.Length;
    }

    void heroAction()
    {
        currentState = BattleState.Waiting;
        StartCoroutine(_heroAction());
    }

    IEnumerator _heroAction()
    {
        Hero activeHero = battleOrder.ActiveHero(currentTurn);

        if (!activeHero.isDead)
        {
            switch (activeHero.allyOrEnemy)
            {
                case AllyOrEnemy.Ally:
                    activeHero.Action(enemies);
                    break;
                case AllyOrEnemy.Enemy:
                    activeHero.Action(allies);
                    break;
            }

            while (activeHero.heroState == HeroState.Attacking)
                yield return null;
        }

        nextTurn();
        currentState = BattleState.HeroAction;

        if (!activeHero.isDead) DamageCheck();
    }

    void DamageCheck()
    {
        bool allDead = true;
        foreach (Hero enemy in enemies.heroes) if (!enemy.isDead) allDead = false;

        if (allDead)
        {
            currentState = BattleState.BattleWin;
            return;
        }

        allDead = true;
        foreach (Hero ally in allies.heroes) if (!ally.isDead) allDead = false;
        if (allDead)
        {
            currentState = BattleState.BattleLose;
            return;
        }
    }

    void EndScreen(string message)
    {
        battleEndCanvas.SetActive(true);
        battleEndMessage.text = message.ToUpper();

        // Reset the game speed to 1
        //SpeedMultiplier.i.ResetSpeed();

        currentState = BattleState.Waiting;
    }
}
