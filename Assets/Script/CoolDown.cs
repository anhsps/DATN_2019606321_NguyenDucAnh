using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    Animator animator;
    public Image cooldownImage; // ProgressBar
    public Text cooldownText; // Text

    private bool clickAtk1, performAtk;
    private float nextAtkTime = 0.0f;
    private float atk1Cooldown = 5.0f; // Thời gian cooldown cho atk1

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Time.time >= nextAtkTime)
        {
            // Kiểm tra điều kiện để thực hiện kỹ năng 1
            if ((Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Keypad1) || clickAtk1) && !performAtk)
            {
                // Kiểm tra vùng quét trước khi thực hiện kỹ năng 1
                if (CanPerformAtk1())
                {
                    performAtk = true;
                    nextAtkTime = Time.time + atk1Cooldown; // Đặt thời gian hồi cho atk1
                    animator.SetTrigger("Attack1");
                    // Thực hiện âm thanh, hiệu ứng, v.v.
                }
            }

            // ... Các điều kiện khác cho các kỹ năng khác ...
        }

        UpdateCooldownUI();
    }

    private bool CanPerformAtk1()
    {
        // Kiểm tra điều kiện cho kỹ năng 1
        // Đặt các điều kiện của bạn ở đây
        return !performAtk && true;
    }

    private void UpdateCooldownUI()
    {
        float cooldownPercentage = Mathf.Clamp01((nextAtkTime - Time.time) / atk1Cooldown);

        // Cập nhật ProgressBar
        cooldownImage.fillAmount = cooldownPercentage;

        // Cập nhật Text
        int remainingCooldown = Mathf.CeilToInt(nextAtkTime - Time.time);
        cooldownText.text = remainingCooldown.ToString();
    }
}
