using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Networking;

public class playerStats
{
    // Auxiliar class to store the stats of a player
    public string name { get; set; }
    public int score { get; set; }
    public int numShots { get; set; }
    public int numGoodShots { get; set; }
    public int numChanges { get; set; }
    public string changeOrder { get; set; }

    public playerStats(string p_name, 
                       int p_score, 
                       int p_numShots, 
                       int p_numGoodShots,
                       int p_numChanges,
                       string p_changeOrder)
    {
        name = p_name;
        score = p_score;
        numShots = p_numShots;
        numGoodShots = p_numGoodShots;
        numChanges = p_numChanges;
        changeOrder = p_changeOrder;
    }
}

public class gameOverLogic : MonoBehaviour
{

    // URL of the form that stores the data
    private string FORM_URL = "url_to_form";

    public GameObject playerNameObject;
    Text playerNameText;

    public GameObject scoreObject;
    Text scoreText;

    public GameObject numShotsObject;
    Text numShotsText;

    public GameObject numGoodShotsObject;
    Text numGoodShotsText;

    string lang = "";


    // Start is called before the first frame update
    void Start()
    {

        playerNameText = playerNameObject.GetComponent<Text>();
        scoreText = scoreObject.GetComponent<Text>();
        numShotsText = numShotsObject.GetComponent<Text>();
        numGoodShotsText = numGoodShotsObject.GetComponent<Text>();

        lang = PlayerPrefs.GetString("lang", "es");

        // Obtain the stats of the game
        playerStats ps = getPlayerStats();

        // Send the stats to a Google Forms
        StartCoroutine(sendDataToForm(ps));

        // Display the results
        DisplayStatsTextMessage(ps);

    }

    IEnumerator sendDataToForm(playerStats ps)
    {
        // Create a form to send all the data to a Google Forms
        WWWForm form = new WWWForm();
        form.AddField("entry.fill_with_own_id", ps.name);
        form.AddField("entry.fill_with_own_id", ps.score);
        form.AddField("entry.fill_with_own_id", ps.numShots);
        form.AddField("entry.fill_with_own_id", ps.numGoodShots);
        form.AddField("entry.fill_with_own_id", ps.numChanges);
        form.AddField("entry.fill_with_own_id", ps.changeOrder);


        UnityWebRequest www = UnityWebRequest.Post(FORM_URL, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Data correctly sent!");
        }
    }

    string generateName()
    {
        int id = (int)Random.Range(0.0f, 9999.0f);
        string name = "Player" + id.ToString();
        return name;
    }

    playerStats getPlayerStats()
    {
        // Function to store all the obtained results
        string name = generateName();
        int score = PlayerPrefs.GetInt("score",0);
        int numShots = PlayerPrefs.GetInt("numShots", 0);
        int numGoodShots = PlayerPrefs.GetInt("numGoodShots", 0);
        int numChanges = PlayerPrefs.GetInt("numChanges",0);

        Debug.Log("Total number of changes: " + numChanges.ToString());

        string changeOrder = "";

        // Iterate to get the order of the changes
        for (int i = 0; i < numChanges; i++)
        {
            string key = "exp_id_" + i.ToString();
            int exp_id = PlayerPrefs.GetInt(key, 0);
            changeOrder += exp_id.ToString() + ",";
        }

        playerStats ps = new playerStats(name, score, numShots, numGoodShots, numChanges, changeOrder);

        return ps;
    }

    void DisplayStatsTextMessage(playerStats ps)
    {
        // Display the results in screen
        string playerNameMsg = "Nombre del jugador:";
        string scoreMsg = "Tu puntuación:";
        string numShotsMsg = "Número de disparos:";
        string numGoodShotsMsg = "Número de disparos acertados:";

        if (lang == "eng")
        {
            playerNameMsg = "Player name:";
            scoreMsg = "Your score:";
            numShotsMsg = "Number of shots:";
            numGoodShotsMsg = "Number of good hits:";
        }

        playerNameText.text = playerNameMsg + " " + ps.name;
        scoreText.text = scoreMsg + " " + ps.score.ToString();
        numShotsText.text = numShotsMsg + " " + ps.numShots.ToString();
        numGoodShotsText.text = numGoodShotsMsg + " " + ps.numGoodShots.ToString();
    }

}
