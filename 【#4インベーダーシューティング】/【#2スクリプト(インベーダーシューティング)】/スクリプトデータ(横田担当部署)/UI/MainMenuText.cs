using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuText : MonoBehaviour
{


    //�^�C�g����ʂ̃X�^�[�g�e�L�X�g���g��A�k��������
    public float time, changeSpeed;
    public bool enlarge;

    void Start()
    {
        enlarge = true;
    }

    void Update()
    {
        changeSpeed = Time.deltaTime * 0.5f;

        if (time < 0)
        {
            enlarge = true;
        }
        if (time > 3f)
        {
            enlarge = false;
        }

        if (enlarge == true)
        {
            time += Time.deltaTime;
            transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
        else
        {
            time -= Time.deltaTime;
            transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
    }
}