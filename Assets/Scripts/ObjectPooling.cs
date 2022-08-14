using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
   
    [SerializeField] private float Speed ;
   // private bool RoadScroll;
    private Vector2 startPos;

    private void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down *Speed*Time.deltaTime);
       transform.position=new Vector2(1.683f,transform.position.y);
    }
}
