using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooController : MonoBehaviour
{
    public GameObject audioOne;
    public GameObject audioTwo;

    private void Start()
    {
        audioOne.SetActive(false);
        Invoke("callMooLogic", 2f);
    }

    void callMooLogic()
    {
        int r = Random.Range(0, 3);
        float rand = Random.Range(3.5f, 7.5f);

        if (r == 0)
        {
            callMoo();
        }

        else
        {
            callGrumble();
        }

        Invoke("callMooLogic", rand);

    }

    void callMoo()
    {
        audioOne.SetActive(true);
        Invoke("endMoo", 3f);
    }

    void endMoo()
    {
        audioOne.SetActive(false);
    }

    void callGrumble()
    {
        audioTwo.SetActive(true);
        Invoke("endGrumble", 3f);
    }

    void endGrumble()
    {
        audioTwo.SetActive(false);
    }



}
