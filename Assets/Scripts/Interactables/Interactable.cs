using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactableRadius = 7.5f;
    public float pickUpRadius = 1;
    public int interactType;
    public GameObject other;

    private Material glowMaterial;
    private Color glowColour;
    private float glowDistance;

    private GameObject player;
    private GameObject camera;
    private GameObject hands;
    private GameObject snap;
    private GameObject keyPad;
    private GameObject playerAnimator;

    private bool hasInteracted;
    private Animator animator;
    private PlayerController playerController;
    private CharacterController characterController;

    public void Start()
    {
        hasInteracted = false;
        glowMaterial = GetComponent<Renderer>().material;

        player = GameObject.Find("Player");
        camera = GameObject.Find("ThirdPersonCamera");

        hands = GameObject.Find("HoldObjectPosition");
        snap = GameObject.Find("bench1_pillow1");
        keyPad = other;
        playerController = player.GetComponent<PlayerController>();
        characterController = player.GetComponent<CharacterController>();

        playerAnimator = GameObject.Find("Suit_Female");
        animator = playerAnimator.GetComponent<Animator>();  
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
                    glowMaterial.SetColor("_EmissionColor", new Vector4(glowColour.r,glowColour.g,glowColour.b,0) * glowDistance);
                    // glowMaterial.SetColor("_EmissionColor", Color.red);
                    Debug.Log(glowDistance);
                }
            }

            //Within a specific range, interact with item
            Collider[] pickUpColliders = Physics.OverlapSphere(transform.position, pickUpRadius);
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
                holdingObject();
                break;
            //block door type
            case 1:
                pushObject();
                break;
            //pick up note type
            case 2:
                pickUpNote();
                break;
            //keypad object type
            case 3:
                keypadObject();
                break;
            }
        }
    }

    void holdingObject()
    {
        // this.GetComponent<Rigidbody>().isKinematic = true;  
        this.GetComponent<MeshCollider>().enabled = false;
        this.transform.position = hands.transform.position;
        this.transform.parent = hands.transform.parent;
        animator.SetBool("onHold", true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            // this.GetComponent<Rigidbody>().isKinematic = false;  
            this.GetComponent<MeshCollider>().enabled = true;
            this.transform.position = snap.transform.position;
            this.transform.parent = snap.transform.parent;
            animator.SetBool("onHold", false);
            hasInteracted = false;
        }
    }

    void pushObject()
    {
        //push object into place
        //set door to locked
        animator.SetTrigger("onPush");
        hasInteracted = false;
    }

    void pickUpNote()
    {
        hasInteracted = false;
    }

    void keypadObject()
    {
        keyPad.SetActive(true);
        camera.SetActive(false);
        playerController.enabled = false;
        characterController.enabled = false;
        if (Input.GetKeyDown(KeyCode.E))
        {
            keyPad.SetActive(false);
            camera.SetActive(true);
            playerController.enabled = true;
            characterController.enabled = true;
            hasInteracted = false;
        }
    }
}
