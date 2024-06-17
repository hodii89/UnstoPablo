using System.Collections.Generic;
using UnityEngine;

public class ScoreCountingScript : MonoBehaviour
{
    public int NpcsLeft;

    // Update is called once per frame
    void Update()
    {
        // Find all GameObjects with the tag "Npc"
        NpcsLeft = GameObject.FindGameObjectsWithTag("Npc").Length;

        // You can further process the NPC count here, for example:
        // - Update a UI text element to display the count
        // - Trigger events or actions based on the NPC count
    }
}