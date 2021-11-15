using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Keypad : MonoBehaviour
{
    public string curPassword = "2-6-9";
    public string input;
    public Text displayText;
    public AudioSource failAudio;

    private float btnClicked;
    private string[] inputArr = {"0", "0", "0"};

    void Start()
    {
        btnClicked = 0;
        inputArr[0] = "0";
        inputArr[1] = "0";
        inputArr[2] = "0";
    }
    
    void OnEnable()
    {
        btnClicked = 0;
        inputArr[0] = "0";
        inputArr[1] = "0";
        inputArr[2] = "0";
        displayText.text = getStringInput();
    }

    void Update()
    {
        displayText.text = getStringInput();
    }

    public void ValueEntered(string valueEntered)
    {
        switch (valueEntered)
        {
            case "1": case "2": case "3": case "4": case "5": case "6": case "7": case "8": case "9": case "0":
                updateArray(valueEntered);
                break;
            
            case "C":
                inputArr[0] = "0";
                inputArr[1] = "0";
                inputArr[2] = "0";
                break;
            
            case "E":
                input = getStringInput();
                if (input == curPassword)
                {
                    //Load the next scene
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                    Debug.Log("Correct Password!");
                    btnClicked = 0;
                }        
                else
                {
                    //Reset input varible
                    inputArr[0] = "0";
                    inputArr[1] = "0";
                    inputArr[2] = "0";
                    displayText.text = getStringInput();
                    failAudio.Play();
                    btnClicked = 0;
                }
                break;
            }
        }

    String getStringInput()
    {
        input = inputArr[0] + "-" + inputArr[1] + "-" + inputArr[2];
        return input;
    } 

    void updateArray(string newInput)
    {
        switch (btnClicked)
        {
            case 1:
                inputArr[0] = newInput;
                break;
            case 2:
                inputArr[1] = newInput;
                break;
            case 3:
                inputArr[2] = newInput;
                break;
            default:
                String temp1 = inputArr[1];
                String temp2 = inputArr[2];
                inputArr[0] = temp1;
                inputArr[1] = temp2;
                inputArr[2] = newInput;
                break;
        }
    }
}
