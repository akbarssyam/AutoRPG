using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class SpeedMultiplier : Singleton<SpeedMultiplier>
{
    public TextMeshProUGUI text;

    public static float[] multiplier = { 1f, 1.5f, 2f };
    public static string[] multiplierText = { "1", "1.5", "2" };

    int speedMode = 0; // Index coresponding to multiplier array

    private void Start()
    {
        ChangeSpeed(Settings.i.data.battleSpeed);
    }

    public void ChangeSpeed()
    {
        speedMode = (speedMode + 1) % SpeedMultiplier.multiplier.Length;
        ChangeSpeed(speedMode);
    }

    public void ResetSpeed()
    {
        speedMode = 0;
        ChangeSpeed(speedMode);
    }

    public void ChangeSpeed(int i)
    {
        speedMode = i % SpeedMultiplier.multiplier.Length;
        Time.timeScale = SpeedMultiplier.multiplier[speedMode];
        updateUI();

        Settings.i.UpdateSpeed(speedMode);
    }

    void updateUI()
    {
        text.text = multiplierText[speedMode] + "x";
    }
}
