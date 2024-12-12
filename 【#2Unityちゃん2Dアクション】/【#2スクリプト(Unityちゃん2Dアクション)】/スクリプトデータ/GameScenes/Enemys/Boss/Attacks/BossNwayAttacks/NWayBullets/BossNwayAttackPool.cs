using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class BossNwayAttackPool : MonoBehaviour
{
    //BossN-Way�U���pObjectPool

    //�e�̃v���t�@�u
    [SerializeField] GameObject boss3wayAttackPregfab;

    //�I�u�W�F�N�g�v�[��
    ObjectPool<GameObject> pool;

    void Start()
    {
        pool = new ObjectPool<GameObject>
            (
                CreateBoss3WayPoolObject,  //��1�����@�v�[�����ɃI�u�W�F�N�g���Ȃ��ꍇ�A��������
                OnTakeFromPool,         //��2�����@�v�[�����Ƀv�[������Ă��Ĕ�\���ɂȂ��Ă���I�u�W�F�N�g��\����Ԃɂ���
                OnReturnedToPool,       //��3�����@�I�u�W�F�N�g���v�[�����ɖ߂�
                DestroyBoss3WayPoolObject, //��4�����@�I�u�W�F�N�g���v�[���ɖ߂��Ȃ������Ƃ�(�ő吔�𒴂����Ƃ�)�I�u�W�F�N�g���폜����
                true,                   //��5�����@���Ńv�[���ɂ���I�u�W�F�N�g��ǉ������ꍇ�ɗ�O�Ƃ��邩
                                        //true�ɂ��Ă����ƃI�u�W�F�N�g���v�[�����ɖ߂�Ƃ��Ɏ����Ŏ��s�����
                5,          //�ŏ��ɍ����I�u�W�F�N�g��
                10           //�ő吔�@���̒l�ȏ㐶�����ꂽ��A�j�󂷂�
            );
    }

    //ObjectPool �R���X�g���N�^1�ڂ̈����̊֐�
    //�v�[���ɋ󂫂��������ɐV���ɐ������鏈��
    //objectPool.Get()�̎��Ă΂��
    GameObject CreateBoss3WayPoolObject()
    {
        GameObject createdBoss3way = Instantiate(boss3wayAttackPregfab);

        return createdBoss3way;

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
    void DestroyBoss3WayPoolObject(GameObject target)
    {
        Destroy(target);
    }


    //�G�l�~�[���Ăяo������
    public GameObject Get3Way()
    {
        GameObject created3Way = pool.Get();
        return created3Way;
    }

    //�Ăяo���Ă���G�l�~�[���폜����
    public void Del3Way(GameObject created3Way)
    {
        pool.Release(created3Way);

    }
}

