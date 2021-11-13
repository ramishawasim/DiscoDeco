using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooController : MonoBehaviour
{
    public GameObject audioOne;

    private void Start()
    {
        audioOne.SetActive(false);
        Invoke("callMooLogic", 2f);
    }

    void callMooLogic()
    {
            callMoo();
            float rand = Random.Range(3.5f, 9f);
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

}
