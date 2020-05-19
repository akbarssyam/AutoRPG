using System;
using System.IO;
using UnityEngine;

[Serializable]
public class SettingsJSON
{
    public int[] allyHeroes;
    public int[] enemyHeroes;
    public int battleSpeed;
}

public class Settings: Singleton<Settings>
{
    public static string filename = "settings.json";
    [HideInInspector]
    public SettingsJSON data;

    private void Awake()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        string path = Application.dataPath + "/" + filename;

        string jsonText = File.ReadAllText(path);

        data = JsonUtility.FromJson<SettingsJSON>(jsonText);
    }

    public void UpdateSpeed(int battleSpeed)
    {
        data.battleSpeed = battleSpeed;

        SaveSettings();
    }

    public void UpdateHeroes(int[] allyHeroes, int[] enemyHeroes)
    {
        data.allyHeroes = allyHeroes;
        data.enemyHeroes = enemyHeroes;

        SaveSettings();
    }

    void SaveSettings()
    {
        string path = Application.dataPath + "/" + filename;

        string content = JsonUtility.ToJson(data);

        File.WriteAllText(path, content);
    }
}