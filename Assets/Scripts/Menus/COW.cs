using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class COW : MonoBehaviour
{
    public GameObject TypeTexter;

    private int notes;

    void Start()
    {
        PlayerPrefs.SetString("cowText", "");
    }
    void Update()
    {
        this.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("cowText");
    }
}
