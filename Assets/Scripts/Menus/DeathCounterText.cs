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
        if (deathCounter == 0)
        {
            this.GetComponent<TMP_Text>().text = $"You got away from Beef Daddy this time...";
        }
        else
        {
            this.GetComponent<TMP_Text>().text = $"You got captured {deathCounter} time(s)...";
        }
    }
}
