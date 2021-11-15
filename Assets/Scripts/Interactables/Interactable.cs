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

    private Material glowMaterial;
    private Color glowColour;
    private float glowDistance;

    private GameObject player;
    private GameObject camera;
    private GameObject doorBlocks;
    // private GameObject hands;
    // private GameObject snap;

    private GameObject playerAnimator;
    private Animator animator;
    private PlayerController playerController;
    private CharacterController characterController;

    private bool hasInteracted;
    private int isHolding;
    private bool doorIsClosed;
    public bool doorIsBlocked;
    private int notes;

    public void Start()
    {
        hasInteracted = false;

        //glow
        glowMaterial = GetComponent<Renderer>().material;

        //for enabling/disabling
        player = GameObject.Find("Player");
        camera = GameObject.Find("ThirdPersonCamera");
        doorBlocks = GameObject.Find("DoorBlocks");
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
    }

    void Update()
    {
        if (hasInteracted == false) {
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
            //pick up object type
            case 0:
                if (isHolding == 0)
                pickUpObject();
                break;
            //block door type
            case 1:
                blockDoor();
                break;
            //open door type
            case 2:
                openDoor();
                break;
            //close door type
            case 3:
                closeDoor();
                break;
            //pick up note type
            case 4:
                pickUpNote();
                break;
            //keypad object type
            case 5:
                keypadObject();
                break;
            //hide type
            case 6:
                hide();
                break;
            //hide type
            case 7:
                placeChair();
                break;
            }
        }
    }

    void pickUpObject()
    {
        // this.GetComponent<MeshCollider>().enabled = false;
        // this.transform.position = hands.transform.position;
        // this.transform.parent = hands.transform.parent;
        isHolding = PlayerPrefs.GetInt("isHolding");
        if (isHolding == 0)
        {
            player.transform.Find("HoldChair").gameObject.SetActive(true);
            PlayerPrefs.SetInt("isHolding", 1);
            
            // doorBlocks.gameObject.SetActive(true);
            animator.SetBool("onHold", true);

            parent.transform.Find("chair").gameObject.SetActive(false);
            parent.transform.Find("InteractableBase").gameObject.SetActive(true);

            hasInteracted = false;
        }
        hasInteracted = false;

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     this.GetComponent<MeshCollider>().enabled = true;
        //     this.transform.position = snap.transform.position;
        //     this.transform.parent = snap.transform.parent;
        //     animator.SetBool("onHold", false);
        //     hasInteracted = false;
        // }
    }
    
    void blockDoor()
    {
        //push object into place
        //set door to locked
        // animator.SetTrigger("onPush");
        isHolding = PlayerPrefs.GetInt("isHolding");
        if (isHolding == 1 && doorIsClosed && !doorIsBlocked)
        {
            parent.transform.Find("blockChair").gameObject.SetActive(true);
            parent.transform.Find("original").gameObject.SetActive(true);
            parent.transform.Find("pivot").gameObject.SetActive(false);

            player.transform.Find("HoldChair").gameObject.SetActive(false);

            PlayerPrefs.SetInt("isHolding", 0);
            doorIsBlocked = true;

            animator.SetBool("onHold", false);
            hasInteracted = false;
        }
        else
        {
            parent.transform.Find("block/BlockDoorIndicator/BlockChair").gameObject.SetActive(false);
            player.transform.Find("HoldChair").gameObject.SetActive(true);
            PlayerPrefs.SetInt("isHolding", 1);
            doorIsBlocked = false;

            animator.SetBool("onHold", true);
            hasInteracted = false;
        }
    }

    void openDoor()
    {
        if (!doorIsBlocked){
            parent.transform.Find("original").gameObject.SetActive(false);
            parent.transform.Find("pivot").gameObject.SetActive(true);
            doorIsClosed = false;
            hasInteracted = false;
        }
    }

    void closeDoor()
    {
        parent.transform.Find("original").gameObject.SetActive(true);
        parent.transform.Find("pivot").gameObject.SetActive(false);
        doorIsClosed = true;
        hasInteracted = false;
    }

    void pickUpNote()
    {
        notes = PlayerPrefs.GetInt("notes");
        PlayerPrefs.SetInt("notes", notes+1);
        Debug.Log("Notes" + (notes+1).ToString());
        hasInteracted = false;
        this.gameObject.SetActive(false);
    }

    void keypadObject()
    {
        keypad.SetActive(true);
        camera.SetActive(false);
        playerController.enabled = false;
        characterController.enabled = false;
        if (Input.GetKeyDown(KeyCode.E))
        {
            keypad.SetActive(false);
            camera.SetActive(true);
            playerController.enabled = true;
            characterController.enabled = true;
            hasInteracted = false;
        }
    }

    void hide()
    {
        hasInteracted = false;
    }
    
    void placeChair()
    {
        isHolding = PlayerPrefs.GetInt("isHolding");
        if (isHolding == 1 && parent.transform.Find("InteractableBase").gameObject.activeSelf)
        {
            player.transform.Find("HoldChair").gameObject.SetActive(false);
            PlayerPrefs.SetInt("isHolding", 0);
            
            animator.SetBool("onHold", false);
            
            parent.transform.Find("chair").gameObject.SetActive(true);
            parent.transform.Find("InteractableBase").gameObject.SetActive(false);
            hasInteracted = false;
        }
        hasInteracted = false;
    }

    void Victory()
    {
        notes = PlayerPrefs.GetInt("notes");
        if (notes == 3){
            Debug.Log("Victory");
        }
    }

}
