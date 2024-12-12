using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //-----�ړ��֘A-----
    [Header("�ړ��X�s�[�h�֘A")]
    //�ړ��X�s�[�h
    [SerializeField]private float moveSpeed;
    //�����x
    [SerializeField]private float acceleration;
    //���E�ړ��̃X�s�[�h
    public float leftRightSpeed;
    //���E�ړ��̈ړ�����
    [SerializeField]private float limit;
    //public Animator animator;


    [Space(50)]
    [Header("�ړ��֘A")]
    //----�X���C�v�ړ�����----
    //�X���C�v���J�n�����|�W�V����

    Vector3 StartTouchPos;

    //X�����̃X���C�v
    //Vector3 XStartTouchXPos;

    //Y�����̃X���C�v
    //Vector3 YStartTouchYPos;

    //�X���C�v���C�������|�W�V����
    Vector3 EndTouchPos;

    //�ǂꂾ��X���W���X���C�v������
    float flickValue_X;

    //�ǂꂾ��Y���W���X���C�v������
    float flickValue_Y;

    //�ǂꂾ��X���W���X���C�v������ړ����邩
    [SerializeField] float SwipMoveXDis;

    //�ǂꂾ��Y���W���X���C�v������_�b�V�����邩
    [SerializeField] float SwipMoveYDis;

    //�w�肵���l�ȏ�ɃX���C�v�����猟�m����
    [SerializeField] float Swip_Distance_Detection;

    //�_�b�V�����Ă��邩�ۂ�
    public bool DashNow;

    //�L���������E�ړ��������ǂ���
    public bool MoveNow;



    //�������̃G�t�F�N�g
    [SerializeField] GameObject DashEffect;
    

    //�������N�[���^�C��
    private int AcceleItem;


    //-----�W�����v�֘A-----
    private Vector3 jump;


    //�W�����v��
    [SerializeField]
    private float jumpForce;


    //�ݒu����
    private bool isGrounded;
    Rigidbody rb;


    //�擾�R�C����
    public static int HubCoin;


    //�擾�A�C�e����
    public static int HubItem;


    [SerializeField]private  TextMeshProUGUI ItemNumText;
    
    //�A�C�e���}�C�i�X��
    //GamaManager�̕��ł��擾���āA�ꊇ�ŕҏW�ł���悤��
    public int MinusItemNum;


    //���݈ʒu�̎擾
    private float InstansPos;
    private float NewInstansPos;
    Vector3 PlayerYPos;

    //GAmeManager���擾
    public GameManager gameManager;

    //���C���J�����̃X�N���v�g���擾
    public CameraScript camerascript;
    
    //�������֘A
    //��������
    [SerializeField]float Insdis;
    //�ǂꂾ��������邩
    int currentZ = 0;

    ////[SerializeField] int forward = 10;

    private const int createcooltime = 10;


    //�A�j���[�V�����֌W
    Animator  Anima;
    [SerializeField] GameObject unitychan;

    //�G�t�F�N�g�֌W
    [SerializeField] Image Effect;


    //���֌W
    public AudioClip JumpSE;
    public AudioClip CoinSE;
    public AudioClip DamegeSE;
    public AudioClip damegeVoice;
    AudioSource audioSource;

    // �_���[�W���󂯂����̓_�ŏ���
    [Header("DamegeEffect")]
    [SerializeField] float flashInterval;    //�_�ł̊Ԋu
    [SerializeField] int loopCount; //�_�ł̃��[�v�J�E���g
    Collider collider;
    Renderer renderer;
    Material material;

    SkinnedMeshRenderer chilSkinnedMexhRenderer;        //���g�̎q�I�u�W�F�N�g
    SkinnedMeshRenderer grondchilSkinnedMexhRenderer;   //���g�̑��I�u�W�F�N�g
    SkinnedMeshRenderer greatGreatGrandSon;             //���g�̌����I�u�W�F�N�g
    //AudioSource audioSource;

    public bool isHit; //�����������ǂ����̃t���O

    void Start()
    {
        //RigidBody���擾
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);


        Anima = unitychan.GetComponent<Animator>();


        //�ŏ��̈ʒu��ۑ�
        InstansPos = transform.position.z;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        //�G�t�F�N�g�p�̐F���擾
        Effect.color = Color.clear;

        //��
        audioSource = GetComponent<AudioSource>();


        //�����R�C����(HubCoin�̃��Z�b�g)
        HubCoin = 0;


        //�����A�C�e����
        HubItem = 0;
        //ItemNumText.text = "AccelItem:" + HubItem;

        //�J�������擾
        camerascript = GameObject.Find("Main Camera").GetComponent<CameraScript>();


        PlayerYPos = transform.position;

        //�ݒu����
        isGrounded = true;

        collider = GetComponent<CapsuleCollider>();
        renderer = GetComponent<MeshRenderer>();
        material = GetComponent<Material>();

        chilSkinnedMexhRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        grondchilSkinnedMexhRenderer = chilSkinnedMexhRenderer.GetComponentInChildren<SkinnedMeshRenderer>();
        greatGreatGrandSon�@= grondchilSkinnedMexhRenderer.GetComponentInChildren<SkinnedMeshRenderer>();

    }


    void Update()
    {
        //Debug.Log(isGrounded);
        SwipeMove();
        Move();

        
        //�G�t�F�N�g�̐F
        //�G�t�F�N�g�̐F����ɓ����ɂ���
        //�F�́A��Q���ɓ����������ɕύX����
        Effect.color = Color.Lerp(Effect.color, Color.clear, Time.deltaTime);

        if (isGrounded == true)
        {
            
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
        

    }




    //�ړ�����
    void Move()
    {
        //float pos_x = transform.position.x;

        //�Q�[������timescale�𑝂₷���ƂŁA�A�j���[�V�����Ȃǂ̃X�s�[�h�������Ȃ�
        //�͈͂��P�{�`3�{�̊Ԃɐݒ�
        Time.timeScale = Mathf.Clamp(1 + acceleration * HubCoin,�@1,3);

        //���Ɉړ����鏈��
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed/*, Space.World*/;
        


        //PC�ł̑���
        //���[���ړ�
        //���ړ�
        if (Input.GetKeyDown(KeyCode.LeftArrow )|| Input.GetKeyDown(KeyCode.A))
        {
            if (transform.position.x > -3)
            {
                transform.position += new Vector3((float)-3, 0, 0);
            }

        }
        
        


        //�E�ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D))
        {
            
            if (transform.position.x < 3)
            {
               
                transform.position += new Vector3((float)3, 0, 0);
            }

        }

        //��������(��)
        if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            if (!DashNow)
            {
                DashNow = true;
                //���C���J�����ɂ��Ă���J�����X�N���v�g�̃R���[�`�������s
                camerascript.StartCoroutine("CameraMove");

                //RigidBody�ɏՌ��������ĉ���
                rb.AddForce(0f, 0f, 10f, ForceMode.Impulse);

                //DashNow = false;
            }

            ////���C���J�����ɂ��Ă���J�����X�N���v�g�̃R���[�`�������s
            //camerascript.StartCoroutine("CameraMove");

            //    //RigidBody�ɏՌ��������ĉ���
            //    rb.AddForce(0f, 0f, 10f, ForceMode.Impulse);
           

        }

    }

    //�X���C�v���쎞�̋���
    void SwipeMove()
    {
        //�X���C�v�J�n���_�̍��W���擾
        if (Input.GetMouseButtonDown(0) == true)
        {
            //��ʂ�G�ꂽ�瑀��\��Ԃɂ���
            MoveNow = true;
            StartTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

        }

       

        if (Input.GetMouseButton(0) == true)
        {
            EndTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

            FlickDirection();
            GetDirection();
            //NewTestGetDirection();


        }




    }

    //�X���C�v���ꂽ�ʂ��v�Z
    void FlickDirection()
    {
        //X���W�̃X���C�v�ʂ��擾
        flickValue_X = EndTouchPos.x - StartTouchPos.x;

        //Y���W�̃X���C�v�ʂ��擾
        flickValue_Y = EndTouchPos.y - StartTouchPos.y;

        Debug.Log("X�X���C�v�F"+flickValue_X);

    }


    //���ȏ�X���C�v���ꂽ��ʒu�ړ�
    void GetDirection()
    {
        //�L�������ړ��\��ԂȂ�
        if (MoveNow)
        {

            //���ړ�
            if (flickValue_X < -SwipMoveXDis)
            {


                if (transform.position.x > -3)
                {
                    //���Ɉړ�����
                    transform.position += new Vector3(-3, 0, 0);

                    //GetMouseButton�������ہA���̂܂܃X���C�h��������ΘA���ňړ��ł���
                    //StartTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

                    //����\��Ԃ�false�ɕύX
                    MoveNow = false;
                }
            }


            //�E�ړ�
            if (flickValue_X > SwipMoveXDis)
            {

                if (transform.position.x < 3)
                {
                    transform.position += new Vector3(3, 0, 0);

                    //�ǉ�
                    //StartTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

                    MoveNow = false;

                }
            }

            //�_�b�V��
            //GetMouseButton�ɂ���ƁA��ɃX���C�h����ƘA���Ŏ��s�����B
            if (flickValue_Y > SwipMoveYDis)
            {
                Debug.Log("�_�b�V���F��");
                if (!DashNow)
                {
                    DashNow = true;
                    //���C���J�����ɂ��Ă���J�����X�N���v�g�̃R���[�`�������s
                    camerascript.StartCoroutine("CameraMove");

                    //RigidBody�ɏՌ��������ĉ���
                    rb.AddForce(0f, 0f, 10f, ForceMode.Impulse);

                    //DashNow = false;
                }
            }
        }

    }



    //���ȏ�X���C�v���ꂽ��ʒu�ړ�&�ړ�������͎w�𗣂��Ȃ��ƈړ��ł��Ȃ��悤�ɂ���
    //void NewTestGetDirection()
    //{
    //    //�L�������ړ��\��ԂȂ�
    //    if (MoveNow)
    //    {

    //        //���ړ�
    //        if (flickValue_X < -SwipMoveXDis)
    //        {


    //            if (transform.position.x > -3)
    //            {
    //                //���Ɉړ�����
    //                transform.position += new Vector3(-3, 0, 0);

    //                //GetMouseButton�������ہA���̂܂܃X���C�h��������ΘA���ňړ��ł���
    //                //StartTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

    //                //����\��Ԃ�false�ɕύX
    //                MoveNow = false;
    //            }
    //        }


    //        //�E�ړ�
    //        if (flickValue_X > SwipMoveXDis)
    //        {

    //            if (transform.position.x < 3)
    //            {
    //                transform.position += new Vector3(3, 0, 0);

    //                //�ǉ�
    //                //StartTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

    //                MoveNow = false;

    //            }
    //        }

    //        //�_�b�V��
    //        //GetMouseButton�ɂ���ƁA��ɃX���C�h����ƘA���Ŏ��s�����B
    //        if (flickValue_Y > SwipMoveYDis)
    //        {
    //            Debug.Log("�_�b�V���F��");
    //            if (!DashNow)
    //            {
    //                DashNow = true;
    //                //���C���J�����ɂ��Ă���J�����X�N���v�g�̃R���[�`�������s
    //                camerascript.StartCoroutine("CameraMove");

    //                //RigidBody�ɏՌ��������ĉ���
    //                rb.AddForce(0f, 0f, 10f, ForceMode.Impulse);

    //                //DashNow = false;
    //            }
    //        }
    //    }
    //}




//�^�b�v�����u�ԁA�����true�ɂ��āA�w�𗣂�����false�ɂ���


//�����蔻��(Trigger)�ɐڐG������
void OnTriggerEnter(Collider other)
    {
        //Obstacle(��Q��)�ɓ���������
        if (other.gameObject.tag == "Obstacle")
        {
            StartCoroutine(PlayerDamegeEffect());
            //�_���[�WSE���Đ�
            audioSource.PlayOneShot(DamegeSE);
            audioSource.PlayOneShot(damegeVoice);


            //�Q�[���}�l�[�W���[�̃X�N���v�g�̃}�C�i�X�̒l���ꏏ�ɕύX�ł���悤�ɒ���
            HubCoin -= MinusItemNum;
            if (HubCoin < 0)
            {
                HubCoin = 0;
            }

            //�Q�[���}�l�[�W���[�X�N���v�g��HPminus�ɃA�N�Z�X
            gameManager.MinusScore();

            //�G�t�F�N�g�̐F��ς���@�ԐF
            this.Effect.color = new Color(0.5f, 0f, 0f, 0.5f);

        }

    }

    //�R�C���擾���Ɏ��s
    public void Coinplus()
    {
        //�擾SE���Đ�
        audioSource.PlayOneShot(CoinSE);
        
        //�n�u�R�C����1���₷
        HubCoin += 1;

        //�Q�[���}�l�[�W���[�̃v���X�X�R�A�����s
        gameManager.PlusScore();

    }

 
    //�����蔻��(Collider)�ɓ���������
    void OnCollisionEnter(Collision collision)
    {
        //����̃^�O��"�X�e�[�W"��������
        if (collision.gameObject.tag == "Stage")
        {
            //�ڒn�����true�ɂ���
            isGrounded = true;
        }
    }

    //�����蔻��(Collider)���痣�ꂽ��
    void OnCollisionExit(Collision collision)
    {
        //����̃^�O��"�X�e�[�W"��������
        if (collision.gameObject.tag == "Stage")
        {
            //�ڒn�����false��
            isGrounded = false;

        }
    }



    IEnumerator PlayerDamegeEffect()
    {
        //�����蔻���ύX
        isHit = true;

        //�_�Ń��[�v
        for (int i = 0; i < loopCount; i++)
        {

            yield return new WaitForSeconds(flashInterval);

            //�����_���[���I�t
            //renderer.enabled = false;
            //rendrer.enabled = false;
            //collider.enabled = false;

            //material.color = material.color - new Color(0, 0, 0, 1);
            greatGreatGrandSon.enabled = false;

            yield return new WaitForSeconds(flashInterval);

            renderer.enabled = true;
            //collider.enabled = true;
            //material.color = material.color - new Color(0, 0, 0, 0);

            greatGreatGrandSon.enabled = true;
        }

        isHit = false;
    }

}
