using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //���֘A
    public AudioSource audioSource;

    //�X�R�A�֘A
    public static int Coin;
    public TextMeshProUGUI scoreText;

    //�����A�C�e��
    private static int GmanaHubItem;

    //�N���A�R�C����
    [SerializeField] private int ClearCoinNum;


    //����
    [SerializeField] private float TimeCount;

    //�N���A����
    public static float ClearTimeCount;

    public TextMeshProUGUI TimeText;


    //�v���C���[����󂯎�����R�C�����}�C�i�X�ɂ��邽�߂̕ϐ�����
    //�X�N���v�g���󂯎��
    public Player playerSC;

    //Player�X�N���v�g�̕ϐ��̎󂯎��p
    int PlayerSCMinusItemNum;


    
    void Start()
    {
        //�R�C���̖������O���Ƀ��Z�b�g����
        Coin = 0;

        //�R�C���̖������e�L�X�g�ɕ\��
        scoreText.text = "Coin:" + Coin;


        //���Ԃ������_���܂ŕ\��ToString n1�ɂ��A�����_�ȉ�1���܂ŕ\������
        TimeText.text = "Time:" + TimeCount.ToString("n1");


        //�v���C���[�X�N���v�g�ɂ��Ă���ϐ�MinusItemNum����
        PlayerSCMinusItemNum = playerSC.MinusItemNum;
       

    }

    void Update()
    {
        //���ԊǗ�
        TimeKeeper();


    }


    //���ԊǗ�(�J�E���g�_�E��)
    public void TimeKeeper()
    {
        //unscaledDeltaTime�ɂ��ATimeScale���ω����Ă����̃X�s�[�h�ŃJ�E���g���i��
        TimeCount += Time.unscaledDeltaTime;
        //�e�L�X�g�̎��Ԃ𐏎��A�X�V�B
        TimeText.text = "Time:" + TimeCount.ToString("n1");
    }

    //�X�R�A(�R�C��)�̉��Z
    public void PlusScore()
    {
        //�R�C�������Z
        Coin++;
        scoreText.text = "Coin:" + Coin;

        //�v���C���[�̃A�C�e�����Q��
        GmanaHubItem = Player.HubCoin;

        //���肵���R�C�����N���A�����������
        if (GmanaHubItem == ClearCoinNum)
        {
            
            //�V�[���ړ��ɂ����鎞�Ԃ͉��Z����Ȃ��̂ŃN���A������������_�̃^�C���ŕۑ������

            //�V�[���ړ�������
            ClearGame();

        }

    }

    //�X�R�A(�R�C��)���Z
    public void MinusScore()
    {
      
        //�v���C���[�̃X�N���v�g����R�C���̃}�C�i�X�����擾���Ă��̕������炷
        Coin -= PlayerSCMinusItemNum;
        scoreText.text = "Coin:" + Coin;

        //�X�R�A��0�����ɂȂ�����
        if (Coin < 0)
        {
            //�X�R�A��0�Ŏ~�߂�
            Coin = 0;
            scoreText.text = "Coin:" + Coin;
        }

    }

    //�Q�[�����N���A������
    public void ClearGame()
    {

        //�S�[�������ۂ̃^�C����ClearTimeCount�ɑ��
        ClearTimeCount = TimeCount;

        //��ʂ��t�F�[�h���ăV�[���ڍs
        Initiate.Fade("Result", Color.black, 1.0f);

    }


}
