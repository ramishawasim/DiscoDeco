using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dance : MonoBehaviour
{
    private InputActionReference movementControl;
    private GameObject playerAnimator;
    private Animator animator;
    private PlayerController playerController;
    private CharacterController characterController;
    private Transform cameraMainTransform;
    private float playerSpeed;
    private bool hasInteracted;

    void Start()
    {
        //for animation
        playerAnimator = GameObject.Find("Suit_Female");
        animator = playerAnimator.GetComponent<Animator>(); 

        playerController = this.GetComponent<PlayerController>();
        characterController = this.GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;
        hasInteracted = false;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (hasInteracted == false && PlayerPrefs.GetInt("isDead") == 0)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "DanceFloor")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hasInteracted = true;
                        PlayerPrefs.SetInt("isDancing", 1);
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
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("isDancing", 0);

        hasInteracted = false;
    }
}
