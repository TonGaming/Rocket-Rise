using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Engine Sound Effects")]
    [SerializeField] AudioClip engineThrustSFX;
    [SerializeField][Range(0, 1)] float engineVolume = 1f;


    Transform rocketTransform;
    AudioSource gameAudioSource;


    private bool isSounding;

    void Awake()
    {
        rocketTransform = FindObjectOfType<Player>().GetComponent<Transform>();
        gameAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckSounding();
        UpdateAudioPosition();
    }

    void CheckSounding()
    {
        if (gameAudioSource.isPlaying)
        {
            isSounding = true;
        }
        else if (!gameAudioSource.isPlaying)
        {
            isSounding = false;

        }
    }

    public bool GetSounding()
    {
        return isSounding;
    }

    // hàm dùng chung để bật âm thanh tuy nhiên khá là tù vì làm ntn thì k ngắt được âm thanh một khi đã bậts
    public void PlayAudioClipOnRocket(AudioClip clip, float volume)
    {
        
        Vector3 rocketPosition = rocketTransform.position;

        AudioSource.PlayClipAtPoint(clip, rocketPosition, volume);
    }

    // ngắt âm thanh
    public void StopAudio()
    {
        gameAudioSource.Stop();
    }
    // chạy âm thanh
    public void PlayEngineAudio()
    {
        //PlayAudioClipOnRocket(engineThrustSFX, engineVolume);
        gameAudioSource.PlayOneShot(engineThrustSFX, engineVolume);
    }

    // update vị trí của âm thanh theo vị trí của tên lửa
    void UpdateAudioPosition()
    {
        transform.position = rocketTransform.position;
    }

}
