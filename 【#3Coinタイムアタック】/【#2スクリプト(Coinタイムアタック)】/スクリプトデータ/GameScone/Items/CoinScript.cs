using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    //�R�C��(�A�C�e��)�Ɋւ���Script

    //�R�C���̉�]�X�s�[�h
    [SerializeField]private int CoinrotateSpeed;

    public GameManager gameManager;

    public Player player;

    Renderer  renderer;


    
    void Start()
    {
        //�Q�[���}�l�[�W���[���擾
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //�v���C���[���擾
        player = GameObject.Find("Player").GetComponent<Player>();

        renderer = GetComponent<MeshRenderer>();
        
        
    }


    void Update()
    {  
        //�I�u�W�F�N�g(�R�C��)����]������
        transform.Rotate(0, CoinrotateSpeed * Time.deltaTime, 0, Space.World);
    }

    //�����蔻��(Trigger)������������
    private void OnTriggerEnter(Collider other)
    {
        //���肪Player��������
        if (other.gameObject.tag == "Player")
        {
            //�����_���[���\���ɂ���
            renderer.enabled = false;

            
            //Player��Coinpuls�����s
            player.Coinplus();

        }
        if (other.gameObject.tag == "DestroyArea")
        {
            //�����_���[���\���ɂ���
            //gameObject.SetActive(true);
            renderer.enabled = true;

        }
    }
}
