using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNwayAttack : MonoBehaviour
{
    //BossN-Way攻撃のグループの弾呼び出し処理


    [SerializeField] GameObject boss3WayAttackBullet;
    int wayNumber;
    [SerializeField] int wayNumberlimit;



    GameObject[] create3WayBullets;

    //[SerializeField] Boss3wayAttackBulletScript boss3WayAttackBulletScript; //3Way攻撃

    //自分自身を返すプール
    BossNwayAttackPool boss3wayAttackpool;
    //[SerializeField] GameObject seriaboss3wayAttackpool;


    GameObject createBossF3WayPos;
    //弾のプール
    BossNWayBulletPool boss3WayBulletPool;



    void OnEnable()
    {
        transform.position = new Vector2(0, 1);

    }
    void Start()
    {
        
        
        //boss3WayBulletPool = gameObject.GetComponent<Boss3WayBulletPool>();
        //GameObject createBossFirePos = boss3WayBulletPool.Get3WayBullet();

        boss3WayBulletPool = GameObject.Find("BossEnemy").GetComponent<BossNWayBulletPool>();
        boss3wayAttackpool = GameObject.Find("BossEnemy").GetComponent<BossNwayAttackPool>();

        Debug.Log("Boss3WayAttackScript:First");
        Create3WayBullet();
        Debug.Log("Boss3WayAttackScript:Second");
    }

    void Create3WayBullet()
    {
        Debug.Log("Boss3WayAttackScript:Third");
        createBossF3WayPos = boss3WayBulletPool.Get3WayBullet();

        Debug.Log("Boss3WayAttackScript:555");

     

    }


    void Update()
    {

        create3WayBullets = GameObject.FindGameObjectsWithTag("Boss3WayBullet");

            if (create3WayBullets.Length == 0)
            {
            HitBoxDestroy();

            }

       
    }

    void HitBoxDestroy()
    {
        boss3wayAttackpool.Del3Way(gameObject);
    }

   
   

}
