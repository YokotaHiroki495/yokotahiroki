using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResultSceneScript: MonoBehaviour
{



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR

            EditorApplication.isPlaying = false;
#else

         Application.Quit();

#endif

        }
    }
    //--------�V�[���ړ�����ۂ̉��o

    public void OnClick()
    {
        //�t�F�[�h���g��battle��GameScene�Ɉړ�
        Initiate.Fade("TitleScene", Color.black, 5f);


    }

}
