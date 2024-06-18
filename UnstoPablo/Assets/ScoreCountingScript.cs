using System.Collections.Generic;
using UnityEngine;

public class ScoreCountingScript : MonoBehaviour
{
    public int NpcsStart;
    public int NpcsLeft;

    public int points;

    private void Start()
    {
        NpcsStart = GameObject.FindGameObjectsWithTag("Npc").Length;
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Find all GameObjects with the tag "Npc"
        NpcsLeft = GameObject.FindGameObjectsWithTag("Npc").Length;

        if (NpcsLeft == 0)
        {
            DeactivateAllScripts();
        }
    }

    private void DeactivateAllScripts()
    {
        // Get all MonoBehaviour scripts in the scene
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();

        // Loop through each script and disable it
        foreach (MonoBehaviour script in allScripts)
        {
            script.enabled = false;
        }
    }
}