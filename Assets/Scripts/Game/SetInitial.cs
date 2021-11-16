using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Attach to empty game object in Start Menu
public class SetInitial : MonoBehaviour
{
    private GameObject[] getCount;
    // Setting initial PlayerPrefs
    void Start()
    {
        PlayerPrefs.DeleteAll();
        //Set "isHolding" to 0 at start
        PlayerPrefs.SetInt("isHolding", 0);
        //Set "isHiding" to 0 at start
        PlayerPrefs.SetInt("isHiding", 0);
        //Set "notes" to 0 at start
        PlayerPrefs.SetInt("notes", 0);
        //Set "cowText" to "" at start
        PlayerPrefs.SetString("cowText", "");
        //Set "chairCount" to getCount.Length
        getCount = GameObject.FindGameObjectsWithTag("Chair");
        PlayerPrefs.SetInt("chairCount", getCount.Length);
    }
}

