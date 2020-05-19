using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    TextMeshPro tmp;
    Color textColor;
    float lifetime = 0.5f;

    // --------------------------
    // ----- Static methods -----
    // --------------------------

    public static DamagePopup CreateDamage(Vector3 pos, int damageAmount)
    {
        return Create(GameAssets.i.damagePopup, pos, damageAmount.ToString());
    }

    public static DamagePopup CreateHeal(Vector3 pos, int damageAmount)
    {
        return Create(GameAssets.i.healPopup, pos, damageAmount.ToString());
    }

    public static DamagePopup CreateText(Vector3 pos, string text)
    {
        return Create(GameAssets.i.textPopup, pos, text);
    }

    public static DamagePopup Create(Transform pf_Popup, Vector3 pos, string text)
    {
        Transform damagePopupTransform = Instantiate(pf_Popup, pos, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(text);

        return damagePopup;
    }

    // Initiatlization
    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    public void Setup(string text)
    {
        tmp.SetText(text);
        textColor = tmp.color;
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = -1f;
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            // Start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            tmp.color = textColor;
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
