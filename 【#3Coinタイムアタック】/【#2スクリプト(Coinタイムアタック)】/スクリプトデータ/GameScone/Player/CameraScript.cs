using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{

    //�v���C���[���擾
    [SerializeField]private GameObject player;

    Player playerScript;

    //�������̃G�t�F�N�g
    [SerializeField] GameObject DashEffect;

    float yVelocity = 5.0f;
    [SerializeField] private float NomalFov = 75f;       //�ʏ펞�̃J����FOV
    [SerializeField] private float AccelFov  = 100f;    //�������̃J����FOV
    [SerializeField] private float DashCameraRetunTime = 2; //�������ɐ؂�ւ�����J���������̈ʒu�ɖ߂��܂ł̎���
    private float smoothTime = 1000;

    float StartFov;
    float NowFov;
    Camera Camera = null;

    //�v���C���[�Ƃ̋���
    [SerializeField]private float offsetZ;
    float offsetZ_Save = 0f;

    //target(Player)�̈ʒu���擾���邽��
    [SerializeField] private Transform target;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();


        //�J�����ƃv���C���[�̋��������߂�(�v���C���[�̍��E�ɂ��Ǐ])
        StartFov = 75f;
        offsetZ_Save = offsetZ;


        //�J�����̈ʒu��ݒ�
        Camera = Camera.main;

         

}

    
    void Update()
    {
        //�I�u�W�F�N�g���W��ϐ��Ɋi�[
        Vector3 pos = transform.position;
        pos.z = target.position.z + offsetZ;
        transform.position = pos;

       

       
        //�J������FOV��StartFov���傫���Ȃ�
        if(Camera.fieldOfView > StartFov)
        {
            //FOV������������������
            Camera.fieldOfView -= 1f;
          
            //�������̃G�t�F�N�g��\��
            DashEffect.SetActive(true);
        }

        //�J�����̈ʒu�̔�r������
        //���̈ʒu��菬����������
        if(offsetZ < offsetZ_Save)
        {
            //�傫������
            offsetZ += 1f;
           
            
        }
        //FOV��80�ȉ��Ȃ�
        if(Camera.fieldOfView < 80)
        {
            //�_�b�V���̃G�t�F�N�g���\��
            DashEffect.SetActive(false);
        }

    }


    //�R���[�`��
    public IEnumerator CameraMove()
    {
        Camera.main.fieldOfView = Mathf.SmoothDamp(AccelFov, NomalFov, ref yVelocity, smoothTime);

        offsetZ -= 5;

        
        yield return new WaitForSeconds(DashCameraRetunTime);

        playerScript.DashNow = false;






    }

   
}
