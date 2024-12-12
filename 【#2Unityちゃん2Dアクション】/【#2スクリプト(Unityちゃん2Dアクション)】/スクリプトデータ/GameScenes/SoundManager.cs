using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("プレイヤーボイス")]
    [SerializeField] AudioClip playerDamegeVoice;
    [SerializeField] AudioClip playerDangerVoice;

    PlayerScript playerScript;
    int playerHPwatcher;


    AudioSource audioSource;

   



    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();

        audioSource = GetComponent<AudioSource>();

       
    }


    void Update()
    {
        playerHPwatcher = playerScript.playeryHp;




        if (playerHPwatcher > 1)
        {
            //audioSource.PlayOneShot(playerDamegeVoice);

        }
        if(playerHPwatcher == 1)
        {
            //audioSource.PlayOneShot(playerDangerVoice);
        }

    }
}
