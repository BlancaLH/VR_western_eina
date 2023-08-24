using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class targetMovement : MonoBehaviour
{
    bool is_moving = false;
    bool moving_right = true;

    float velocity_speed = 0.01f;

    float x_max = 20.0f;
    float x_min = -3.0f;

    public GameObject text_object;
    Text text;

    int points_per_shot = 10;

    string lang = "";


    // Start is called before the first frame update
    void Start()
    {
        text = text_object.GetComponent<Text>();
        lang = PlayerPrefs.GetString("lang", "es");
        UpdateScoreMessage();
    }

    void UpdatePlayerPoints()
    {
        // Function to update the score of the player
        int points = PlayerPrefs.GetInt("score", 0);
        points = points + points_per_shot;
        PlayerPrefs.SetInt("score", points);

        int goodShots = PlayerPrefs.GetInt("numGoodShots", 0);
        goodShots = goodShots + 1;
        PlayerPrefs.SetInt("numGoodShots", goodShots);
    }

    void UpdateScoreMessage()
    {
        // Function to update the score message
        int points = PlayerPrefs.GetInt("score", 0);
        string score_msg = "Puntuación:";

        if (lang == "eng")
        {
            score_msg = "Score:";
        }

        text.text = score_msg + " " + points.ToString();
    }

    void MoveTarget()
    {
        // Function to move the cowboy
        if (moving_right)
        {
            transform.position = transform.position - velocity_speed * transform.right;
            if (transform.position.x >= x_max) moving_right = false;
        }
        else
        {
            transform.position = transform.position + velocity_speed * transform.right;
            if (transform.position.x <= x_min) moving_right = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the player makes 2 good shots, start moving the cowboy
        int numberGoodShots = PlayerPrefs.GetInt("numGoodShots",0);
        if (numberGoodShots >= 2)
        {
            is_moving = true;
        }

        if (is_moving)
        {
            MoveTarget();
        }
    }



    void OnCollisionEnter(Collision obj)
    {
        // An arrow has hit the cowboy, update score and the canvas message
        if (obj.gameObject.tag == "arrow")
        {
            UpdatePlayerPoints();
            UpdateScoreMessage();
        }
    }
}
