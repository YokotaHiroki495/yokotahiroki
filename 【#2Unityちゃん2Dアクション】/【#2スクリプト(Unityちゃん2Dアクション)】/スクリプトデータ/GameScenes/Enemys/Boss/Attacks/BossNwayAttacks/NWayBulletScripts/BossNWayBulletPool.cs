using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class BossNWayBulletPool : MonoBehaviour
{
    //BossN-Way�U���̒e���̂�ObjectPool

    //�e�̃v���t�@�u
    [SerializeField] GameObject boss3wayBulletPregfab;

    //�I�u�W�F�N�g�v�[��
    ObjectPool<GameObject> boss3WayBulletPool;

    void Start()
    {

       
        boss3WayBulletPool = new ObjectPool<GameObject>
            (
                Create3WayBulletPoolObject,  //��1�����@�v�[�����ɃI�u�W�F�N�g���Ȃ��ꍇ�A��������
                OnTakeFromPool,         //��2�����@�v�[�����Ƀv�[������Ă��Ĕ�\���ɂȂ��Ă���I�u�W�F�N�g��\����Ԃɂ���
                OnReturnedToPool,       //��3�����@�I�u�W�F�N�g���v�[�����ɖ߂�
                Destroy3WayBulletPoolObject, //��4�����@�I�u�W�F�N�g���v�[���ɖ߂��Ȃ������Ƃ�(�ő吔�𒴂����Ƃ�)�I�u�W�F�N�g���폜����
                true,                   //��5�����@���Ńv�[���ɂ���I�u�W�F�N�g��ǉ������ꍇ�ɗ�O�Ƃ��邩
                                        //true�ɂ��Ă����ƃI�u�W�F�N�g���v�[�����ɖ߂�Ƃ��Ɏ����Ŏ��s�����
                5,          //�ŏ��ɍ����I�u�W�F�N�g��
                10           //�ő吔�@���̒l�ȏ㐶�����ꂽ��A�j�󂷂�
            );
    }

    //ObjectPool �R���X�g���N�^1�ڂ̈����̊֐�
    //�v�[���ɋ󂫂��������ɐV���ɐ������鏈��
    //objectPool.Get()�̎��Ă΂��
    GameObject Create3WayBulletPoolObject()
    {
        Debug.Log("Create3WayBulletPoolObject");
        GameObject created3wayBullet = Instantiate(boss3wayBulletPregfab);

        return created3wayBullet;

    }


    //ObjectPool �R���X�g���N�^2�ڂ̈����̊֐�
    //�v�[���ɋ󂫂����������̏���
    //objectPool.Get()�̎��Ă΂��
    void OnTakeFromPool(GameObject target)
    {
        Debug.Log("OnTakeFromPool");
        target.SetActive(true);

    }


    //ObjectPool �R���X�g���N�^3�ڂ̈����̊֐�
    //�v�[���ɕԋp���鎞�̏���
    void OnReturnedToPool(GameObject target)
    {
        Debug.Log("OnReturnedToPool");
        target.SetActive(false);
    }


    //ObjectPool �R���X�g���N�^4�ڂ̈����̊֐�
    //�w�肵������葽���Ȃ����玩���ō폜����
    void Destroy3WayBulletPoolObject(GameObject target)
    {
        Debug.Log("Destroy3WayBulletPoolObject");
        Destroy(target);
    }


    //�Ăяo������
    public GameObject Get3WayBullet()
    {
        Debug.Log("Get3WayBullet");

        GameObject created3WayBullet = boss3WayBulletPool.Get();          //�I�u�W�F�N�g���C���X�^���X����Ă��ȃG���[
        return created3WayBullet;
    }

    //�Ăяo���Ă���G�l�~�[���폜����
    public void Del3WayBullet(GameObject created3WayBullet)
    {
        Debug.Log("Del3WayBullet");
        boss3WayBulletPool.Release(created3WayBullet);

    }

}

