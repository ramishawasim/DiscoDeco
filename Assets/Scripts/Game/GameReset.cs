using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach to Empty GameObject to reset PlayerPrefs and other properties on Game reset
//Also Attched to Start Menu
//Currently found in UI -> GameReset
//With this logic, we can now use "PlayerPrefs" to save game info between Scenes
public class GameReset : MonoBehaviour
{
    private static bool spawned = false;
    void Awake()
    {
        if(spawned == false)
        {
            spawned = true;
            DontDestroyOnLoad(gameObject);
            PlayerPrefs.DeleteAll();
        }
        else
        {
            DestroyImmediate(gameObject); //This deletes the new objects
        }
    }
}