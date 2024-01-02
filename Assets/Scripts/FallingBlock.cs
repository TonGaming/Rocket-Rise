using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] float dropDelay = 2f;
    [SerializeField] Light trapsLightSource;

    Rigidbody trapsRigidbody;

    AudioManager gameAudio;
    

    private void Awake()
    {
        gameAudio = FindObjectOfType<AudioManager>();
        trapsRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        trapsRigidbody.useGravity = false;
    }

    private void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine( StartCrumblingRocks());
            Debug.Log("đã chạm vào player rồi");
        } 
    }

    IEnumerator StartCrumblingRocks()
    {
        // bật âm thanh đá rơi
        gameAudio.PlayRocksAudio();

        // chuyển màu cái đèn đá
        trapsLightSource.color = Color.red;

        yield return new WaitForSecondsRealtime(dropDelay);

        ActivateFallingTraps();
    }

    private void ActivateFallingTraps()
    {
        trapsRigidbody.useGravity = true;
    }

    public Vector3 GetRocksPosition()
    {
        return transform.position;
    }
}
