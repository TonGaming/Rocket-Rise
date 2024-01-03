using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] float dropDelay = 2f;
    [SerializeField] Light trapsLightSource;

    [SerializeField] float fallingForce = 10f;
    [SerializeField] bool isFalling;

    Rigidbody trapsRigidbody;
    BoxCollider trapsCollider;

    AudioManager gameAudio;


    private void Awake()
    {
        gameAudio = FindObjectOfType<AudioManager>();

        trapsRigidbody = GetComponent<Rigidbody>();
        trapsCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        trapsRigidbody.useGravity = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Đã nhận được player");

            RockSlide();

            isFalling = true;
        }
    }

    private void RockSlide()
    {
        StartCoroutine(StartCrumblingRocks());
    }


    IEnumerator StartCrumblingRocks()
    {
        if (isFalling == false)
        {

            // bật âm thanh đá rơi
            gameAudio.PlayRocksAudio();

            // chuyển màu cái đèn đá
            trapsLightSource.color = Color.red;

            yield return new WaitForSecondsRealtime(dropDelay);

            ActivateFallingTraps();


        }
    }

    private void ActivateFallingTraps()
    {
        trapsRigidbody.useGravity = true;
        trapsRigidbody.AddForce(Vector3.down * fallingForce, ForceMode.Impulse);
    }
}
