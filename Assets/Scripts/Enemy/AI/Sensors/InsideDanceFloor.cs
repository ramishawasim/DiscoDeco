using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideDanceFloor : MonoBehaviour
{
    private bool canDance;
    private bool busy;

    public void Update()
    {
        if (PlayerPrefs.GetInt("isDancing") == 0)
        {
            busy = false;
        }

        if (!busy)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
            {
                if (hit.transform.tag != "DanceFloor" && PlayerPrefs.GetInt("isDancing") == 1)
                {
                    busy = true;
                    canDance = false;
                }
                else if (hit.transform.tag == "DanceFloor" && PlayerPrefs.GetInt("isDancing") == 1)
                {
                    canDance = true;
                }
                else
                {
                    canDance = false;
                }
            }
        }
        else
        {
            canDance = false;
        }
    }

    public bool CanDance()
    {
        return canDance;
    }
}
