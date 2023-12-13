using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;//để dùng Math.Round làm tròn chữ số thập phân

public class SpellCooldown : MonoBehaviour
{
    [HideInInspector] public Image imageCooldown;
    public Image imageCooldown1, imageCooldown2, imageCooldown3, imageCooldown4, imageCooldownDash;
    //[SerializeField] private Text textCooldown;

    bool isCooldown, isCooldownDash;
    [HideInInspector] public float cooldownTime = 1f;
    float cooldownTimeDash = 1f;
    float cooldownTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //textCooldown.gameObject.SetActive(false);
        imageCooldown1.fillAmount = imageCooldown2.fillAmount = imageCooldown3.fillAmount = imageCooldown4.fillAmount = 0;
        imageCooldownDash.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Q)) UseSpell();*/
        if (isCooldown)
        {
            ApplyCooldown();
        }
        if (isCooldownDash)
        {
            ApplyCooldownDash();
        }
    }

    void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0)
        {
            isCooldown = false;
            //textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0;
        }
        else
        {
            //textCooldown.text = Math.Round(cooldownTimer,1).ToString();//Mathf.RoundToInt -> tròn số nguyên
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public void UseSpell()
    {
        if (!isCooldown)
        {
            isCooldown = true;
            //textCooldown.gameObject.SetActive(true);
            cooldownTimer = cooldownTime;
        }
    }

    //xài riêng cho skill Dash, vì game để Dash dùng kết hợp vs skill atk1 dc mà ko trùng Spell Cooldown
    void ApplyCooldownDash()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0)
        {
            isCooldownDash = false;
            imageCooldownDash.fillAmount = 0;
        }
        else
        {
            imageCooldownDash.fillAmount = cooldownTimer / cooldownTimeDash;
        }
    }

    public void UseSpellDash()
    {
        if (!isCooldownDash)
        {
            isCooldownDash = true;
            cooldownTimer = cooldownTimeDash;
        }
    }
}
