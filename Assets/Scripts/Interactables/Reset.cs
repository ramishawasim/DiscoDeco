using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Attach to Player
//Player Reset functionality
public class Reset : MonoBehaviour
{
    public float threshold = -50f;

    void Update()
    {
        if (transform.position.y < threshold)
        {
            reset();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit player)
    {
        if (player.gameObject.tag == "Enemies")
        {
            reset();
        }
    }

    void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
