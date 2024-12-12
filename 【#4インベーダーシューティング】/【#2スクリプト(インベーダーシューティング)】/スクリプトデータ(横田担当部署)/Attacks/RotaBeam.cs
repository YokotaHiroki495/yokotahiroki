using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaBeam : MonoBehaviour
{
    //“ã‚¬•¥‚¢ƒr[ƒ€UŒ‚‚Ìˆ—
    [SerializeField] public float BulletSpeed = 0.05f;
    [SerializeField] public float RotationSpeed = 30;


    float AngelX;
    float DesAngelX;

    public float DestroyTime = 3;
    private void Start()
    {
        


        Destroy(gameObject, DestroyTime);


    }


    // Update is called once per frame
    void Update()
    {
        //¶¬‚µ‚½êŠ‚Å‰ñ“]‚³‚¹‚é
        transform.Rotate(RotationSpeed * Time.deltaTime, 0, 0);
    }


    private void OnTriggerStay(Collider col)
    {
        //“G‚É“–‚½‚Á‚½‚ç“G‚ğ”j‰ó
        if (col.gameObject.tag == "Enemy" && col.gameObject.tag == "Enemy2")
        {
            Destroy(col.gameObject);
        }
    }
}