using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScreenChange : MonoBehaviour
{
    public PlayerController player;
    public Canvas canvas = null;

    public int index;
    public string nextLevel;

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LightBot1()
    {
        SceneManager.LoadScene("LightBot #1");
    }
    public void LightBot2()
    {
        SceneManager.LoadScene("LightBot #2");
    }
    public void LightBot3()
    {
        SceneManager.LoadScene("LightBot #3");
    }
    public void LightBot4()
    {
        SceneManager.LoadScene("LightBot #4");
    }
    public void LightBot5()
    {
        SceneManager.LoadScene("LightBot #5");
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void GameReset()
    {
        var current = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(current);
        SceneManager.LoadScene(current.name);
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        canvas = GetComponentInParent<Canvas>();
        canvas.enabled = false;
        canvas = GameObject.FindGameObjectWithTag("Pause").GetComponent<Canvas>();
        canvas.enabled = true;
    }
    public void GameResume()
    {
        Time.timeScale = 1;
        canvas = GetComponentInParent<Canvas>();
        canvas.enabled = false;
        canvas = GameObject.FindGameObjectWithTag("Game").GetComponent<Canvas>();
        canvas.enabled = true;
    }

    public void GameGo()
    {
        ActionPanelActionCreator actionListManager = GetComponentInParent<ActionPanelActionCreator>();
        if (actionListManager)
        {
            actionListManager.UpdatePlayerActionList();
        }
        
    }

    public string NextLevel(string currentLevel)
    {

        Console.WriteLine(NextLevel("LightBot #1")); // outputs: LightBot #2
        Console.WriteLine(NextLevel("LightBot #2")); // outputs: LightBot #3
        Console.WriteLine(NextLevel("LightBot #3")); // outputs: LightBot #4
        Console.WriteLine(NextLevel("LightBot #4")); // outputs: LightBot #5

        return currentLevel;
    }

}