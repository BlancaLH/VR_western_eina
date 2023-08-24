using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class menuManager : MonoBehaviour
{

    public GameObject button1_text_object;
    Text button1_text;

    public GameObject button2_text_object;
    Text button2_text;

    public void loadGameScene()
    {
        string scene = "GameScene";
        string game_mode = "free";
        PlayerPrefs.SetString("mode", game_mode);
        SceneManager.LoadScene(scene);
    }

    public void loadExperimentScene()
    {
        string scene = "GameScene";
        string game_mode = "experiment";
        PlayerPrefs.SetString("mode", game_mode);
        SceneManager.LoadScene(scene);
    }

    public void SetSpanish()
    {
        PlayerPrefs.SetString("lang", "es");
        string lang = PlayerPrefs.GetString("lang", "es");
        SetButton1Text(lang);
        SetButton2Text(lang);
    }

    public void SetEnglish()
    {
        PlayerPrefs.SetString("lang", "eng");
        string lang = PlayerPrefs.GetString("lang", "es");
        SetButton1Text(lang);
        SetButton2Text(lang);
    }

    void Start()
    {
        button1_text = button1_text_object.GetComponent<Text>();
        button2_text = button2_text_object.GetComponent<Text>();

        SetSpanish();
    }

    void Update()
    {
        // Keybindings for the menu
        // The keybindings are intended for the vertical mode of the joystick
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            SetSpanish();
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            SetEnglish();
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            loadGameScene();
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            loadExperimentScene();
        }

        // Bindings for laptop
        
        /*
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            SetSpanish();
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            SetEnglish();
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            loadGameScene();
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            loadExperimentScene();
        }*/
        
    }

    private void SetButton1Text(string lang)
    {
        if (lang == "es")
        {
            button1_text.text = "Jugar";
        }
        else
        {
            button1_text.text = "Play";
        }
    }

    private void SetButton2Text(string lang)
    {
        if (lang == "es")
        {
            button2_text.text = "Experimento";
        }
        else
        {
            button2_text.text = "Experiment";
        }
    }

}
