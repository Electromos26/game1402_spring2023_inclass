using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;

public class GameMenu : MonoBehaviour
{
    //Our game menu is going to keep track of our UI information, as well as allow access to things like a pause menu, and an exit screen. As well, we want it to keep track of a cursor for aiming
    public static bool isPaused; //this keeps track of whether our game is paused or not
    public ExitPopup exitPopup;
    public TextMeshProUGUI scoreTextPrefab; // Renamed to better reflect its purpose
    public TextMeshProUGUI deathTextPrefab;
    public TextMeshProUGUI killTextPrefab;
    private int numTeams;
    private TextMeshProUGUI[] scoreFields;//this score fields is based on the number of teams and their respective values
    private TextMeshProUGUI[] deathFields;//this score fields is based on the number of teams and their respective values

    private int[] teamDeathCounts;    // Start is called before the first frame update
    void Start()
    {
        numTeams = GameManager.Instance.teams.Length;
        scoreFields = new TextMeshProUGUI[numTeams];
        deathFields = new TextMeshProUGUI[numTeams];

        teamDeathCounts = new int[numTeams];

        for (int i = 0; i < numTeams; i++)
        {
            //here we need to setup our text fields
            scoreFields[i] = Instantiate(scoreTextPrefab);
            scoreFields[i].transform.SetParent(scoreTextPrefab.transform.parent, false);
            scoreFields[i].color = GameManager.Instance.teams[i];

            deathFields[i] = Instantiate(deathTextPrefab); // Instantiate the alive teammate count field
            deathFields[i].transform.SetParent(deathTextPrefab.transform.parent, false);
            deathFields[i].color = GameManager.Instance.teams[i];
            teamDeathCounts[i] = 0;
        }
        Destroy(scoreTextPrefab.gameObject); //we use the text prefab to create objects attached to a particular parent, and then we destroy them
        Destroy(deathTextPrefab.gameObject); //we use the text prefab to create objects attached to a particular parent, and then we destroy them
        isPaused = false; //we start with our game paused
    }
    public void TeamDeath(int teamIndex)
    {
        teamDeathCounts[teamIndex]++;
    }

    // Update is called once per frame
    void Update()
    {

        //let's update the text all the time

       

        for (int i = 0; i < numTeams; i++)
        {
            deathFields[i].text = "died: \n" + teamDeathCounts[i].ToString();
            scoreFields[i].text = "score: \n" + ScoreManager.Instance.scores[i].ToString();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;//this reverses a boolean value
            exitPopup.gameObject.SetActive(isPaused);
        }
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0; //this timeScale set to 0 pauses our game
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1; //we are now back at regular speed
        }
      
    }
}
