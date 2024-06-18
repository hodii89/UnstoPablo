using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ScoreCountingScript : MonoBehaviour
{
    public int NpcsStart;
    public int NpcsLeft;
    public int points;

    public TextMeshProUGUI pointsText; // Referencja do komponentu TextMeshPro
    public TextMeshProUGUI pointsTextRestart; // Referencja do komponentu TextMeshPro
    public GameObject npcIconPrefab; // Prefab ikony NPC
    public Transform iconContainer; // Kontener dla ikon NPC
    public Vector2 iconOffset; // Offset miêdzy ikonami NPC
    public GameObject FinalScore;
    private void Start()
    {
        NpcsStart = GameObject.FindGameObjectsWithTag("Npc").Length;
        points = 0;
        UpdateNpcIcons();
        pointsTextRestart.enabled = false;

        // Przyk³ad przypisania w inspektorze: pointsText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        NpcsLeft = GameObject.FindGameObjectsWithTag("Npc").Length;
        UpdateNpcIcons();

        if (NpcsLeft == 0)
        {
            
            DeactivateAllScripts();
            FinalScore.SetActive(true);
        }

        // Aktualizacja tekstu w TextMeshPro
        pointsText.text = "Points: " + points.ToString();
        pointsTextRestart.text = "Points: " + points.ToString();
    }

    private void DeactivateAllScripts()
    {
        pointsText.enabled = false;
        pointsTextRestart.enabled = true;

        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();

        foreach (MonoBehaviour script in allScripts)
        {
            if(script != this || script != FinalScore.GetComponent<Image>())
            {
                script.enabled = false;
            }  
        }
    }

    private void UpdateNpcIcons()
    {
        // Usuniêcie starych ikon
        foreach (Transform child in iconContainer)
        {
            Destroy(child.gameObject);
        }

        // Dodanie nowych ikon
        for (int i = 0; i < NpcsLeft; i++)
        {
            GameObject icon = Instantiate(npcIconPrefab, iconContainer);
            icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * iconOffset.x, 0);
        }
    }
}