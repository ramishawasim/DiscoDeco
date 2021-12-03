using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathCounterText : MonoBehaviour
{
    private int deathCounter;

    void Start()
    {
        deathCounter = PlayerPrefs.GetInt("deathCounter");
        this.GetComponent<TMP_Text>().text = $"You Died {deathCounter} Times!";
    }
}