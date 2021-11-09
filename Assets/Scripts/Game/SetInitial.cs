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
        //Set duckCount = 0 at start
        PlayerPrefs.SetFloat("duckCount", 0);
    }
}

