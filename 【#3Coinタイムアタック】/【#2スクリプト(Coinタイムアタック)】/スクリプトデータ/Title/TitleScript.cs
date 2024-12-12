using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


//TitleScene�̃X�N���v�g
public class TitleScript: MonoBehaviour
{
    //�^�C�g����ʗpScript
    //���U���g�̃X�R�A�������Ă��ĕ\������

    //Score�ϐ���ۑ����邽�߂̔z��𐶐�
    public float [] TitleHighScore;

    //�����L���O�p�X�R�A
    [SerializeField] GameObject TitleFirstScore;
    [SerializeField] GameObject TitleSecondScore;
    [SerializeField] GameObject TitleTherdScore;

    //TextMesh��\������p�̕ϐ�
    private TextMeshProUGUI TitleFirstText;
    private TextMeshProUGUI TitleSecondText;
    private TextMeshProUGUI TitleTherdText;

    //���U���g��ʂ�Script���擾������
    public ResultScript resultscropt;


    //�V�[���ړ�����ۂ̉��o
    //���[�h����V�[�����O
    [SerializeField] private string SceneName;
    //�t�F�[�h�̐F
    [SerializeField] private Color FadeColor;
    //�t�F�[�h�̑��x
    [SerializeField] private float FadeSpeed;
 


    void Start()
    {
        //�^�C�g���p�̔z��̒��g���쐬
        TitleHighScore = new float[3];



        //�^�C�g���\�����ɕۑ�����Ă���n�C�X�R�A���^�C�g���悤�̔z��ɓ����
        TitleHighScore[0] = PlayerPrefs.GetFloat("HIGHSCORETIME0");
        TitleHighScore[1] = PlayerPrefs.GetFloat("HIGHSCORETIME1");
        TitleHighScore[2] = PlayerPrefs.GetFloat("HIGHSCORETIME2");



        //�e�L�X�g�̏�q�I�u�W�F�N�g���擾
        TitleFirstText = TitleFirstScore.GetComponent<TextMeshProUGUI>();
        TitleSecondText = TitleSecondScore.GetComponent<TextMeshProUGUI>();
        TitleTherdText = TitleTherdScore.GetComponent<TextMeshProUGUI>();


        //�擾�����l���^�C�g���ɕ\��
        TitleFirstText.text = "1st:" + TitleHighScore[0].ToString("n1");
        TitleSecondText.text = "2nd:" + TitleHighScore[1].ToString("n1");
        TitleTherdText.text = "3rd:" + TitleHighScore[2].ToString("n1");

    }

    // Update is called once per frame
    void Update()
    {
        //����Escapa�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Quite�����s
            Quite();
        }
        
    }

    //�{�^��(�X�^�[�g�{�^��)�������ꂽ��
    public void OnClick()
    {
        //�t�F�[�h���g����GameScene�Ɉړ�
        Initiate.Fade("Tutorial", Color.black, 1.0f);
    }



    //�Q�[���C��
    void Quite()
    {
        //# �v���v���Z�X(�O����)�@�v���b�g�t�H�[���ˑ��R���p�C��
#if UNITY_EDITOR //�Q�[���R�[�h���� Unity �G�f�B�^�[�̃X�N���v�g���Ăяo�����߂� #define �f�B���N�e�B�u
        //�G�f�B�^�[�̍Đ����[�h�̏I��
        UnityEditor.EditorApplication.isPlaying = false; //true = ���s�@false = �I��
#else
        //�A�v���P�[�V�����̏I��
        Application.Quit(); //exe�ɏo�͂����ۂɂ͂��̕������Ȃ��ƃQ�[�����I�����Ȃ�
#endif

    }
}
