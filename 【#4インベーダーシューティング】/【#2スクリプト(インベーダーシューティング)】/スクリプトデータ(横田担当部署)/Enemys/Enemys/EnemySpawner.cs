using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{

    //------�G�֘A-----

    //�G��ϐ��ɓ����
    //[SerializeField]�ɕύX����
    [SerializeField] GameObject NewEnemyPrefab;
    [SerializeField] GameObject NewEnemy2Prefab;
    [SerializeField] GameObject NewEnemy3Prefab;

    //�X�e�[�W���n�܂��ēG�𐶐�����܂ł̎���
    [SerializeField] float start = 0.0f;

    //�G�����̃C���^�[�o��
    //���ޗp�ӂ���
    [SerializeField] float interval = 0.08f;
    [SerializeField] float intarval2 = 0.8f;


    [Header("�V�[���ړ��֌W")]
    //�{�X��|������V�[�����ړ�����܂ł̎���
    [SerializeField] float sceneMoveTime;
    
    //�V�[���ړ����Ƀt�F�[�h�ɂ����鎞��
    [SerializeField] float sceneFadeTime;
    
    
    GameObject[] BS;


    //���O��ς���
    /// <summary>
    //TimeScript  Timer  
    /// </summary>
    /// 
    //�������ԗp�̕ϐ�
    TimeScript timeLimit;

    void Start()
    {


        
        timeLimit = FindObjectOfType<TimeScript>();

       


        // ���݂̃V�[����ۑ�
        switch (SceneManager.GetActiveScene().name)
        {

            case "EnemyScene":
            case "BossEnemyScene":
               

                //�c�莞�Ԃ�1�b�ł�����ΓG�𐶐�
                if (timeLimit.time >= 1)
                {
                    Debug.Log("�G����");

                    StartCoroutine(Spawn(NewEnemyPrefab, start, interval));
                    StartCoroutine(Spawn(NewEnemy2Prefab, start, intarval2));
                    StartCoroutine(Spawn(NewEnemy3Prefab, start, intarval2));

                }

                break;
        }


        Debug.Log(SceneManager.GetActiveScene().name);




    }

    //��by�����搶�@�R���[�`�����g���ꍇ

    //����(�������A�@���ԁA�@�C���^�[�o��)
    IEnumerator Spawn(GameObject enemy, float start, float interval)
    {
        //��������܂ł̎���
        yield return new WaitForSeconds(start);

        //�������[�v
        while (true)
        {
            //
            if (Time.timeScale > 0)
            {
                //�G�𐶐�����
                Instantiate(enemy, new Vector3(-10f + 20 * Random.value, 8, 2f), Quaternion.Euler(-90, 0, 0));
            }


            //�C���^�[�o���̎��ԑ҂�
            //if���̒��ɓ����Ɩ������[�v�ɂȂ�
            yield return new WaitForSeconds(interval);


        }



        yield return null; ;
    }

    public void Update()
    {


        //�c�莞�Ԃ�0�b�ɂȂ�ΓG�̐������~�߂�
        if (timeLimit.time == 0)
        {
          
            StopAllCoroutines();


        }
    

        //Update�̒��ŃV�[����ǂݍ��܂Ȃ�
        //
        if ((SceneManager.GetActiveScene().name == "BossEnemyScene"))
        {
            
            //�A���S���Y���ƃ��W�b�N
            BS = GameObject.FindGameObjectsWithTag("Boss");

            //Debug.Log("�{�X�̐�" + BS.Length);
            if (BS.Length == 0)
            {

                //�R���[�`����S�Ď~�߂�
                StopAllCoroutines();

                Invoke("BossDestroySceneMove", sceneMoveTime);

            }
        }

       
    }



    void BossDestroySceneMove()
    {
        //�v���O�C���@��ʂ��t�F�[�h�C���@�t�F�[�h�A�E�g������
        //(�ړ���̃V�[�����A�t�F�[�h���̉�ʂ̃J���[�A�t�F�[�h���鎞��)
        Initiate.Fade("GameClearScene", Color.black, sceneFadeTime);
    }







}