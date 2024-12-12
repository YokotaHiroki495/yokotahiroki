using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemy : MonoBehaviour
{
    //�G�̗̑�
    [SerializeField]int EnemyHp = 3;


    //-------------�G�̈ړ��֘A-------------
    //���ւ̈ړ��X�s�[�h
    [SerializeField] float Speed = 3;
    
    //�����̃����_������
    int moverand;

    //���E�̈ړ��X�s�[�h
    public float XSpeed = 5f;

    //���E�̈ړ�����
    bool XPlusMove = true;//X�����Ɂ{�ړ����H
    //�ړ���̃����_���ݒ�
    int Posrand;


    //�����G�t�F�N�g
    public GameObject explosionPrefab;

    //�h���b�v�A�C�e���֘A
    public GameObject CureBombPrefab;

    //������
    public GameObject ItemPrefab;
    int itemrand;

    //���֘A
    public AudioClip Sound1;
    AudioSource audioSource;


    void Start()
    {
        //�����������_������
        moverand = Random.Range(1, 3);

        //�����_���Ȑ����ʒu
        Posrand = Random.Range(1, 4);

        //���֌W
        audioSource = GetComponent<AudioSource>();

        
    }


    void Update()
    {
        //transform�擾
        Transform Ene2Tra = this.transform;

        //���[���h���W����ɍ��W���擾
        Vector3 WorldPos = Ene2Tra.position;

        //��O�Ɍ������Ĉړ�
        if (moverand == 1)
        {
            //�ʒu�p�^�[��1�̂Ƃ�
            if (Posrand == 1)
            {
                //�����A�����ʒu��X10�����傫��������
                if (WorldPos.x > 10)
                {
                    //�}�C�i�X(��)�����Ɉړ�����
                    WorldPos.x -= XSpeed * Time.deltaTime;
                }
                //�����A�����ʒu��X-10����������������
                else if (WorldPos.x < -10)
                {
                    //�{����(�E)�Ɉړ�����
                    WorldPos.x += XSpeed * Time.deltaTime;
                }

            }
            //�ʒu�p�^�[��2�̂Ƃ�
            if (Posrand == 2)
            {
                if (WorldPos.x > 5)
                {
                    WorldPos.x -= XSpeed * Time.deltaTime;
                }
                else if (WorldPos.x < -5)
                {
                    WorldPos.x += XSpeed * Time.deltaTime;
                }

            }
            //�ʒu�p�^�[��3�̂Ƃ�
            if (Posrand == 3)
            {
                if (WorldPos.x > 1)
                {
                    WorldPos.x -= XSpeed * Time.deltaTime;
                }
                else if (WorldPos.x < -1)
                {
                    WorldPos.x += XSpeed * Time.deltaTime;
                }

            }

            //���Ɍ������Ĉړ�
            WorldPos.y -= Speed * Time.deltaTime;

        }
        //�W�O�U�O�ړ�
        if (moverand == 2)
        {
            //���Ɍ������Ĉړ�
            WorldPos.y -= Speed * Time.deltaTime;

            //�������肪true(�{��)�Ȃ�
            if (XPlusMove)
            {
                //���̂܂܁{�����Ɉړ�����
                WorldPos.x += XSpeed * Time.deltaTime;

                //�������ȏ�Ɉړ�������
                if (WorldPos.x >= 10)
                {
                    //�{�����ɐi��ł��锻���false(�R)�ɂ���
                    XPlusMove = false;
                }
            }
            else//�����������肪false�ɂȂ��Ă�����
            {
                //�}�C�i�X�����Ɉړ�����
                WorldPos.x -= XSpeed * Time.deltaTime;

                //�}�C�i�X�ɐi��Ŕ���O�ɏo����
                if (WorldPos.x <= -10)
                {
                    //���������true(�{��)�ɂ���
                    XPlusMove = true;
                }
            }
        }
        //���[���h���W����Ƃ��č��W��ύX
        Ene2Tra.position = WorldPos;

    }



    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "bulletPrefab")
        {
            //�����̗̑͂�1���炵��
            EnemyHp -= 1;

            //Debug.Log(EnemyHp);
            //�����̗͂�0�ɂȂ�����
            if (EnemyHp <= 0)
            {

              
                //���̃I�u�W�F�N�g��j�󂷂�
                Destroy(gameObject);

                //�����G�t�F�N�g��\�����āA�G�t�F�N�g��1�b��ɍ폜����
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);


                //�{���񕜃A�C�e���̃����_������
                itemrand = Random.Range(1, 31);
                if (itemrand == 1)
                {
                    Instantiate(ItemPrefab, transform.position, Quaternion.identity);

                }
            }
        }
        if (col.gameObject.tag == "Beam")
        {
            //���̃I�u�W�F�N�g��j�󂷂�
            Destroy(gameObject);

            //�����G�t�F�N�g��\�����āA�G�t�F�N�g��1�b��ɍ폜����
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);



            //�{���񕜃A�C�e���̃����_������
            itemrand = Random.Range(1, 1001);
            if (itemrand == 1)
            {
                Instantiate(ItemPrefab, transform.position, Quaternion.identity);

            }

        }



    } 
}


