using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRecoveryItemScript : MonoBehaviour
{
    //Player��HP(�̗�)�pScript

    
    PlayerScript playerScript;
    
    //�񕜗�
    [SerializeField] int recoveryAmount;
    void Start()
    {
        //Player���擾
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }



    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�A�C�e����Player�ɐG�ꂽ��
        if (collision.gameObject.CompareTag("Player"))
        {

            //�񕜗ʂ���HP����
            playerScript.playeryHp += recoveryAmount;

            //���g��j�󂷂�
            Destroy(gameObject);

        }
    }
}
