using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI statusTextField;

    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LogPlayerStatus();
    }

    void LogPlayerStatus()
    {
        if (player.GetIsHurtStatus() && !player.GetIsDeadStatus())
        {
            statusTextField.text = "You are hurt, now you are much slower";
        }
        else if (player.GetIsDeadStatus())
        {
            statusTextField.text = "You have crashed, better luck next time";
        }
        else if (!player.GetIsHurtStatus() && !player.GetIsDeadStatus())
        {
            statusTextField.text = "You are all good, now you fly like a wind";
        }
        
    }
}
