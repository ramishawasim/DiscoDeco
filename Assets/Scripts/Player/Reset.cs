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
    private PlayerController playerController;
    private CharacterController characterController;

    private void Start()
    {
        animator = animator.GetComponent<Animator>();  
        playerController = this.GetComponent<PlayerController>();
        characterController = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (transform.position.y < threshold)
        {
            animator.SetTrigger("onDeath");
            StartCoroutine(WaitForAnimation(animator));
        }
    }

    void OnControllerColliderHit(ControllerColliderHit player)
    {
        if (player.gameObject.tag == "Enemies")
        {
            animator.SetTrigger("onDeath");
            StartCoroutine(WaitForAnimation(animator));
        }
    }

    void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator WaitForAnimation(Animator anim)
    {
        playerController.enabled = false;
        characterController.enabled = false;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        reset();
    }
}
