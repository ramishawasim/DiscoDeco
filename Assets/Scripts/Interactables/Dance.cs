using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : MonoBehaviour
{
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
        //Within a specific range, interact with item
        Collider[] danceColliders = Physics.OverlapSphere(transform.position, 1);
        foreach (var danceCollider in danceColliders)
        {
            if (danceCollider.tag == "Player")
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
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        playerController.enabled = true;
        characterController.enabled = true;
    }
}
