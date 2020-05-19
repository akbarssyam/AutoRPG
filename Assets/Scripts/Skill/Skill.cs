using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public new string name = "Default Skill";
    public string description;

    public int power;               // Power multiplier (Attack * power in percent)
    public float accuracy;          // Hit chance
    public int actChance;           // Activation chance

    public AnimationClip animationClip;
    public AnimationClip hitEffectClip;

    public AudioClip castingAudio;
    public AudioClip hitEffectAudio;
    
    public SkillTarget skillTarget;

    public abstract void Activate(int atk, Team targets, Hero self);

    public bool HitCheck(float accuracy)
    {
        return (Random.Range(0f, 1f) <= accuracy);
    }

    public virtual bool ActivationCheck(Team target)
    {
        return (UnityEngine.Random.Range(0f, 100f) <= actChance);
    }

}
