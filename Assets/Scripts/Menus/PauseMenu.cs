using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    // public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public static bool paused = false;
    PauseAction action;
    public GameObject button;

    // public GameObject pauseMenuUI;

    // Update is called once per frame

    private void Awake()
    {
        action = new PauseAction();
        
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }


    private void Start()
    {
        action.Pause.PauseGame.performed += _ => DeterminePause();
    }

    private void DeterminePause()
    {
        if (paused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        button.SetActive(true);
        Time.timeScale = 1f;
        paused = false;
        //button.SetActive(true);

    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        button.SetActive(false);
        // GameIsPaused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}
