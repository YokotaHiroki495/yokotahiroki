using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBarScript : MonoBehaviour
{
    int bossMaxHP; //�����Ƀ{�X��playerHp�̒l������

    int bossCrrentHp; //�����Ƀ{�X�̌��݂�HP������

    public Slider bossHpSlider; // �{�X�̗̑̓o�[


    BossController bossScript;

    void Start()
    {
        bossScript = GameObject.Find("BossEnemy").GetComponent<BossController>();

        //�o�[���ő��
        bossHpSlider.value = 1;

        //�v���C���[�̏����̗͂��ő�HP�ɂ���
        bossMaxHP = bossScript.enemyHp;
        
        //bossMaxHP = bossHp; //crrentHp;



        //�ő�̗͂����݂̗̑͂ɓ����
        bossCrrentHp = bossMaxHP;
    }

    public void BossHPDamege()
    {
        //Boss�������Ă���enemyHp����ɎQ�Ƃ��āA�̗̓o�[�ɔ��f������
        bossCrrentHp = bossScript.enemyHp;

        //�X���C�_�[��maxHP���猻�݂̗̑͂ɔ��f������
        bossHpSlider.value = (float)bossCrrentHp / (float)bossMaxHP;


    }
}
