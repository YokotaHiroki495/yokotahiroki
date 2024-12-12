using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class BossFireAttackPool : MonoBehaviour
{
    //Boss�̒ʏ�U���p��ObjectPool

    //�e�̃v���t�@�u
    [SerializeField] GameObject bossFireAttackPrefab;

    //�I�u�W�F�N�g�v�[��
    ObjectPool<GameObject> bossFirepool;

    void Start()
    {
        bossFirepool = new ObjectPool<GameObject>
            (
                CreateBossFirePoolObject,  //��1�����@�v�[�����ɃI�u�W�F�N�g���Ȃ��ꍇ�A��������
                OnTakeFromPool,         //��2�����@�v�[�����Ƀv�[������Ă��Ĕ�\���ɂȂ��Ă���I�u�W�F�N�g��\����Ԃɂ���
                OnReturnedToPool,       //��3�����@�I�u�W�F�N�g���v�[�����ɖ߂�
                DestroyBossFirePoolObject, //��4�����@�I�u�W�F�N�g���v�[���ɖ߂��Ȃ������Ƃ�(�ő吔�𒴂����Ƃ�)�I�u�W�F�N�g���폜����
                true,                   //��5�����@���Ńv�[���ɂ���I�u�W�F�N�g��ǉ������ꍇ�ɗ�O�Ƃ��邩
                                        //true�ɂ��Ă����ƃI�u�W�F�N�g���v�[�����ɖ߂�Ƃ��Ɏ����Ŏ��s�����
                10,          //�ŏ��ɍ����I�u�W�F�N�g��
                20           //�ő吔�@���̒l�ȏ㐶�����ꂽ��A�j�󂷂�
            );
    }

    //ObjectPool �R���X�g���N�^1�ڂ̈����̊֐�
    //�v�[���ɋ󂫂��������ɐV���ɐ������鏈��
    //objectPool.Get()�̎��Ă΂��
    GameObject CreateBossFirePoolObject()
    {
        //Inspector�ŃG�l�~�[�𕡐��ݒ�o����悤�ɂ���
        GameObject createdBossFire = Instantiate(bossFireAttackPrefab);

        return createdBossFire;

    }


    //ObjectPool �R���X�g���N�^2�ڂ̈����̊֐�
    //�v�[���ɋ󂫂����������̏���
    //objectPool.Get()�̎��Ă΂��
    void OnTakeFromPool(GameObject target)
    {
        target.SetActive(true);

    }


    //ObjectPool �R���X�g���N�^3�ڂ̈����̊֐�
    //�v�[���ɕԋp���鎞�̏���
    void OnReturnedToPool(GameObject target)
    {
        target.SetActive(false);
    }


    //ObjectPool �R���X�g���N�^4�ڂ̈����̊֐�
    //�w�肵������葽���Ȃ����玩���ō폜����
    void DestroyBossFirePoolObject(GameObject target)
    {
        Destroy(target);
    }


    //�G�l�~�[���Ăяo������
    public GameObject GetBossFire()
    {
        GameObject createdBossFire = bossFirepool.Get();
        return createdBossFire;
    }

    //�Ăяo���Ă���G�l�~�[���폜����
    public void DelBossFire(GameObject createdBossFire)
    {
        bossFirepool.Release(createdBossFire);

    }
}
