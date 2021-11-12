using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float interactableRadius = 2.5f;
    public float pickUpRadius = 1;

    private GameObject hands;
    private bool hasInteracted;

    void Start()
    {
        hasInteracted = false;
        hands = GameObject.Find("HoldObjectPosition");
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
                    Debug.Log("Within Interact Range! ~~ Turn On Glow ~~");
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
            holdingObject();
        }
    }

    void holdingObject()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;  
        this.GetComponent<BoxCollider>().enabled = false;
        this.transform.position = hands.transform.position;
        this.transform.parent = hands.transform;
        if (Input.GetKeyDown(KeyCode.E))
        {
            dropObject();
            hasInteracted = false;
        }
    }

    void dropObject()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;  
        this.GetComponent<BoxCollider>().enabled = true;
    }
}
