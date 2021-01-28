using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI health_text;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        health_text = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        health_text.text = player.GetHealth().ToString();
    }
}
