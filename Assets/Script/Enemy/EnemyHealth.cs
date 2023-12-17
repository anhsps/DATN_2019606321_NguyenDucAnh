using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    Animator animator;
    public int maxHP = 50;
    [HideInInspector] public int currentHP;
    [HideInInspector] public string hurtPrefix="Hurt";//xài trong boss Na
    public Slider E_HP_Slider;
    public bool drop = true;
    public GameObject item;
    public GameObject bloodEffect;
    public AudioSource maleHurt_audio, femaleHurt_audio;
    public bool male = true;
    AudioSource hurt_audio;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        E_HP_Slider.maxValue = maxHP;
        E_HP_Slider.value = currentHP;

        /*if (male) hurt_audio = maleHurt_audio;
        else hurt_audio = femaleHurt_audio;*/
        hurt_audio = male ? maleHurt_audio : femaleHurt_audio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hurt_audio.Play();
        animator.SetTrigger(hurtPrefix);
        currentHP -= damage;
        E_HP_Slider.value = currentHP;
        if (currentHP <= 0) Die();
    }
    void Die()
    {
        hurt_audio.Play();
        animator.SetTrigger("Die");
        GetComponent<Collider2D>().enabled = false;
        Instantiate(bloodEffect, transform.position, transform.rotation);
        Destroy(gameObject, 1.5f);
    }

    public void DropItem()
    {
        if (drop) Instantiate(item, transform.position, transform.rotation);
    }
}
