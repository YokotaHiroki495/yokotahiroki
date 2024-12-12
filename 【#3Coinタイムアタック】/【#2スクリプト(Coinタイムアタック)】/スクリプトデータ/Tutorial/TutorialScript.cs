using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
   //チュートリアル(説明)画面用Script

    public void OnClick()
    {
        //フェードを使ってGameSceneに移動
        Initiate.Fade("GameScene", Color.black, 1.0f);
    }
}
