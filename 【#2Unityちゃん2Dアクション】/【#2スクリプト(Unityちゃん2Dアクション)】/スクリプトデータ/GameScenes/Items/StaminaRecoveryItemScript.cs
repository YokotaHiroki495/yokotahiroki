using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaRecoveryItemScript : MonoBehaviour
{
    //Player�̃X�^�~�i�񕜗p�A�C�e��Script

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
