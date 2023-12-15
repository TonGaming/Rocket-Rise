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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rocketRigidbody.AddRelativeForce(Vector3.up * moveAmount, ForceMode.Force);
            if (gameAudio.GetSounding() == false)
            {
                // khi ấn W và k có âm thanh engine thì sẽ bật tiếng
                gameAudio.PlayEngineAudio();

            }
        }
        else if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.UpArrow) && gameAudio.GetSounding() == true)
        {
            // nếu k ấn W và đang có âm thanh Engine thì tắt tiếng engine
            gameAudio.StopAudio();

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

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Walls":

                levelManager.StartResetLevel();
                break;
            case "LandingPad":

                levelManager.StartLoadNextLevel();
                break;

        }
    }

    
}
