using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    Player player;
    LevelManager levelManager;
    AudioManager gameAudio;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
        gameAudio = FindObjectOfType<AudioManager>();
    }

    // Collision Handler
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Walls":
                // nếu isDead = false
                if (!player.GetIsDeadStatus())
                {
                    // reset level sau 1 khoảng tgian nhất định
                    levelManager.StartResetLevel();

                    // bật âm thanh nổ tàu
                    gameAudio.PlayExplosionAudio();

                    // set trạng thái isDead thành true
                    player.SetIsDeadStatus(true);
                }

                break;

            case "LandingPad":

                // nếu tàu vẫn chưa success
                if (!player.GetIsSuccessStatus())
                {
                    // bdau load level mới sau 2s
                    levelManager.StartNextLevel();

                    // bật âm thanh qua màn
                    gameAudio.PlaySuccessAudio();

                    // chỉnh lại trạng thái isSuccess sau khi đã chạm vào LandingPad
                    player.SetIsSuccess(true);
                }

                break;

        }
    }
}
