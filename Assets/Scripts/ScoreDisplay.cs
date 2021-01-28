using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI score_text;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        score_text = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = gameSession.GetScore().ToString();
    }
}
