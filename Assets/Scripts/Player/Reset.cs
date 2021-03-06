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
    private int deathCounter;

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
        deathCounter = PlayerPrefs.GetInt("deathCounter");
        PlayerPrefs.SetInt("deathCounter", deathCounter+1);
        Debug.Log(deathCounter+1);
        StartCoroutine(WaitForAnimation(animator));
    }

    IEnumerator WaitForAnimation(Animator anim)
    {
        playerController.enabled = false;
        characterController.enabled = false;
        PlayerPrefs.SetInt("isDead", 1);
        animator.SetTrigger("onDeath");
        keypad.SetActive(false);

        // yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        yield return new WaitForSeconds(1.69f);
        reset();
    }
}
