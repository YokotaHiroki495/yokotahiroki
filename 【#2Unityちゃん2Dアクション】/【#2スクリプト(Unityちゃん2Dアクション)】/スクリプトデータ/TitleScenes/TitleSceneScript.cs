using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneScript : MonoBehaviour
{
    //--------シーン移動する際の演出
   
    public void OnClick()
    {
        //フェードを使っbattleてGameSceneに移動
        Initiate.Fade("BattleRoyaleScene", Color.black, 5f);
        

    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Z))
        {

            Initiate.Fade("BattleRoyaleScene", Color.black, 5f);

        }


        //ESCキーを押したら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            //エディターの再生を止めて
            EditorApplication.isPlaying = false;
#else
         //ゲームを終了
         Application.Quit();

#endif

        }
    }




}
