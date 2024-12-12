using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
   

  
    void Start()
    {
        //マウスカーソルの表示を消す
        Cursor.visible = false;
       
    }

    // Update is called once per frame
    void Update()
    {
       

        //コントローラーのスタートボタン、もしくはコントローラーのAボタンをを押したら
        if (Input.GetButtonDown("Start")　|| Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Z))
        {

           //プラグインを使用

            Initiate.Fade("EnemyScene", Color.black, 1.0f);

        }

     

        //ESCキーを押したらゲーム終了
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






