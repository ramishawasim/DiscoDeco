using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLights : MonoBehaviour
{
    public Vector3 center;
    [SerializeReference]
    public float rotationMult;
    public float spotAngleMult;
    public GameObject baseLightLocation;
    private Light light;

    private void Start()
    {
        center = baseLightLocation.transform.position;
        light = GetComponent<Light>();

    }
    void Update()
    {
        transform.RotateAround(center, Vector3.up, 135 * Time.deltaTime * rotationMult);
        light.spotAngle = (15f * Mathf.Sin(Time.time * 3) * spotAngleMult) + 45f;
    }
}
