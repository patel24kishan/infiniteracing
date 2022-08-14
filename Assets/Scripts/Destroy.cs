using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    private void Start()
    {

    }

    void Update()
    {
        if (transform.position.y < -47)
        {
            Destroy(this.gameObject);
            
        }
        
        if (this.gameObject.tag == "Coin_GO")
        {
            if (this.gameObject.transform.childCount == 0)
                Destroy(this.gameObject);
            else
            {
                Destroy(this.gameObject,5.0f);
            }
        }
    }


private void OnCollisionEnter2D(Collision2D other1)
    {
        if (other1.gameObject.tag == "Player")
        {
            Game_Controller.instance.GameOver();
        }
    }


}
