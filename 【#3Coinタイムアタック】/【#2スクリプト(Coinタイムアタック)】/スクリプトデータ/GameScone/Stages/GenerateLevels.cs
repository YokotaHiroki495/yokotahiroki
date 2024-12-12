using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevels : MonoBehaviour
{
    //�X�e�[�W�����Ɋւ���Script

    //���I�u�W�F�N�g���i�[����z��
    public GameObject[] level;


    public GameObject[] coinArea;
    public GameObject[] obstaclesArea;
    
    //��������I�u�W�F�N�g��Z���̈ʒu
    [SerializeField]public int zPos;

    //���𐶐��������
    public bool creatingLevel = false;

    //�������鏰�̔ԍ�
    public int lvlNum;
    public int coinAreaNum;
    public int obstacleAreaNum;


    //�������鏰�̍ŏ��i���o�[
    [SerializeField] private int reateMinNunber;

    //��������ۂ̍ŏ������_���i���o�[
    [SerializeField] private int rundomMinNumber;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject yuka;
    [SerializeField] private TextMesh counter;

    //Area�̐�����
    [SerializeField] int forward;
    //CoinArea�̐����ʒu
    [SerializeField] int zPositionPuls;
    //CoinArea�ɑ΂��Ă�ObstacleArea�̐����ʒu�̒���
    [SerializeField] int zPositionSpace;


    //�Œᐶ����
    [SerializeField]int currentZ;

    //�ǂ̃^�C�~���O�ŏ������̂�
    int createfield;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = reateMinNunber; i <= forward; i++)
        {
            CreatStage();
        }

    }

    // Update is called once per frame
    void Update()
    {




        //���̃|�W�V�������擾
        Vector3 yukaPos = yuka.transform.position;

        //�v���C���[�̈ʒu���擾
        Vector3 playerPos = player.transform.position;

        //�������擾
        float dis = Vector3.Distance(yukaPos, playerPos);

        //counter.text = Convert.ToString(dis);

        //int createfield = (int)(transform.position.z / 10  /*createcooltime*/); //�l��const��
        createfield = (int)(playerPos.z);

      

    }

    public void CreatStage()
    {
        //���������_���őI��
        //lvlNum = Random.Range(rundomMinNumber, level.Length); //�����Ő�������X�e�[�W�ɐ��������ď�Q���X�e�[�W���A���Ő�������Ȃ��悤�ɂ���

        //�C���X�^���X�őI���������𐶐�
        //Instantiate(level[lvlNum], new Vector3(0, 0, zPos), Quaternion.identity);

        //�R�C���G���A�Ə�Q���G���A�����ꂼ�ꃉ���_���Ŕz��ɓ����
        coinAreaNum = Random.Range(rundomMinNumber, coinArea.Length);
        obstacleAreaNum = Random.Range(rundomMinNumber, obstaclesArea.Length);

        //���ꂼ���z����Ń����_���ɐ���
        Instantiate(coinArea[coinAreaNum], new Vector3(0, 0, zPos), Quaternion.identity);
        Instantiate(obstaclesArea[obstacleAreaNum], new Vector3(0, 0, zPos + zPositionSpace), Quaternion.identity);

        //��Q���G���A���R�C���G���A�ƌ��݂ɐ��������悤�Ɉʒu�𒲐�
        zPos += zPositionPuls;
        creatingLevel = false;
    }


}
