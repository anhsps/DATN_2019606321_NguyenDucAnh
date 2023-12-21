﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("LoadMenu", 2.5f);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
