using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTransformPositionArea : MonoBehaviour
{
    //�X�e�[�W���ړ�������Script

    //�Ǐ]����Ώ�
    [SerializeField] private Transform target;

    //�ΏۂƂ̋���
    [SerializeField] private float offsetX;




    // Update is called once per frame
    void Update()
    {
        
        //�I�u�W�F�N�g���W��ϐ��Ɋi�[
        Vector3 pos = transform.position * Time.deltaTime;

        //������ŕϓ����Ȃ��悤��Time.deltaTime�������Č�������ۂ�
        pos.z = target.position.z + offsetX�@;
        transform.position = pos;


    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("StartArea"))
        {
            Destroy(other.gameObject);
        }
    }



}
