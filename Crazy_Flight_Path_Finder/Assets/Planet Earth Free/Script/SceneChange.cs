﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour {

    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }
        
    public void ExitGame()
    {
        Application.Quit();
    }
}
