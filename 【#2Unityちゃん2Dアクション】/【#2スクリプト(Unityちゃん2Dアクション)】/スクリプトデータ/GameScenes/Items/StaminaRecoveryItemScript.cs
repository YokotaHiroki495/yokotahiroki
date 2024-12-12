using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaRecoveryItemScript : MonoBehaviour
{
    //Playerのスタミナ回復用アイテムScript

    PlayerScript playerScript;
    [SerializeField] int recoveryAmount;
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            playerScript.nowPlayerAttackStamina += recoveryAmount;

            Destroy(gameObject);

        }
    }
}
