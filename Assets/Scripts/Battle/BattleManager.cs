using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

enum BattleState
{
    Waiting,
    PlayerTurn,
    EnemyTurn,
    BattleWin,
    BattleLose,
    BattleEnd
}

public class BattleManager : MonoBehaviour
{
    public Hero ally;
    public Hero enemy;
    public GameObject victoryBanner, defeatBanner;
    public GameObject battleEndCanvas;
    public TextMeshProUGUI battleEndMessage;

    BattleState currentState = BattleState.Waiting;

    // Start is called before the first frame update
    void Start()
    {
        startBattle();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case BattleState.PlayerTurn:
                playerTurn();
                break;
            case BattleState.EnemyTurn:
                enemyTurn();
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

    void startBattle()
    {
        currentState = BattleState.PlayerTurn;
    }

    void playerTurn()
    {
        currentState = BattleState.Waiting;
        StartCoroutine(_playerTurn());
    }

    IEnumerator _playerTurn()
    {
        ally.Attack(enemy);

        yield return new WaitForSeconds(1f);

        currentState = BattleState.EnemyTurn;
        DamageCheck();
    }

    void enemyTurn()
    {
        currentState = BattleState.Waiting;
        StartCoroutine(_enemyTurn());
    }

    IEnumerator _enemyTurn()
    {
        enemy.Attack(ally);

        yield return new WaitForSeconds(1f);

        currentState = BattleState.PlayerTurn;
        DamageCheck();
    }

    void DamageCheck()
    {
        if (enemy.isDead) currentState = BattleState.BattleWin;
        else if (ally.isDead) currentState = BattleState.BattleLose;
    }

    void EndScreen(string message)
    {
        battleEndCanvas.SetActive(true);
        battleEndMessage.text = message.ToUpper();

        currentState = BattleState.Waiting;
    }
}
