using UnityEngine;

public class HighSpeedEnemy : MonoBehaviour
{

    // �����̕ϐ��Ɓ@�O���͗���

    //�ϐ��̒��ł̕��с@���\�b�h�̒��ł̕���
    
    //�C���X�y�N�^�[�\���������̂��ŏ���
    //����Ȃ����̂͌��



    //�����G�t�F�N�g
    public GameObject explosionPrefab;

    //�h���b�v�A�C�e���֘A
    public GameObject CureBombPrefab;

    //�����o�ϐ����A���[�J���ϐ������������蔻�f���ĕ�����
    int itemrand;

    //�ړ��֘A

    //�����̃����_������
    int moverand;

    //���ւ̈ړ��X�s�[�h
    [SerializeField] float Speed = 3f;

    //���E�̈ړ��X�s�[�h
    public float XSpeed = 5f;

    //���E�̈ړ�����
    bool XPlusMove = true;//X�����Ɂ{�ړ����H

    //�ړ��ʒu�̃����_������
    int Posrand;



    void Start()
    {
        //�����������_������
        moverand = Random.Range(1, 3);

        //�����_���Ȑ����ʒu
        Posrand = Random.Range(1, 4);

    }


    void Update()
    {


        //Debug.Log("�ړ��l"+moverand);

        //transform�擾

        //���̃f�[�^�ւ̃A�N�Z�X�����ʂɑ����邾��
        //���Ȃ������ǂ�
        
        //Transform Ene2Tra = this.transform;



        //���[���h���W����ɍ��W���擾
        //Vector3 WorldPos = Ene2Tra.position;
        Vector3 WorldPos = transform.position;

        // 2�@�ɂ͖��O�����Ē萔�ɂ���
        if (WorldPos.z < 2)
        {
            //5�ɂ����O�����Ē萔��
            WorldPos.z += 5 * Time.deltaTime;

        }

        //��O�Ɍ������Ĉړ�


        //swith��
        //if�Ȃ�
        if (moverand == 1)
        {
            //�ʒu�p�^�[��1�̂Ƃ�
            //�������̂ق����X�C�b�`��
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

            ////���Ɍ������Ĉړ�
            //WorldPos.y -= Speed * Time.deltaTime;

        }
        //�W�O�U�O�ړ�

        // if (moverand == 2)�g���Ȃ炱����else if���g��
        //if (moverand == 2)
        else if(moverand == 2)
        {
            ////���Ɍ������Ĉړ�
            //WorldPos.y -= Speed * Time.deltaTime;

            //�������肪true�Ȃ�
            if (XPlusMove)
            {
                //���̂܂܁{�����Ɉړ�����
                WorldPos.x += XSpeed * Time.deltaTime;

                //�������ȏ�Ɉړ�������
                if (WorldPos.x >= 10)
                {
                    //�{�����ɐi��ł��锻���false�ɂ���
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


        //���Ɍ������Ĉړ�
        WorldPos.y -= Speed * Time.deltaTime;




        //���[���h���W����Ƃ��č��W��ύX
        transform.position = WorldPos;

    }
    // ���\�b�h�����āA

    private void OnTriggerStay(Collider col)
    {




        // switch�ɂ���@�@�P�[�X�o���b�g�v���t�@�u���@�P�[�X�r�[���ŏ�����ς���

        if (col.gameObject.tag == "bulletPrefab")
        {
 
            //���̃I�u�W�F�N�g��j�󂷂�
            Destroy(gameObject);

            //�����G�t�F�N�g��\�����āA�G�t�F�N�g��1�b��ɍ폜����
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

           
            //�{���񕜃A�C�e���̐���
            itemrand = Random.Range(1, 31);
            if (itemrand == 1)
            {
                Instantiate(CureBombPrefab, transform.position, Quaternion.identity);

            }

        }

        if (col.gameObject.tag == "Beam")
        {

           

            //���̃I�u�W�F�N�g��j�󂷂�
            Destroy(gameObject);

            //�����G�t�F�N�g��\�����āA�G�t�F�N�g��1�b��ɍ폜����
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);


            //�{���񕜃A�C�e���̐���
            //�}�W�b�N�i���o�[�͌�����
            //�C���X�y�N�^�[�Ƃ��ŊȒP�ɒ����o����悤��
            // %
            // Random.value�Őݒ肵�������ǂ�





            //itemrand = Random.Range(1, 1001);
            if (itemrand == 1)
            {
                Instantiate(CureBombPrefab, transform.position, Quaternion.identity);


            }


            // -------------------��������
            //N = 5;

            //if (Random.value * 100 <= N) ;

        }
    }
}
