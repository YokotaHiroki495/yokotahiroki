using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaVarScript : MonoBehaviour
{
    float maxSta; //�����Ƀv���C����playerStamina�̒l������

    float crrentSta; //�����Ƀv���C���[�̌��݂�Stamina������

    public Slider playerHpSlider;

    PlayerScript playerScript;

    //[SerializeField] EnemyAttackScript enemyattackScript;

    int enat;
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        //enemyattackScript = GetComponent<EnemyAttackScript>();


        

        //�o�[���ő��
        playerHpSlider.value = 1;
        
        //�v���C���[�̏����X�^�~�i���ő�ʂɂ���
        maxSta = playerScript.nowPlayerAttackStamina;

        //�ő�X�^�~�i�����݂̃X�^�~�i�ɓ����
        crrentSta = maxSta;

    }

    void Update()
    {
        PlayerStaminaDown();
        
    }



    void PlayerStaminaDown()
    {

        //Player�������Ă���playerHp����ɎQ�Ƃ��āA�̗̓o�[�ɔ��f������
        crrentSta = playerScript.nowPlayerAttackStamina;

        //�X���C�_�[��maxHP���猻�݂̗̑͂ɔ��f������
        playerHpSlider.value = (float)crrentSta / (float)maxSta;



    }
}
