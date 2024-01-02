
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Engine Sound Effects")]
    [SerializeField] AudioClip engineThrustSFX;
    [SerializeField][Range(0, 1)] float engineVolume = 1f;

    [Header("Landing Pad Success SFX")]
    [SerializeField] AudioClip successSFX;
    [SerializeField][Range(0, 1)] float successVolume = 1f;

    [Header("Explosion SFX")]
    [SerializeField] AudioClip explosionSFX;
    [SerializeField][Range(0, 1)] float explosionVolume = 1f;

    [Header("Crumbling Rocks SFX")]
    [SerializeField] AudioClip rocksSFX;
    [SerializeField][Range(0, 1)] float rocksVolume = 1;


    Transform rocketTransform;

    AudioSource gameAudioSource;

    [Header("Locations")]
    [SerializeField] GameObject landingPad;

    private bool isSounding;

    void Awake()
    {
        rocketTransform = FindObjectOfType<Player>().GetComponent<Transform>();

        gameAudioSource = GetComponent<AudioSource>();

        
    }

    void Start()
    {

    }

    void Update()
    {
        UpdateAudioPosition();
    }

    // Get trạng thái biến bool isSounding
    public bool GetSounding()
    {
        return isSounding;
    }

    // hàm dùng chung để bật âm thanh tuy nhiên khá là tù vì làm ntn thì k ngắt được âm thanh một khi đã bậts
    private void PlayAudioClip(AudioClip clip, Vector3 audioLocation, float volume)
    {
        // không thể huỷ PlayClipAtPoint một khi đã chạy 
        AudioSource.PlayClipAtPoint(clip, audioLocation, volume);
    }

    // update vị trí của âm thanh theo vị trí của tên lửa
    void UpdateAudioPosition()
    {
        gameAudioSource.transform.position = rocketTransform.position;
    }

    // ngắt âm thanh động cơ tên lửa
    public void StopEngineAudio()
    {
        if (gameAudioSource.isPlaying)
        {
            gameAudioSource.Stop();
        }
    }

    // chạy âm thanh tên lửa
    public void PlayEngineAudio()
    {
        if (!gameAudioSource.isPlaying)
        {
            //PlayAudioClip(engineThrustSFX, engineVolume);
            gameAudioSource.PlayOneShot(engineThrustSFX, engineVolume);
        }
    }

    // hàm chạy âm thanh success
    public void PlaySuccessAudio()
    {
        PlayAudioClip(successSFX, landingPad.transform.position, successVolume);
    }

    // chạy âm thanh nổ
    public void PlayExplosionAudio()
    {
        PlayAudioClip(explosionSFX, rocketTransform.position, explosionVolume);
    }

    // chạy âm thanh đá rơi
    public void PlayRocksAudio()
    {
        PlayAudioClip(rocksSFX, rocketTransform.position, rocksVolume);
    }


}
