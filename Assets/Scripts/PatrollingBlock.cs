using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingBlock : MonoBehaviour
{
    Vector3 startingPosition;
    [Header("Patrolling distance on X axis")]
    [SerializeField] bool isX;
    // khoảng cách sẽ di chuyển
    [SerializeField] Vector3 movementVectorX = new Vector3(0, 0, 0);

    [Header("Patrolling distance on Y axis")]
    [SerializeField] bool isY;
    [SerializeField] Vector3 movementVectorY = new Vector3(0, 0, 0);

    [Header("Patrolling distance on Z axis")]
    [SerializeField] bool isZ;
    [SerializeField] Vector3 movementVectorZ = new Vector3(0, 0, 0);

    [Header("Moved Percent 1 means full movement distance")]
    [SerializeField][Range(0, 1)] float movementFactor;


    Vector3 offset;

    void Start()
    {
        // vị trí bắt đầu di chuyển 
        startingPosition = transform.position;
    }

    void Update()
    {
        if (isX)
        {
            HorizontalMovement();
        }
        else if (isY)
        {
            VerticalMovement();
        }
        else if (isZ)
        {
            FrontMovement();
        }

    }

    void FrontMovement()
    {
        offset = movementVectorZ * movementFactor;

        transform.position = offset + startingPosition;
    }

    void HorizontalMovement()
    {
        offset = movementFactor * movementVectorX;

        transform.position = offset + startingPosition;
    }

    void VerticalMovement()
    {
        // khoảng cách di chuyển được = hướng di chuyển nhân với tốc độ di chuyển
        offset = movementVectorY * movementFactor;

        // vị trí mới sau khi di chuyển = khoảng cách sau khi di chuyển được + với vị trí ban đầu
        transform.position = offset + startingPosition;
    }
}
