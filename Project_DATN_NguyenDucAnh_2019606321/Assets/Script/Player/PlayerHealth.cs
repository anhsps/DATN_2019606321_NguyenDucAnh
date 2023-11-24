using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    public int maxHP = 200, maxMP = 50;
    [HideInInspector] public float currentHP, currentMP;

    public Slider SliderHP, SliderMP;
    public Text HPScore, MPScore;

    [Header("iFrames")]
    public float iFramesDuration = 1;//time bất khả xâm phạm
    public int numberOfFlashes = 4;//số lần nhấp nháy
    SpriteRenderer spriteRend;

    [Header("sound, gameover")]
    public AudioSource death_audio;
    public AudioSource hurt_audio;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject buttonPause;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();

        currentHP = maxHP;
        SliderHP.maxValue = maxHP;
        SliderHP.value = currentHP;
        HPScore.text = currentHP + " / " + maxHP;

        currentMP = 10;//
        SliderMP.maxValue = maxMP;
        SliderMP.value = currentMP;
        MPScore.text = currentMP + " / " + maxMP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hurt_audio.Play();       

        if (damage < 0) return;
        currentHP -= damage;
        animator.SetTrigger("Hurt");
        StartCoroutine(Invulnerability());//start quy trình(bất khả xâm phạm)

        SliderHP.value = currentHP;
        if (currentHP < 0)      HPScore.text = "0 / " + maxHP;        
        else                    HPScore.text = currentHP + " / " + maxHP;
        if (currentHP <= 0)     Die();
    }
    public void AddHP(int amountHP)
    {
        currentHP += amountHP;
        if (currentHP > maxHP) currentHP = maxHP;
        SliderHP.value = currentHP;
        HPScore.text = currentHP + " / " + maxHP;
    }
    public void AddMP(int amountMP)
    {
        currentMP += amountMP;
        if (currentMP > maxMP) currentMP = maxMP;
        SliderMP.value = currentMP;
        MPScore.text = currentMP + " / " + maxMP;
    }
    void Die()
    {
        death_audio.Play();
        Invoke("Lose", 1f);
        buttonPause.SetActive(false);

        animator.SetTrigger("Die");
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    void Lose()
    {//menu thua
        GameOverUI.SetActive(true);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);//IgnoreLayerCollision: lớp bỏ qua va chạm(layer player,enemy, true)
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 1);//mã color RGB: 0-1.0
            //lợi nhuận trả về trọng số mới trong vài giây để làm cho mã có trọng lượng trước khi chạy dòng tiếp
            yield return new WaitForSeconds(iFramesDuration / numberOfFlashes / 2);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / numberOfFlashes / 2);
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);//tắt IgnoreLayerCollision
    }
}
