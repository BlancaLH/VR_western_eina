using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameLogic : MonoBehaviour
{
    const string scene = "GameOverScene";

    float game_time = 30.0f;

    public GameObject time_text_object;
    Text time_text;

    string game_mode = "";

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the playerprefs
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("numShots", 0);
        PlayerPrefs.SetInt("numGoodShots",0);
        PlayerPrefs.SetInt("numChanges", 0);

        time_text = time_text_object.GetComponent<Text>();

        game_mode = PlayerPrefs.GetString("mode", "");
        Debug.Log(game_mode);

        // If testing mode, increase the game time
        if (game_mode == "free")
        {
            game_time = 60.0f;
        }
    }

    void UpdateTimerText()
    {
        float minutes = Mathf.FloorToInt(game_time / 60);
        float seconds = Mathf.FloorToInt(game_time % 60);

        time_text.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {

        game_time = game_time - Time.deltaTime;

        // If the time has run out, load game over scene
        if (game_time <= 0.0f || Input.GetKey("m"))
        {
            SceneManager.LoadScene(scene);
        }
        else
        {
            UpdateTimerText();
        }
    }
}
