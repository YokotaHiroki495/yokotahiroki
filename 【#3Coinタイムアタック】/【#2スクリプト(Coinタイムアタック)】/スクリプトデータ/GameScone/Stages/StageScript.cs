using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScript : MonoBehaviour
{
    
    Vector3 myPosition;

    //Area‚ðˆÚ‘—‚³‚¹‚é‹——£
    [SerializeField] int positionzPulsNumber = 200;

    //StageScript stageScript;
    CoinScript coinScript;

    void Start()
    { 
        coinScript = GameObject.Find("Coin").GetComponent<CoinScript>();
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("DestroyArea"))
        {
            myPosition = transform.position;

            gameObject.transform.position = new Vector3(myPosition.x, myPosition.y, myPosition.z + 200);
           

        }

    }



}
