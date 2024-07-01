using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : MonoBehaviour
{
    public AudioSource atk1_1_audio, atk1_2_audio, atk1_Stab_audio, atk2_audio;
    Animator animator;
    //string[] listStatesAtk = { "atk1", "atk2", "atk3", "atk4" };

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SoundIno()
    {
        if (IsInSpecificStates("atk1")) atk1_2_audio.Play();
        else if (IsInSpecificStates("atk2")) atk1_1_audio.Play();
    }
    void SoundAtk1()
    {
        atk1_1_audio.Play();
    }
    void SoundAtk1_Stab()
    {
        atk1_Stab_audio.Play();
    }
    void SoundAtk2()
    {
        atk2_audio.Play();
    }

    bool IsInSpecificStates(params string[] stateNames)
    {//các states cụ thể
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        foreach (string stateName in stateNames)
        {
            if (stateInfo.IsName(stateName))
                return true;
        }
        return false;
    }
}
