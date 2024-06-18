using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Dodaj referencjê do TextMeshPro

public class ScoreCountingScript : MonoBehaviour
{
    public int NpcsStart;
    public int NpcsLeft;
    public int points;

    public TextMeshProUGUI pointsText;  // Referencja do komponentu TextMeshPro

    private void Start()
    {
        NpcsStart = GameObject.FindGameObjectsWithTag("Npc").Length;
        points = 0;

        // Przyk³ad przypisania w inspektorze: pointsText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        NpcsLeft = GameObject.FindGameObjectsWithTag("Npc").Length;

        if (NpcsLeft == 0)
        {
            DeactivateAllScripts();
        }

        // Aktualizacja tekstu w TextMeshPro
        pointsText.text = "Points: " + points.ToString();
    }

    private void DeactivateAllScripts()
    {
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();

        foreach (MonoBehaviour script in allScripts)
        {
            script.enabled = false;
        }
    }
}