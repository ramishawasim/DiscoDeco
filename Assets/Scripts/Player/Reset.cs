using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Attach to Player
//Player Reset functionality
public class Reset : MonoBehaviour
{
    public float threshold = -50f;
    public Animator animator;

    private void Start()
    {
        animator = animator.GetComponent<Animator>();  
    }

    void Update()
    {
        if (transform.position.y < threshold)
        {
            animator.SetTrigger("onDeath");
            StartCoroutine(Wait());
            reset();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit player)
    {
        if (player.gameObject.tag == "Enemies")
        {
            animator.SetTrigger("onDeath");
            StartCoroutine(Wait());
            reset();
        }
    }

    void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }
}
