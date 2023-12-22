using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] float moveAmount = 10f;
    [SerializeField] float rotationAmount = 20f;

    [SerializeField] bool isDead = false;
    [SerializeField] bool isSuccess = false;


    Rigidbody rocketRigidbody;

    AudioManager gameAudio;
    LevelManager levelManager;



    private void Awake()
    {
        gameAudio = FindObjectOfType<AudioManager>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        rocketRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            ProcessThrust();
            ProcessRotation();

        }
        else if (isDead)
        {
            gameAudio.StopEngineAudio();
        }

    }

    void ProcessThrust()
    {
        // Thrusting
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // di chuyển
            rocketRigidbody.AddRelativeForce(Vector3.up * moveAmount, ForceMode.Force);

            if (gameAudio.GetSounding() == false && !isDead)
            {
                // khi ấn W và k có âm thanh engine thì sẽ bật tiếng
                gameAudio.PlayEngineAudio();

            }

        }
        else if ((!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.UpArrow) || isDead == true) 
            && gameAudio.GetSounding() == true)
        {
            // nếu k ấn W và đang có âm thanh Engine thì tắt tiếng engine
            gameAudio.StopEngineAudio();

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
            rocketRigidbody.AddTorque(Vector3.forward * -rotationAmount, ForceMode.Force);

        }

    }



    // Getter Setter
    public bool GetIsDeadStatus()
    {
        return isDead;
    }

    public void SetIsDeadStatus(bool isDeadValue)
    {
        isDead = isDeadValue;
    }

    public bool GetIsSuccessStatus()
    {
        return isSuccess;
    }

    public void SetIsSuccess(bool isSuccessValue)
    {
        isSuccess = isSuccessValue;
    }

}
