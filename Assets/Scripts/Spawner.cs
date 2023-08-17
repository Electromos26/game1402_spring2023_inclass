using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public AIController prefab;
    [SerializeField]
    private float spawnRange = 3f;
    [SerializeField]
    private float interval = 5f;

    private float timer = Mathf.Infinity;
    private int numTeams;

    Renderer renderer;
    
    [SerializeField]
    private int spawnerTeam = 0;
    // Start is called before the first frame update
    void Start()
    {
        numTeams = GameManager.Instance.teams.Length;
        renderer = GetComponent<Renderer>();

    }
    // Update is called once per frame
    void Update()
    {
        for (int teamIndex = 0; teamIndex < numTeams; teamIndex++)
        {
            if (spawnerTeam == teamIndex)
            {
                Color teamColor = GameManager.Instance.teams[teamIndex];    // set color of spawners to the teamindex selected 
                renderer.material.color = teamColor;


                break;
            }

        }
    }

    private void SpawnAIUnit()
    {
        // Increment the timer with deltaTime
        timer += Time.deltaTime;

        // Check if the timer has reached the spawnInterval
        if (timer >= interval)
        {
            AIController aiUnit = Instantiate(prefab, transform.position, Quaternion.identity);
            aiUnit.setTeam(spawnerTeam); // Set the team for the spawned AI by getting and setting

            timer = 0.0f; // Reset the timer
        }
    }

}

