using UnityEngine;

public class MoveStageSelect : MonoBehaviour
{

    void Update()
    {
        SceneMove();
    }

    void SceneMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // シーンを移動する
            Initiate.Fade("StageSelect", Color.black, 2.0f);

        }
    }
}
