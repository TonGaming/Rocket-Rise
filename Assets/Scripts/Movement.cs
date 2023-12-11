using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float moveAmount = 10f;
    [SerializeField] float rotationAmount = 20f;
    float rotationSides = 1f;

    Rigidbody rocketRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        // Thrusting
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rocketRigidbody.AddRelativeForce(Vector3.up * moveAmount, ForceMode.Force);
        } 
    }

    void ProcessRotation()
    {
        // Rotate on the right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rocketRigidbody.AddTorque(Vector3.forward * rotationAmount, ForceMode.Force);
        }
        // Rotate on the left
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rocketRigidbody.AddTorque(Vector3.forward * -1 * rotationAmount, ForceMode.Force);
        }
    }
}
