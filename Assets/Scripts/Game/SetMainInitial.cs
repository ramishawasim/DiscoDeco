using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMainInitial : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
        //Set "deathCounter" to 0 at start
        PlayerPrefs.SetInt("deathCounter", 0);
    }
}
