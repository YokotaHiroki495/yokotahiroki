using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneScript : MonoBehaviour
{
    //--------�V�[���ړ�����ۂ̉��o
   
    public void OnClick()
    {
        //�t�F�[�h���g��battle��GameScene�Ɉړ�
        Initiate.Fade("BattleRoyaleScene", Color.black, 5f);
        

    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Z))
        {

            Initiate.Fade("BattleRoyaleScene", Color.black, 5f);

        }


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




}
