using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float moveAmount = 10f;
    [SerializeField] float rotationAmount = 20f;

    Rigidbody rocketRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
    }

    void ProcessMovement()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rocketRigidbody.AddRelativeForce(Vector3.up * moveAmount, ForceMode.Force);
        } 
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rocketRigidbody.AddTorque(0, 0, rotationAmount, ForceMode.Force);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rocketRigidbody.AddTorque(0, 0, -1 * rotationAmount, ForceMode.Force);
        }
    }
}
