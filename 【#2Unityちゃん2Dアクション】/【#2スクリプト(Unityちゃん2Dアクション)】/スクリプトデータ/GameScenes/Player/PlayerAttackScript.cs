using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    //PlayerÇÃçUåÇópScript

    void Start()
    {
        Invoke("HitBoxDestroy", 0.1f);

    }


    void HitBoxDestroy()
    {
        Destroy(gameObject);

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            Destroy(collision.gameObject);
            HitBoxDestroy();
        }

    }


}
