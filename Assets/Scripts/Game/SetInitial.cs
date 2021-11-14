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
        //Set "isHolding" to 1 at start
        PlayerPrefs.SetInt("isHolding", 0);
    }
}

