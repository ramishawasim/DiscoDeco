using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFieldOfView : MonoBehaviour
{
    public float radius;

    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;

        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position + Vector3.up, radius, targetMask);

        if (rangeChecks.Length != 0)
        {

            Debug.Log("ha");

            Transform target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward * -1, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position + Vector3.up, directionToTarget, distanceToTarget, obstructionMask))
                {
                    Debug.Log(1);
                    canSeePlayer = true;
                }
                else
                {
                    Debug.Log(2);
                    canSeePlayer = false;
                }
            }
            else
            {
                Debug.Log(3);
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            Debug.Log(4);
            canSeePlayer = false;
        }
    }
}
