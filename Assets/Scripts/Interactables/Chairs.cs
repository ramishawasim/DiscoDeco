using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Chairs : MonoBehaviour
{
    public float interactableRadius = 10f;
    public float interactRadius = 1.5f;
    public int interactType;
    public GameObject keypad;
    public GameObject parent;
    public AudioSource sound;

    private Material glowMaterial;
    private Color glowColour;
    private float glowDistance;

    private GameObject player;
    private GameObject camera;
    public GameObject noteMessage;

    private GameObject playerAnimator;
    private Animator animator;
    private PlayerController playerController;
    private CharacterController characterController;

    private bool hasInteracted;
    private int isHolding;
    private int isHiding;
    private bool doorIsClosed;
    private bool doorIsBlocked;
    private int notes;
    private int isDead;
    private GameObject[] getCount;

    private int frames = 0;

    public void Start()
    {
        hasInteracted = false;

        //glow
        glowMaterial = GetComponent<Renderer>().material;

        //for enabling/disabling
        player = GameObject.Find("Player");
        camera = GameObject.Find("ThirdPersonCamera");

        playerController = player.GetComponent<PlayerController>();
        characterController = player.GetComponent<CharacterController>();

        //for animation
        playerAnimator = GameObject.Find("Suit_Female");
        animator = playerAnimator.GetComponent<Animator>();

        parent = parent;
        doorIsClosed = true;
        doorIsBlocked = false;

        InvokeRepeating("DitherEffect", 0f, 1.0f);
    }

    void DitherEffect()
    {
        if (hasInteracted == false)
        {
            glowMaterial.SetFloat("_DitherAlpha", 0);
            //Within a specific range, activate interactable glow
            Collider[] interactableColliders = Physics.OverlapSphere(transform.position, interactableRadius);
            foreach (var interactableCollider in interactableColliders)
            {
                if (interactableCollider.tag == "Player")
                {
                    float dist = Vector3.Distance(player.transform.position, this.transform.position);
                    glowDistance = 1 - (dist / (interactableRadius));
                    glowMaterial.SetFloat("_DitherAlpha", glowDistance);
                }
            }
        }
    }

    void Update()
    {
        doorIsBlocked = doorIsBlocked;

        if (hasInteracted == false)
        {
            //Within a specific range, interact with item
            Collider[] pickUpColliders = Physics.OverlapSphere(transform.position, interactRadius);
            foreach (var pickUpCollider in pickUpColliders)
            {
                if (pickUpCollider.tag == "Player")
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        hasInteracted = true;
                    }
                }
            }
        }
        else
        {
            switch (interactType)
            {
                //pick up object type
                case 0:
                    pickUpChair();
                    break;
                //place chair type
                case 1:
                    placeChair();
                    break;
                //block door type
                case 2:
                    blockDoor();
                    break;
                //pick up block chair type
                case 3:
                    pickUpBlockedChair();
                    break;
            }
        }
    }

    void pickUpChair()
    {
        isHolding = PlayerPrefs.GetInt("isHolding");
        getCount = GameObject.FindGameObjectsWithTag("Chair");

        if (isHolding == 0 && getCount.Length <= PlayerPrefs.GetInt("chairCount"))
        {
            PlayerPrefs.SetInt("isHolding", 1);
            animator.SetBool("onHold", true);
            try
            {
                parent.transform.Find("chair").gameObject.SetActive(false);
            }
            catch (Exception e)
            {
                Debug.Log("blockChair");
            }
            try
            {
                parent.transform.Find("blockChair").gameObject.SetActive(false);
            }
            catch (Exception e)
            {
                Debug.Log("chair");
            }

            player.transform.Find("HoldChair").gameObject.SetActive(true);
            parent.transform.Find("InteractableBase").gameObject.SetActive(true);
        }
        hasInteracted = false;
    }

    void placeChair()
    {
        isHolding = PlayerPrefs.GetInt("isHolding");
        getCount = GameObject.FindGameObjectsWithTag("Chair");

        if (isHolding == 1 && parent.transform.Find("InteractableBase").gameObject.activeSelf && getCount.Length <= PlayerPrefs.GetInt("chairCount"))
        {
            PlayerPrefs.SetInt("isHolding", 0);
            animator.SetBool("onHold", false);

            player.transform.Find("HoldChair").gameObject.SetActive(false);
            parent.transform.Find("InteractableBase").gameObject.SetActive(false);
            parent.transform.Find("chair").gameObject.SetActive(true);
        }
        hasInteracted = false;
    }

    void blockDoor()
    {
        isHolding = PlayerPrefs.GetInt("isHolding");
        getCount = GameObject.FindGameObjectsWithTag("Chair");
        doorIsBlocked = parent.GetComponent<IsDoorBlocked>().doorIsBlocked;

        if (isHolding == 1 && doorIsClosed && !doorIsBlocked && getCount.Length <= PlayerPrefs.GetInt("chairCount"))
        {
            sound.Play();
            parent.transform.Find("InteractableBase").gameObject.SetActive(false);
            player.transform.Find("HoldChair").gameObject.SetActive(false);
            parent.transform.Find("blockChair").gameObject.SetActive(true);

            parent.transform.Find("pivot").gameObject.SetActive(false);
            parent.transform.Find("original").gameObject.SetActive(true);

            PlayerPrefs.SetInt("isHolding", 0);
            parent.GetComponent<IsDoorBlocked>().doorIsBlocked = true;

            animator.SetBool("onHold", false);
        }
        hasInteracted = false;
    }

    void pickUpBlockedChair()
    {
        isHolding = PlayerPrefs.GetInt("isHolding");
        getCount = GameObject.FindGameObjectsWithTag("Chair");
        doorIsBlocked = parent.GetComponent<IsDoorBlocked>().doorIsBlocked;

        if (isHolding == 0 && doorIsClosed && doorIsBlocked && getCount.Length <= PlayerPrefs.GetInt("chairCount"))
        {
            parent.transform.Find("blockChair").gameObject.SetActive(false);
            parent.transform.Find("InteractableBase").gameObject.SetActive(true);

            player.transform.Find("HoldChair").gameObject.SetActive(true);
            PlayerPrefs.SetInt("isHolding", 1);
            parent.GetComponent<IsDoorBlocked>().doorIsBlocked = false;

            animator.SetBool("onHold", true);
        }
        hasInteracted = false;
    }
}
