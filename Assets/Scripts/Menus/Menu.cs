using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

