using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement Section")]
    [SerializeField] float moveAmount = 10f;
    [SerializeField] float rotationAmount = 20f;

    [Header("Status Section")]
    [SerializeField] bool isDead = false;
    [SerializeField] bool isSuccess = false;

    [Header("Particles Effects Section")]
    [SerializeField] ParticleSystem mainEnginePE;
    [SerializeField] ParticleSystem leftSidePE;
    [SerializeField] ParticleSystem rightSidePE;
    [SerializeField] ParticleSystem explosionPE;
    [SerializeField] ParticleSystem successPE;



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

            // bật Particle Effects
            mainEnginePE.Play();

            // bật âm thanh
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

            // Dừng PE lại khi nhả phím 
            mainEnginePE.Stop();
        }
    }

    void ProcessRotation()
    {
        // Rotate on the right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Xử lý quay phải
            rocketRigidbody.AddTorque(Vector3.forward * rotationAmount, ForceMode.Force);

            // Bật Particle Effects bên trái (đẩy sang phải)
            leftSidePE.Play();
        }
        else if (!Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.RightArrow))
        {
            leftSidePE.Stop();
        }
        // Rotate on the left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rocketRigidbody.AddTorque(Vector3.forward * -rotationAmount, ForceMode.Force);

            rightSidePE.Play();
        }
        else if (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.LeftArrow))
        {
            rightSidePE.Stop();
        }

    }

    public void ActivateSuccessPE()
    {
        successPE.Play();
    }

    public void ActivateExplosionPE()
    {
        explosionPE.Play();
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
