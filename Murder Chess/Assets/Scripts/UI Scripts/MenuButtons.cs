using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour
{

    public void playGame() {

        SceneManager.LoadScene(6); // Loads scene 6 = Board scene

    }

    public void exitGame() {

        Application.Quit(); // pretty sure this doesn't work in editor

    }}
