using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Movement : MonoBehaviour
{
    public float Speed = 40f;
    private bool isright, isclicked;
   // [SerializeField] private Rigidbody2D rb;

    private Vector3 mousePos;
    private Vector2 objectPos;
    private float distance = 10;
    [SerializeField] private AudioSource _CarSource, _GameAreaSource;

    void Start()
    {

    }

    void Update()
    { Vector2 pos = transform.position;
        objectPos.x = Mathf.Clamp(transform.position.x, -7.6f, 10.5f);
        objectPos.y = Mathf.Clamp(transform.position.y, -22, 23);
        transform.position = objectPos;

       // OnMouseDrag();
    }
    void OnMouseDrag()
    {
        mousePos = new Vector2(Input.mousePosition.x,0);
        objectPos =new Vector2(Camera.main.ScreenToWorldPoint(mousePos).x,transform.position.y);

            transform.position = objectPos;
    
    }
   
    
    private void OnCollisionEnter2D(Collision2D other1)
    {
        if (other1.gameObject.tag == "Respawn")
        {
            _GameAreaSource.Stop();
            _CarSource.Play();
            Game_Controller.instance.GameOver();
        }
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      //  other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if (other.gameObject.tag == "Road1")
        {
            //Debug.Log("rd 1 hit");
            Game_Controller.instance.Spawn = true;
        }
        else if (other.gameObject.tag == "Road2")
        {
           // Debug.Log("rd 2 hit");
            Game_Controller.instance.Spawn = false;
        }
        
        else if (other.gameObject.tag == "Coin")
        {
            // Debug.Log("rd 2 hit");
           Game_Controller.instance.UpdateCoins();
            Destroy(other.gameObject);
        }
        
        else if (other.gameObject.tag == "Bag")
        {
            // Debug.Log("rd 2 hit");
            for (int i = 0; i < 14; i++)
            {
                Game_Controller.instance.UpdateCoins();
            }
            Destroy(other.gameObject);
        }
    }
}


