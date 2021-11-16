using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Attach to Player
//Player Reset functionality
public class Reset : MonoBehaviour
{
    public GameObject keypad;
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

    // void Update()
    // {
    //     if (transform.position.y < threshold)
    //     {
    //         StartCoroutine(WaitForAnimation(animator));
    //     }
    // }

    // void OnControllerColliderHit(ControllerColliderHit player)
    // {
    //     if (player.gameObject.tag == "Enemies")
    //     {
    //         StartCoroutine(WaitForAnimation(animator));
    //     }
    // }

    void reset()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void Die()
    {
        StartCoroutine(WaitForAnimation(animator));
    }

    IEnumerator WaitForAnimation(Animator anim)
    {
        PlayerPrefs.SetInt("isDead", 1);
        animator.SetTrigger("onDeath");
        keypad.SetActive(false);
        playerController.enabled = false;
        characterController.enabled = false;
        // yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        yield return new WaitForSeconds(1.69f);
        reset();
    }
}
