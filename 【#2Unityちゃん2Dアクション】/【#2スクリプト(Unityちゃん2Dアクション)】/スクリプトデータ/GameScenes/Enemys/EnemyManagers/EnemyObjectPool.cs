
using UnityEngine;
using UnityEngine.Pool;


public class EnemyObjectPool : MonoBehaviour
{
    //Enemy�p��ObjectPool
    //�G�̃v���t�@�u
    [SerializeField] GameObject[] EnemyPrefab;

    //�ŏ��ɐ�������G�̐�
    [SerializeField] int firstSpawnEnemyNumber;

    //��������G�̍ő吔
    [SerializeField] int maxSpawnEnemyNumber;


    //�I�u�W�F�N�g�v�[��
    ObjectPool<GameObject> pool;

    void Start()
    {
        pool = new ObjectPool<GameObject>
            (
                EnemyCreatePoolObject,  //��1�����@�v�[�����ɃI�u�W�F�N�g���Ȃ��ꍇ�A��������
                OnTakeFromPool,         //��2�����@�v�[�����Ƀv�[������Ă��Ĕ�\���ɂȂ��Ă���I�u�W�F�N�g��\����Ԃɂ���
                OnReturnedToPool,       //��3�����@�I�u�W�F�N�g���v�[�����ɖ߂�
                EnemyDestroyPoolObject, //��4�����@�I�u�W�F�N�g���v�[���ɖ߂��Ȃ������Ƃ�(�ő吔�𒴂����Ƃ�)�I�u�W�F�N�g���폜����
                true,                   //��5�����@���Ńv�[���ɂ���I�u�W�F�N�g��ǉ������ꍇ�ɗ�O�Ƃ��邩
                                        //true�ɂ��Ă����ƃI�u�W�F�N�g���v�[�����ɖ߂�Ƃ��Ɏ����Ŏ��s�����
                firstSpawnEnemyNumber,          //�ŏ��ɍ����I�u�W�F�N�g��
                maxSpawnEnemyNumber           //�ő吔�@���̒l�ȏ㐶�����ꂽ��A�j�󂷂�
            );
    }

    //ObjectPool �R���X�g���N�^1�ڂ̈����̊֐�
    //�v�[���ɋ󂫂��������ɐV���ɐ������鏈��
    //objectPool.Get()�̎��Ă΂��
    GameObject EnemyCreatePoolObject()
    {
        //Inspector�ŃG�l�~�[�𕡐��ݒ�o����悤�ɂ���
        GameObject createdEnemy = Instantiate(EnemyPrefab[Random.Range(0,EnemyPrefab.Length)]);

        return createdEnemy;

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
    void EnemyDestroyPoolObject(GameObject target)
    {
        Destroy(target);
    }


    //�G�l�~�[���Ăяo������
    public GameObject GetEnemy()
    {
        GameObject createdEnemy = pool.Get();
        return createdEnemy;
    }

    //�Ăяo���Ă���G�l�~�[���폜����
    public void DelEnemy(GameObject createdEnemy)
    {
        pool.Release(createdEnemy);

    }
}
