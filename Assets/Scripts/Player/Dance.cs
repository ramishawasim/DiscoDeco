using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : MonoBehaviour
{
    private GameObject playerAnimator;
    private Animator animator;
    private PlayerController playerController;
    private CharacterController characterController;

    void Start()
    {
        //for animation
        playerAnimator = GameObject.Find("Suit_Female");
        animator = playerAnimator.GetComponent<Animator>(); 

        playerController = this.GetComponent<PlayerController>();
        characterController = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "DanceFloor")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerController.enabled = false;
                    characterController.enabled = false;
                    animator.SetTrigger("onDance");
                    StartCoroutine(WaitForAnimation(animator));
                }
            }
        }
    }

    IEnumerator WaitForAnimation(Animator anim)
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        playerController.enabled = true;
        characterController.enabled = true;
    }
}
