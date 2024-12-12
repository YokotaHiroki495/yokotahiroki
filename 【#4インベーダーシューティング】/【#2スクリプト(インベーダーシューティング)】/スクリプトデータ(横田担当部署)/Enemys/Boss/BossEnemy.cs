using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossEnemy : MonoBehaviour
{

    //[SerializeField]public �̂悤�ȏ������͂��Ȃ�
    //�ǂ���������
    //public������


    [SerializeField]int BossHp = 10000;
    [SerializeField]float bossMoveSpeed = 1f;

    //BossController BC;

    public GameObject BossexplosionPrefab;
    public GameObject ExplosionPrefab;



    void Start()
    {

        //BC = GetComponent<BossController>();
    }

    void Update()
    {
        // �v���C���[���Ɍ������Ĉړ���������
        transform.Translate(0, 0, -bossMoveSpeed * Time.deltaTime);

        // �R���C�_�[�ƃ��W�b�h�m�f�B�œ����蔻����Ƃ�Ȃ�
        // moveposition�łƂ��������悢�@���@�D��x�͒Ⴍ���A���Ԃ���������


        // �R�����g�������ꍇ��//�̌�ɃX�y�[�X������
        // //�̌�ɃX�y�[�X�����Ȃ��͈̂ꎞ�I�ɃR�����g�A�E�g���Ă���v���O�����̎�
        
    }



    // ���L�������@���\�b�h�����ČĂяo���������ǂ��Ǝv��
    //void OnTriggerEnter(Collider other) => Damege(other.gameObject.tag, "bulletPrefab");
    //void OnTriggerStay(Collider other) => Damege(other.gameObject.tag, "Beam");
    ////������n����Damege���\�b�h�����s����

    //// ��
    //void Damege(string targetTag, string hitTag)
    //{
    //    if (targetTag == hitTag)
    //    {
    //        BossHp--;
    //        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

    //        if (BossHp <= 0)
    //        {
    //            GameObject obj = Instantiate(BossexplosionPrefab, transform.position, Quaternion.identity) as GameObject;
    //            obj.transform.localScale = new Vector3(50, 50, 50);
    //            Destroy(gameObject);


    //        }

    //    }

    //}
    // -----------------------------------------------------------------------------


    void OnTriggerEnter(Collider other) => Damege(other, "bulletPrefab");
    void OnTriggerStay(Collider other) => Damege(other, "Beam");


    void Damege(Collider other, string hitTag)
    {
        if (other.gameObject.tag == hitTag)
        {
            BossHp--;
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

            if (BossHp <= 0)
            {
                // Boss���j�p�����G�t�F�N�g��ǂݍ����
                GameObject obj = Instantiate(BossexplosionPrefab, transform.position, Quaternion.identity) as GameObject;

                //�w�肵���ʒu�ŃG�t�F�N�g��\��
                obj.transform.localScale = new Vector3(50, 50, 50);

                Invoke("MoveClearScene", 2);

                //������j��
                Destroy(gameObject);

            }

        }

    }


    void MoveClearScene()
    {
        Debug.Log("�N���A�V�[���ړ�");
        //�v���O�C���@��ʂ��t�F�[�h�C���@�t�F�[�h�A�E�g������
        //(�ړ���̃V�[�����A�t�F�[�h���̉�ʂ̃J���[�A�t�F�[�h���鎞��)
        Initiate.Fade("GameClear", Color.black, 1.0f);

    }













    //void OnTriggerEnter(Collider col)
    //{
    //    // �I���g���K�[�G���^�[
    //    // �R���C�_�[�ƃ��W�b�h�{�f�B�łƂ��Ă���



    //    //�v���C���[�̍U��������������
    //    if (col.gameObject.tag == "bulletPrefab")
    //    {
    //        // �̗͂�1���炷
    //        // �}�W�b�N�i���o�[�̓A�E�g
    //        // ���̏������Ȃ�
    //        BossHp -= 1;

    //        // -1�Ȃ炱��ŗǂ�
    //        //BossHp--;

    //        //�����G�t�F�N�g���Đ�
    //        //�I�u�W�F�N�g�v�[���̂ق����ǂ��������A�ŏ��̍�i�Ȃ̂ŃM���ǂ�
    //        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

    //        //�̗͂�0�ɂȂ�����
    //        if (BossHp <= 0)
    //        {
    //            // �����G�t�F�N�g��ǂݍ����
    //            //�G�t�F�N�g�̕\�����W�������ɏ����Ƃ�
    //            //GameObject obj = Instantiate(BossexplosionPrefab, transform.position, Quaternion.identity) as GameObject;

    //            //// �w�肵���ʒu�ŃG�t�F�N�g��\��
    //            ////  
    //            //obj.transform.localScale = new Vector3(50, 50, 50);


    //            // ��L���܂Ƃ߂ď����Ƃ����Ȃ�
    //            Instantiate(BossexplosionPrefab, transform.position, Quaternion.identity).transform.localScale = new Vector3(50, 50, 50);

    //            //������j��
    //            Destroy(gameObject);

    //        }
    //    }


    //}




    //// other�� col�ɂ���Ƃ�
    //void OnTriggerStay(Collider other)
    //{
    //    //���[�U�[�U���ɓ���������
    //    if (other.gameObject.tag == "Beam")
    //    {

    //        // ���g�����\�b�h��



    //        BossHp -= 1;

    //        // -1�Ȃ炱��ŗǂ�
    //        //BossHp--;

    //        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    //        //Debug.Log(BossHp);

    //        //�̗͂�0�ɂȂ�����
    //        if (BossHp <= 0)
    //        {
    //            //Boss���j�p�����G�t�F�N�g��ǂݍ����
    //            GameObject obj = Instantiate(BossexplosionPrefab, transform.position, Quaternion.identity) as GameObject;

    //            //�w�肵���ʒu�ŃG�t�F�N�g��\��
    //            obj.transform.localScale = new Vector3(50, 50, 50);

    //            //������j��
    //            Destroy(gameObject);

    //        }
    //    }
    //}





}
