using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
   //�`���[�g���A��(����)��ʗpScript

    public void OnClick()
    {
        //�t�F�[�h���g����GameScene�Ɉړ�
        Initiate.Fade("GameScene", Color.black, 1.0f);
    }
}
