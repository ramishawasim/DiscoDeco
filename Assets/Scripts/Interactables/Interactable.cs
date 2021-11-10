using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactableRadius = 2.5f;
    public float pickUpRadius = 1;

    private GameObject hands;
    private bool hasItem;

    void Start()
    {
        hasItem = false;
        hands = GameObject.Find("HoldObjectPosition");
    }

    void Update()
    {
        if (hasItem == false) {
            //Within a specific range, activate interactable glow
            Collider[] interactableColliders = Physics.OverlapSphere(transform.position, interactableRadius);
            foreach (var interactableCollider in interactableColliders)
            {
                if (interactableCollider.tag == "Player")
                {
                    Debug.Log("Within Interact Range! ~~ Turn On Glow ~~");
                }
            }

            //Within a specific range, pick up item
            Collider[] pickUpColliders = Physics.OverlapSphere(transform.position, pickUpRadius);
            foreach (var pickUpCollider in pickUpColliders)
            {
                if (pickUpCollider.tag == "Player")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hasItem = true;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            dropObject();
            hasItem = false;
        }
    }

    void dropObject()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;  
        this.GetComponent<BoxCollider>().enabled = true;
    }
}
