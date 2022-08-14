using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour {

	public GameObject player;		
	//private Vector2 offset;			
	
	void Start () 
	{
		
	}
	
	
	void LateUpdate () 
	{
        
       // transform.position = new Vector2(1.5f, player.transform.position.y);

        transform.position = new Vector3(0, player.transform.position.y,-10f);
    }
}
