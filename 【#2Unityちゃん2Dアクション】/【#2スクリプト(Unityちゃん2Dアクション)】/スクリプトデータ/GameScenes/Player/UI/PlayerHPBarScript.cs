using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHPBarScript : MonoBehaviour
{
    int maxHP; //�����Ƀv���C����playerHp�̒l������

    int crrentHp; //�����Ƀv���C���[�̌��݂�HP������

    public Slider playerHpSlider;

    PlayerScript playerScript;

    int enat;
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        //enemyattackScript = GetComponent<EnemyAttackScript>();
     

        //�o�[���ő��
        playerHpSlider.value = 1;

        //�v���C���[�̏����̗͂��ő�HP�ɂ���
        maxHP = playerScript.playeryHp;//crrentHp;

        //�ő�̗͂����݂̗̑͂ɓ����
        crrentHp = maxHP;
    }

    void Update()
    {
        PlayerHPDamege();
    }



    public  void PlayerHPDamege()
    {

        //Player�������Ă���playerHp����ɎQ�Ƃ��āA�̗̓o�[�ɔ��f������
        crrentHp = playerScript.playeryHp;

        //�X���C�_�[��maxHP���猻�݂̗̑͂ɔ��f������
        playerHpSlider.value = (float)crrentHp / (float)maxHP;
        


    }
}
