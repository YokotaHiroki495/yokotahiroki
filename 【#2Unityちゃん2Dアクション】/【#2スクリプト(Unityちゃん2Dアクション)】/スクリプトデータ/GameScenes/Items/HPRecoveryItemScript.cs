using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRecoveryItemScript : MonoBehaviour
{
    //PlayerのHP(体力)用Script

    
    PlayerScript playerScript;
    
    //回復量
    [SerializeField] int recoveryAmount;
    void Start()
    {
        //Playerを取得
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }



    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //アイテムがPlayerに触れたら
        if (collision.gameObject.CompareTag("Player"))
        {

            //回復量だけHPを回復
            playerScript.playeryHp += recoveryAmount;

            //自身を破壊する
            Destroy(gameObject);

        }
    }
}
