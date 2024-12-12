using UnityEngine;



public class SpawnEnemy : MonoBehaviour
{


    //�I�u�W�F�N�g�v�[��
    [SerializeField] EnemyObjectPool enemyManager;
    

    // �G�̐����ʒu�֘A
    // �G�̃X�|�[���ʒu�p�̕ϐ�
    Vector2 randomrange;

    //�G�̐����ʒu�̐ݒ�
    
    //X���W�̍ŏ��|�W�ƍő�|�W
    [SerializeField] float spawnXmin, spawnXmax;

    //Y���W�̍ŏ��|�W�ƍő�|�W
    [SerializeField] float spawnYmin, spawnYmax;

    // �G�̃X�|�[���֘A
    // �X�e�[�W���n�܂��Ă��牽�b��ɓG�𐶐����n�߂邩
    [SerializeField] float start;

    //�G�̐����̃C���^�[�o������
    [SerializeField] float interval;


    void Start()
    {
      
        //����I�ɓG���X�|�[��������
        InvokeRepeating("CreateEnemy", start, interval);


        //�X�R�A(�c��G��)��0�ɂȂ�����
        if (GameManager.score == 0)
        {
            //�G�̃X�|�[�����~����
            CancelInvoke();
            
            //�c�����G������
            enemyManager.DelEnemy(gameObject);

        }
    }



    void Update()
    {
        //�G�̐����ʒu��
        randomrange = new Vector2(Random.Range(spawnXmin, spawnXmax), Random.Range(spawnYmin, spawnYmax));

    }


    //�G�̐����@Pool(EnemyManager)�ɃA�N�Z�X
    void CreateEnemy()
    {

        //�G���I�u�W�F�N�g�v�[��������o��
        GameObject enemy = enemyManager.GetEnemy();

        //�G���w�肵���͈͂̒��Ń����_���ɐ���
        enemy.transform.position = randomrange;

    }

    
}

