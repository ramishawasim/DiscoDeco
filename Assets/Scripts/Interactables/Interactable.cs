using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Interactable : MonoBehaviour
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

        //objects
        // hands = GameObject.Find("HoldObjectPosition");
        // snap = GameObject.Find("bench1_pillow1");

        keypad = keypad;
        parent = parent;
        doorIsClosed = true;
        doorIsBlocked = false;

        InvokeRepeating("DitherEffect", 0f, 0.2f);
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
                    if (Input.GetKeyDown(KeyCode.E))
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
                case 0:
                    openDoor();
                    break;
                //close door type
                case 1:
                    closeDoor();
                    break;
                //pick up note type
                case 2:
                    pickUpNote();
                    break;
                //keypad object type
                case 3:
                    keypadObject();
                    break;
                //hide type
                case 4:
                    hide();
                    break;
            }
        }
    }

    public void openDoor()
    {
        doorIsBlocked = parent.GetComponent<IsDoorBlocked>().doorIsBlocked;
        if (!doorIsBlocked && !parent.transform.Find("blockChair").gameObject.activeSelf)
        {
            sound.Play();
            parent.transform.Find("original").gameObject.SetActive(false);
            parent.transform.Find("pivot").gameObject.SetActive(true);
            doorIsClosed = false;
            hasInteracted = false;
        }
    }

    void closeDoor()
    {
        doorIsBlocked = parent.GetComponent<IsDoorBlocked>().doorIsBlocked;
        if (!doorIsBlocked && !parent.transform.Find("blockChair").gameObject.activeSelf)
        {
            sound.Play();
            parent.transform.Find("pivot").gameObject.SetActive(false);
            parent.transform.Find("original").gameObject.SetActive(true);
            doorIsClosed = true;
            hasInteracted = false;
        }
    }

    void pickUpNote()
    {
        animator.SetTrigger("onPickUp");
        notes = PlayerPrefs.GetInt("notes");
        PlayerPrefs.SetInt("notes", notes + 1);
        switch (PlayerPrefs.GetInt("notes"))
        {
            case 1:
                PlayerPrefs.SetString("cowText", "C");
                break;
            case 2:
                PlayerPrefs.SetString("cowText", "CO");
                break;
            case 3:
                PlayerPrefs.SetString("cowText", "COW");
                break;
        }
        // noteMessage.gameObject.SetActive(true);

        hasInteracted = false;
        this.gameObject.SetActive(false);
    }

    void keypadObject()
    {
        isDead = PlayerPrefs.GetInt("isDead");
        if (isDead == 0)
        {
            keypad.SetActive(true);
            camera.SetActive(false);
            playerController.enabled = false;
            // characterController.enabled = false;
            if (Input.GetKeyDown(KeyCode.E))
            {
                keypad.SetActive(false);
                camera.SetActive(true);
                playerController.enabled = true;
                characterController.enabled = true;
                hasInteracted = false;
            }
        }
    }

    void hide()
    {
        isHolding = PlayerPrefs.GetInt("isHolding");
        if (isHolding == 1)
        {
            hasInteracted = false;
        }
        else if (isHolding == 0)
        {
            playerController.enabled = false;
            characterController.enabled = false;
            animator.SetBool("onWalk", false);

            Vector3 inStandP = parent.transform.Find("insidePlacement").gameObject.transform.position;
            Quaternion inStandR = parent.transform.Find("insidePlacement").gameObject.transform.rotation;
            player.transform.position = inStandP;
            player.transform.rotation = inStandR;
            PlayerPrefs.SetInt("isHiding", 1);
            player.tag = "Hide";

            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 outStand = parent.transform.Find("outsidePlacement").gameObject.transform.position;
                player.transform.position = outStand;

                player.tag = "Player";
                playerController.enabled = true;
                characterController.enabled = true;
                PlayerPrefs.SetInt("isHiding", 0);

                hasInteracted = false;
            }
        }
    }
}