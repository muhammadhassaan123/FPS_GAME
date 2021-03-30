﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused=false;
    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Escape)){
        //     if(gameIsPaused){
        //         Resume();
        //     }else{
        //         Pause();
        //     }
        // }
    }
    public void  Resume(){
        
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    
    }
    public void Pause(){
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;        
    }
    public void LoadMenu(){
        SceneManager.LoadScene(1);
    }
}
