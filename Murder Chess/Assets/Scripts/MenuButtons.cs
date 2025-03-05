using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour
{

    public void playGame() {

        //Load Game scene using SceneManager.LoadScene("GameScene" / [game scene ID])

    }

    public void exitGame() {

        Application.Quit(); // pretty sure this doesn't work in editor

    }


    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
