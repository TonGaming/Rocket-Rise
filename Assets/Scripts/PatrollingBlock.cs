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
    // tốc độ di chuyển hết khoảng cách đó 
    [SerializeField][Range(0, 1)] float movementFactor;

    // the time it takes to complete a sinewave(circle)
    [SerializeField] float period = 10f;

    float cycles;

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

    void CalculatingSineWave(Vector3 movementVector)
    {
        // số vòng đã lặp = thời gian đã trôi qua chia cho khoảng thời gian quay hết 1 vòng sine
        cycles = Time.time / period;

        // 1 tau = 2 Pi 
        float tau = Mathf.PI * 2;

        float rawSineWave = Mathf.Sin(cycles * tau);

        // để làm cho giá trị của movementFactor là từ 0 tới 1
        movementFactor = (rawSineWave + 1f) / 2f;

        // khoảng cách di chuyển sẽ bằng tốc độ * quãng đường 
        offset = movementVector * movementFactor;

        // vị trí mới sau khi di chuyển = khoảng cách sau khi di chuyển được + với vị trí ban đầu
        transform.position = offset + startingPosition;
    }

    void FrontMovement()
    {
        
        CalculatingSineWave(movementVectorZ);
    }

    void HorizontalMovement()
    {
        CalculatingSineWave(movementVectorX);


    }

    void VerticalMovement()
    {
        // khoảng cách di chuyển được = hướng di chuyển nhân với tốc độ di chuyển
        CalculatingSineWave(movementVectorY);


    }
}
