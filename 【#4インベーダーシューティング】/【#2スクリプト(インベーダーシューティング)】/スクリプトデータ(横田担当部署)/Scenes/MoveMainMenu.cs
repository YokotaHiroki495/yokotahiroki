
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class MoveMainMenu : MonoBehaviour
{

    //���g���C����ۂ̌��݂̃X�e�[�W���擾����ϐ�
    string nowStage;

    void Start()
    {
        nowStage = PauseScript.retryScene;
    }

    private void Update()
    {
        //ESC�L�[����������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            //�G�f�B�^�[�̍Đ����~�߂�
            EditorApplication.isPlaying = false;
#else
         //�Q�[�����I��
         Application.Quit();

#endif

        }
    }






        public void Retry()
        {
       
        //SceneManager.LoadScene("Enemy Test");
        Initiate.Fade(nowStage, Color.black, 1.0f);

        }

  
    public void Title()
    {
        //�^�C�g����ʂɖ߂�
        SceneManager.LoadScene("TitleScene");
    }

}
