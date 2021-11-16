using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : MonoBehaviour
{
    public float range = 12f;
    private GameObject player;
    private GameObject playerAnimator;
    private Animator animator;
    private PlayerController playerController;
    private CharacterController characterController;

    void Start()
    {
        //for animation
        playerAnimator = GameObject.Find("Suit_Female");
        animator = playerAnimator.GetComponent<Animator>(); 

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        characterController = player.GetComponent<CharacterController>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Player")
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
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime - 0.2f);
        playerController.enabled = true;
        characterController.enabled = true;
    }
}
