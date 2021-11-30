using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStateHandler : MonoBehaviour
{

    private GameObject door;

    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward) * 100, Color.yellow);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Door")
            {
                GameObject door = hit.transform.gameObject;

                this.door = door;

            }
        }
    }

    public float DistanceFromDoor()
    {
        if (door == null) return Mathf.Infinity;

        return Vector3.Distance(transform.position, door.transform.position);
    }

    public bool IsFacingDoor()
    {
        return door != null;
    }

    public bool IsDoorBlocked()
    {
        if (door == null) return false;

        IsDoorBlocked isDoorBlocked = door.transform.parent.parent.GetComponent<IsDoorBlocked>();

        return isDoorBlocked.doorIsBlocked;
    }

    public void OpenDoor()
    {
        if (door == null) return;

        Interactable interactable = door.GetComponentInChildren<Interactable>();
        interactable.openDoor();
    }

    public void PlayBreakingSound()
    {
        door.transform.parent.parent.GetComponent<SmashDoorSound>().PlaySound();
    }

    public void BreakChairBlockingDoor()
    {
        foreach (Transform child in door.transform.parent.parent)
        {
            if (child.name == "blockChair")
            {
                GameObject blockChair = child.gameObject;
                blockChair.SetActive(false);
                door.transform.parent.parent.GetComponent<IsDoorBlocked>().doorIsBlocked = false;

                break;
            }
        }
    }

}
