using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour/*,IPointerDownHandler,IPointerUpHandler*/
{
    /*[SerializeField] private Image _img;
    [SerializeField] private Sprite _default,_pressed;


    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
    }*/

    Animator animator;
    public void PointEnterPlay()
    {
        animator.SetBool("Scale", true);
    }
    public void PointExitPlay()
    {
        animator.SetBool("Scale", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
