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
    }

    void FrameUpdate()
    {
        if (hasInteracted == false)
        {
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
        frames++;
        if (frames == 20)
        { //If the remainder of the current frame divided by 10 is 0 run the function.
            frames = 0;
            FrameUpdate();
        }

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
                //pick up object type
                case 0:
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
                //place chair type
                case 7:
                    placeChair();
                    break;
                //pick up block chair type
                case 8:
                    pickUpBlockedChair();
                    break;
            }
        }
    }

    void pickUpObject()
    {
        isHolding = PlayerPrefs.GetInt("isHolding");
        getCount = GameObject.FindGameObjectsWithTag ("Chair");

        if (isHolding == 0 && getCount.Length == PlayerPrefs.GetInt("chairCount"))
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

    void blockDoor()
    {
        isHolding = PlayerPrefs.GetInt("isHolding");
        getCount = GameObject.FindGameObjectsWithTag ("Chair");
        doorIsBlocked = parent.GetComponent<IsDoorBlocked>().doorIsBlocked;
        
        if (isHolding == 1 && doorIsClosed && !doorIsBlocked && getCount.Length == PlayerPrefs.GetInt("chairCount"))
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
        sound.Play();
        parent.transform.Find("pivot").gameObject.SetActive(false);
        parent.transform.Find("original").gameObject.SetActive(true);
        doorIsClosed = true;
        hasInteracted = false;
    }

    void pickUpNote()
    {
        animator.SetTrigger("onPickUp");
        notes = PlayerPrefs.GetInt("notes");
        PlayerPrefs.SetInt("notes", notes+1);
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
        isHiding = PlayerPrefs.GetInt("isHiding");
        isHolding = PlayerPrefs.GetInt("isHolding");
        if(isHiding == 0 && isHolding == 0)
        {
            playerController.enabled = false;
            characterController.enabled = false;
            animator.SetBool("onWalk", false);
            
            Vector3 inStandP = parent.transform.Find("insidePlacement").gameObject.transform.position;
            Quaternion inStandR = parent.transform.Find("insidePlacement").gameObject.transform.rotation;
            player.transform.position = inStandP;
            player.transform.rotation = inStandR;
            player.tag = "Hide";

            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 outStand = parent.transform.Find("outsidePlacement").gameObject.transform.position;
                player.transform.position = outStand;
                
                player.tag = "Player";
                playerController.enabled = true;
                characterController.enabled = true;

                hasInteracted = false;
            }
        }
    }

    void placeChair()
    {
        isHolding = PlayerPrefs.GetInt("isHolding");
        getCount = GameObject.FindGameObjectsWithTag ("Chair");

        if (isHolding == 1 && parent.transform.Find("InteractableBase").gameObject.activeSelf && getCount.Length == PlayerPrefs.GetInt("chairCount"))
        {
            PlayerPrefs.SetInt("isHolding", 0);
            animator.SetBool("onHold", false);

            player.transform.Find("HoldChair").gameObject.SetActive(false);
            parent.transform.Find("InteractableBase").gameObject.SetActive(false);
            parent.transform.Find("chair").gameObject.SetActive(true);
        }
        hasInteracted = false;
    }

    void pickUpBlockedChair()
    {
        isHolding = PlayerPrefs.GetInt("isHolding");
        getCount = GameObject.FindGameObjectsWithTag ("Chair");
        doorIsBlocked = parent.GetComponent<IsDoorBlocked>().doorIsBlocked;
        
        if (isHolding == 0 && doorIsClosed && doorIsBlocked && getCount.Length == PlayerPrefs.GetInt("chairCount"))
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