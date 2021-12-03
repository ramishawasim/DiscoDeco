using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNotes : MonoBehaviour
{
    private int random_int;
    // Start is called before the first frame update
    void Start()
    {
        random_int = Random.Range(0,9);
        this.transform.Find($"KeypadNote {random_int}").gameObject.SetActive(true);
    }
}
