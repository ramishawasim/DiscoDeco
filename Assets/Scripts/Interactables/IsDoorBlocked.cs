using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDoorBlocked : MonoBehaviour
{
    public bool doorIsBlocked;
    void Awake()
    {
        doorIsBlocked = false;
    }
}
