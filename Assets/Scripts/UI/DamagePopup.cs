using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    TextMeshPro tmp;
    Color textColor;
    float lifetime = 0.5f;

    public static DamagePopup Create(Vector3 pos, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.damagePopup, pos, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    public static DamagePopup Create(Vector3 pos, string text)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.damagePopup, pos, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(text);

        return damagePopup;
    }

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        tmp.SetText(damageAmount.ToString());
        textColor = tmp.color;
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
