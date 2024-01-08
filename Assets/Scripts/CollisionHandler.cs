using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Status
    [SerializeField] bool godMode;

    // Scripts
    Player player;
    LevelManager levelManager;
    AudioManager gameAudio;


    private void Awake()
    {
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
        gameAudio = FindObjectOfType<AudioManager>();
    }

    // Setter dành cho godMode
    public void SetGodMode(bool GodModeValue)
    {
        godMode = GodModeValue;
    }

    // Xử lý sự kiện xảy ra khi có va chạm
    private void OnCollisionEnter(Collision collision)
    {
        if (godMode == false)
        {
            switch (collision.gameObject.tag)
            {
                case "Walls":
                    // nếu isDead = false (vẫn sống) và không bị thương thì -> cho isHurt = true
                    if (!player.GetIsDeadStatus() && !player.GetIsHurtStatus())
                    {
                        player.SetIsHurtStatus(true);

                        // bật âm thanh nổ tàu
                        gameAudio.PlayExplosionAudio();

                        // Chạy Particle Effects Explosion
                        player.ActivateExplosionPE();

                        Debug.Log("Đã bị thương");
                    }
                    // nếu isDead = false và bị thương(isHurt = true) thì cho chết
                    else if (!player.GetIsDeadStatus() && player.GetIsHurtStatus())
                    {
                        StartCrashSequence();

                        Debug.Log("đã chết");
                    }

                    break;

                case "LandingPad":

                    // nếu tàu vẫn chưa success và vẫn sống
                    if (!player.GetIsSuccessStatus() && !player.GetIsDeadStatus())
                    {
                        StartSuccessSquence();
                    }

                    break;

            }
        }
    }

    void StartSuccessSquence()
    {

        // bdau load level mới sau 2s
        levelManager.StartNextLevel();

        // tắt hết âm thanh đi để bật âm thanh success
        gameAudio.StopEngineAudio();

        // bật âm thanh qua màn
        gameAudio.PlaySuccessAudio();

        // chỉnh lại trạng thái isSuccess sau khi đã chạm vào LandingPad
        player.SetIsSuccess(true);

        // chỉnh lại các biến bool
        player.SetIsDeadStatus(false);

        // chỉnh lại các biến bool
        player.SetIsHurtStatus(false);

        // Chạy Particle Effects Explosion 
        player.ActivateSuccessPE();
    }

    void StartCrashSequence()
    {
        // reset level sau 1 khoảng tgian nhất định
        levelManager.StartResetLevel();

        // tắt hết âm thanh đi để bật âm thanh success
        gameAudio.StopEngineAudio();

        // bật âm thanh nổ tàu
        gameAudio.PlayExplosionAudio();

        // set trạng thái isDead thành true
        player.SetIsDeadStatus(true);

        // Chạy Particle Effects Explosion
        player.ActivateExplosionPE();
    }
}
