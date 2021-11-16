using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : MonoBehaviour
{
    private GameObject playerAnimator;
    private Animator animator;
    private PlayerController playerController;
    private CharacterController characterController;
    private bool hasInteracted;

    void Start()
    {
        //for animation
        playerAnimator = GameObject.Find("Suit_Female");
        animator = playerAnimator.GetComponent<Animator>(); 

        playerController = this.GetComponent<PlayerController>();
        characterController = this.GetComponent<CharacterController>();
        hasInteracted = false;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (hasInteracted == false)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "DanceFloor")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hasInteracted = true;
                        playerController.enabled = false;
                        animator.SetBool("onWalk", false);
                        animator.SetTrigger("onDance");
                        StartCoroutine(WaitForAnimation(animator));
                    }
                }
            }
        }
    }

    IEnumerator WaitForAnimation(Animator anim)
    {
        // yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        yield return new WaitForSeconds(1.69f);
        playerController.enabled = true;
        hasInteracted = false;
    }
}
