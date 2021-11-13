using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLights : MonoBehaviour
{
    public Vector3 center;
    [SerializeReference]
    public float rotationMult;
    private Light light;

    private void Start()
    {
        center = transform.position;
        light = GetComponent<Light>();

    }
    void Update()
    {
        transform.RotateAround(center, Vector3.up, 135 * Time.deltaTime * rotationMult);
        // transform.RotateAround(center, Vector3.right, 75 * Time.deltaTime);
        light.spotAngle = (15f * Mathf.Sin(Time.time * 3)) + 65f;
    }
}
