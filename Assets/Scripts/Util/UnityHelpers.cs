using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityHelpers : MonoBehaviour
{
    public static void DestroyObject(GameObject gameObject)
    {
        if (gameObject == null) return;

        Destroy(gameObject);
    }
}
