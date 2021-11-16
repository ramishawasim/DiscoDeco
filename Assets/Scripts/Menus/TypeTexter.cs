using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeTexter : MonoBehaviour
{
    public float letterPause = 0.2f;
    // public AudioClip typeSound1;
    // public AudioClip typeSound2;
    string cow;
    string message;
    TMP_Text textComp;
    
    // Use this for initialization
    void OnEnable() 
    {
        cow = PlayerPrefs.GetString("cowText");
        textComp = this.GetComponent<TMP_Text>();
        message = textComp.text + " \"" + cow[cow.Length - 1] + "\"";
        
        Debug.Log(message);
        textComp.text = "";
        StartCoroutine(TypeText());
        this.gameObject.SetActive(false);
    }

    IEnumerator TypeText() {
        foreach (char letter in message.ToCharArray()) {
            textComp.text += "HELLOW";
            // if (typeSound1 && typeSound2)
            //     SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
            //     yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }
}
