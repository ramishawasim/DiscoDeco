using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Attach to empty game object in Start Menu
public class SetInitial : MonoBehaviour
{
    // Setting initial PlayerPrefs
    void Start()
    {
        PlayerPrefs.DeleteAll();
        //Set "isHolding" to 0 at start
        PlayerPrefs.SetInt("isHolding", 0);
        //Set "doorIsBlocked" to 0 at start
        PlayerPrefs.SetInt("doorIsBlocked", 0);
        //Set "notes" to 0 at start
        PlayerPrefs.SetInt("notes", 0);
    }
}

