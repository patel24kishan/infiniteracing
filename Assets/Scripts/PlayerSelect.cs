using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    
    
    [SerializeField] private GameObject _PlayerSelectPage;
 
    [SerializeField] private Sprite _PlayerSelectSprite;
    [SerializeField]   private Sprite  _PlayerSprite;

    private void Start()
    {
       // _PlayerSelectSprite = this.transform.GetChild(0).GetComponent<Image>().sprite;
       // _PlayerSprite = Game_Controller.instance._Player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }

    public void Player(int num)
    {
        Game_Controller.instance._Player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
            this.transform.GetChild(0).GetComponent<Image>().sprite;
        _PlayerSelectPage.SetActive(false);
        PlayerPrefs.SetInt("CarNum",num);
     
    }
 
}