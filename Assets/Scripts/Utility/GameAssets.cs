using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : Singleton<GameAssets>
{
    public Sprite tombstone;

    [Header("Battle Popup Assets")]
    public Transform damagePopup;
    public Transform healPopup;
    public Transform textPopup;
}
