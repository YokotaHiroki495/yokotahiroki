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
    //--------シーン移動する際の演出

    public void OnClick()
    {
        //フェードを使っbattleてGameSceneに移動
        Initiate.Fade("TitleScene", Color.black, 5f);


    }

}
