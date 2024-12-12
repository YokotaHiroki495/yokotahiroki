using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    //���U���g��ʂł̃X�R�A�̕\���pScript

    //-----�X�R�A�ۑ��p-----
    public static float TimeScoreNum;


    //�����L���O�ۑ��p�z��
    public static float[] HighScoreTime;


    //-----�X�R�A�\���p-----
    public GameObject FirstTimeScore;
    public GameObject SecondTimeScore;
    public GameObject TherdTimeScore;

    public TextMeshProUGUI FirstTimeText;
    public TextMeshProUGUI SecondTimeText;
    public TextMeshProUGUI TherdTimeText;

    //����̃X�R�A�\���p
    public GameObject NowTimeScore;
    TextMeshProUGUI NowTimeScoreText;


    void Start()
    {
        //PlayerPrefs.DeleteKey("HIGHSCORETIME0");
        //PlayerPrefs.DeleteKey("HIGHSCORETIME1");
        //PlayerPrefs.DeleteKey("HIGHSCORETIME2");

        //GameManager�������Ă���X�R�A����
        TimeScoreNum = GameManager.ClearTimeCount;
  
        //���Ԃ��r���āA�����L���O����鏈��
        TimeRanking();


        //�e�L�X�g�ɕ\�����鏈��
        TimeTextDisplay();

        


    }


    private void TimeRanking()
    {

        //3�ʂ܂ŕ\������z����쐬
        HighScoreTime = new float[3];

        //�z��̃��[�h
        HighScoreTime[0] = PlayerPrefs.GetFloat("HIGHSCORETIME0");
        HighScoreTime[1] = PlayerPrefs.GetFloat("HIGHSCORETIME1");
        HighScoreTime[2] = PlayerPrefs.GetFloat("HIGHSCORETIME2");


        //-------------------------------------------------------------------------------------------------------------------------------�F�X�C��

        //��r
        //�z����Q�Ƃ��āA�^�C�������Ȃ������璆�g���X�V����B�������́A0�������Ă�����X�V
        //if (HighScoreTime[0] > TimeScoreNum || HighScoreTime[0] == 0)
        //{
        //    //�z��̐���������炵��
        //    HighScoreTime[2] = HighScoreTime[1];
        //    HighScoreTime[1] = HighScoreTime[0];

        //    //1�ʂ��X�V
        //    HighScoreTime[0] = TimeScoreNum;


        //    //�X�R�A��float�^�Ŏ擾
        //    PlayerPrefs.SetFloat("HIGHSCORETIME0", HighScoreTime[0]);
        //    //�X�R�A��ۑ�
        //    PlayerPrefs.Save();
        //}
        ////���X�z��ɓ����Ă���l�ȉ��ŁA�Ȃ�����1�ʂ̒l�������l���傫���ꍇ�B�������͒l��0�������Ă���ꍇ�B
        //else if (HighScoreTime[1] > TimeScoreNum && TimeScoreNum > HighScoreTime[0] || HighScoreTime[1] == 0)
        //{
        //    HighScoreTime[2] = HighScoreTime[1];
        //    HighScoreTime[1] = TimeScoreNum;

        //    PlayerPrefs.SetFloat("HIGHSCORETIME1", HighScoreTime[1]);
        //    PlayerPrefs.Save();
        //}
        //else if (HighScoreTime[2] > TimeScoreNum && TimeScoreNum > HighScoreTime[1] || HighScoreTime[2] == 0)
        //{
        //    HighScoreTime[2] = TimeScoreNum;

        //    PlayerPrefs.SetFloat("HIGHSCORETIME2", HighScoreTime[2]);
        //    PlayerPrefs.Save();

        //}

        //��rver2
        //�ŏ��͕K��1�ʂɂȂ�悤��
        if (HighScoreTime[0] == 0)
        {
            HighScoreTime[0] = TimeScoreNum;

            PlayerPrefs.SetFloat("HIGHSCORETIME0", HighScoreTime[0]);
            PlayerPrefs.SetFloat("HIGHSCORETIME1", HighScoreTime[1]);
            PlayerPrefs.SetFloat("HIGHSCORETIME2", HighScoreTime[2]);
            
            PlayerPrefs.Save();
        }
        else if (TimeScoreNum < HighScoreTime[0])
        {
            HighScoreTime[2] = HighScoreTime[1];
            HighScoreTime[1] = HighScoreTime[0];
            HighScoreTime[0] = TimeScoreNum;

            PlayerPrefs.SetFloat("HIGHSCORETIME0", HighScoreTime[0]);
            PlayerPrefs.SetFloat("HIGHSCORETIME1", HighScoreTime[1]);
            PlayerPrefs.SetFloat("HIGHSCORETIME2", HighScoreTime[2]);
            
            PlayerPrefs.Save();
        }
        else if (TimeScoreNum < HighScoreTime[1])
        {
            HighScoreTime[2] = HighScoreTime[1];
            HighScoreTime[1] = TimeScoreNum;

            PlayerPrefs.SetFloat("HIGHSCORETIME0", HighScoreTime[0]);
            PlayerPrefs.SetFloat("HIGHSCORETIME1", HighScoreTime[1]);
            PlayerPrefs.SetFloat("HIGHSCORETIME2", HighScoreTime[2]);

            PlayerPrefs.Save();
        }
        else if (TimeScoreNum < HighScoreTime[2])
        {
            HighScoreTime[2] = TimeScoreNum;

            PlayerPrefs.SetFloat("HIGHSCORETIME0", HighScoreTime[0]);
            PlayerPrefs.SetFloat("HIGHSCORETIME1", HighScoreTime[1]);
            PlayerPrefs.SetFloat("HIGHSCORETIME2", HighScoreTime[2]);

            PlayerPrefs.Save();
        }


    }

    //�����L���O�\��
    private void TimeTextDisplay()
    {


        //����̃X�R�A�̕\��
        NowTimeScoreText = NowTimeScore.GetComponent<TextMeshProUGUI>();
        NowTimeScoreText.text = "NowTime:" + TimeScoreNum.ToString("n1");



        //1�ʕ\��
        FirstTimeText = FirstTimeScore.GetComponent<TextMeshProUGUI>();
        FirstTimeText.text = "1st:" + HighScoreTime[0].ToString("n1");

        //2�ʕ\��
        SecondTimeText = SecondTimeScore.GetComponent<TextMeshProUGUI>();
        SecondTimeText.text = "2nd:" + HighScoreTime[1].ToString("n1");

        //3�ʕ\��
        TherdTimeText = TherdTimeScore.GetComponent<TextMeshProUGUI>();
        TherdTimeText.text = "3rd:" + HighScoreTime[2].ToString("n1");


    }


    //���g���C�{�^������
    public void Retry()
    {
        //�t�F�[�h���g����GameScene�Ɉړ�
        Initiate.Fade("GameScene", Color.black, 1.0f);
    }

    //�^�C�g���{�^������
    public void Title()
    {
        //�t�F�[�h���g����Title�Ɉړ�
        Initiate.Fade("Title", Color.black, 1.0f);

    }
}
