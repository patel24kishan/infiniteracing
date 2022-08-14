using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public static Game_Controller instance;

    
   
    [SerializeField] private GameObject Road1,
                                        Road2;
  
    [SerializeField] private GameObject RdPos1,
                                         RdPos2;
   
    [SerializeField] private GameObject[] Traffic;
    [SerializeField] private GameObject[] Coins;
    
    [SerializeField] private Sprite[] _PlayerSelectSprite;
    
    [SerializeField] private GameObject _Traffic,
                                         _Coins;
   
    [SerializeField] private GameObject _MoneyBag;
    [SerializeField] private GameObject _GameArea;
    [SerializeField] private GameObject _PauseButton;
    [SerializeField] private GameObject _PlayerSelectButton;

    public GameObject _Player;
    
    [SerializeField] private GameObject _GameOverPage; 
    [SerializeField] private GameObject _HomePage;
    [SerializeField] private GameObject _PausePage;
    [SerializeField] private GameObject _PopUpPage;
    [SerializeField] private GameObject _LoadingPage;
    [SerializeField] private GameObject _Score_Pause_Coin;
    [SerializeField] private GameObject _PlayerSelectPage;
    
    
    [SerializeField] private Text _CoinScore,
                                  _Score,HighScore,
                                  TotalCoin;
    
    [SerializeField] private float[] XPosTraffic_Coin;
   
    private float Gravity;
    private float TimeT,
                  Score, 
                  Temp;
    
    private int xvalue,
                xcvalue,
                HomeFlag;
  
    private int flag;
  
    private int CoinsScore,
                TempCoin;
    public bool GamePlay;

    public bool Spawn,
                Pause;
               
   
    void Start()
    {
        
        instance = this;

        _Player.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _PlayerSelectSprite[PlayerPrefs.GetInt("CarNum",0)];
        Pause = true;
        
                                // U P D A T E   S C O R E 
        InvokeRepeating("UpdateScore",1f,.25f);

      

       PlayerPrefs.DeleteKey("Start");

        StartCoroutine(SceneLoadDelay());
        
        _GameArea.SetActive(false);
        _GameOverPage.SetActive(false);
       // _LoadingPage.SetActive(false);
        _PausePage.SetActive(false);
        _Score_Pause_Coin.SetActive(false);
                       
        HighScore.text = PlayerPrefs.GetFloat("HighScore",0.0f).ToString();
        TotalCoin.text= PlayerPrefs.GetInt("TotalCoin",0).ToString();

        HomeFlag = PlayerPrefs.GetInt("Start", 1);
        if (flag == 1)
        {
            Debug.Log("hompage delay!!");
             
              StartCoroutine(HomepageDelay());
           // StartCoroutine(SceneLoadDelay());
            PlayerPrefs.SetInt("retry",0);
        }
        else
        {
            Debug.Log("homepage delay stop!!");
            _HomePage.SetActive(false);
            StopCoroutine(HomepageDelay());
        }
        
        flag = PlayerPrefs.GetInt("retry", 0);
      
        if (flag == 1)
        {
         
            PlayerPrefs.SetInt("retry",0);
            Play();
    
        }
        else
        {
            _HomePage.SetActive(true);
        }
        
    }

    IEnumerator SceneLoadDelay()
    {
        Debug.Log(" Start sceneLoadDelay!!"+Time.timeScale);
        _LoadingPage.SetActive(true);
        yield return new  WaitForSeconds(1.5f);
        {
            Debug.Log("Stop sceneLoadDelay!!");
            _LoadingPage.SetActive(false);
          
        }
    }
    
    IEnumerator HomepageDelay()
    {
            yield return new WaitForSeconds(10f);
            _HomePage.SetActive(true);  
    }
    
    void Update()
    {
        if (!Pause)
        {
            SpawnRoad();
        }
    }

    public void UpdateCoins()
    {
        if (!Pause)
        {
             CoinsScore++;
             _CoinScore.text = "" + CoinsScore;
        }
    }
    
    public void UpdateScore()
    {
        if (!Pause)
        {
            Temp++;
            _Score.text = "" +Temp.ToString();
        }
    }

    
    public void GameOver()
    {
        
        _GameOverPage.SetActive(true);
        
        Time.timeScale = 0;
        Pause = true;
        
        if(Temp > PlayerPrefs.GetFloat("HighScore",0.0f))
                 PlayerPrefs.SetFloat("HighScore",Temp);

        TempCoin = PlayerPrefs.GetInt("TotalCoin") + CoinsScore;
        PlayerPrefs.SetInt("TotalCoin",TempCoin);
    }
                                //    B U T T O N S
    
    public void Retry()
    {
      //  Debug.Log("retry!!");
        Play();
       // Time.timeScale = 1f;
         SceneManager.LoadScene("First");
         PlayerPrefs.SetInt("retry",1);
    }
    
    public void PlayerSelectButton()
    {
     //   Debug.Log("PlayerSelect!!");
        _PlayerSelectButton.SetActive(false);
        _PlayerSelectPage.SetActive(true);
        
        
    }
    
    public void PlayerSelect()
    {
       // _HomePage.SetActive(false);
        _PlayerSelectButton.SetActive(true);
        _PlayerSelectPage.SetActive(false); 
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        _Player.SetActive(false);
        _PauseButton.SetActive(false);
        _PausePage.SetActive(true);
    }

    public void Play()
    {
        StartCoroutine(TrafficSpawn());
        StartCoroutine(CoinSpawn());
        StartCoroutine(MoneyBagSpawn());
        
     //   Debug.Log("Play!!");
        Pause = false;
        Time.timeScale = 1f;
        _Score_Pause_Coin.SetActive(true);
        _GameArea.SetActive(true);
        _HomePage.SetActive(false);
    }
    
    public void Resume()
    {
      //  Debug.Log("Resume!!");
        Time.timeScale = 1f;
        _Player.SetActive(true);
        _PauseButton.SetActive(true);
        _PausePage.SetActive(false);
    }

    public void Home()
    {
      //  Debug.Log("Home!!");
     //  Pause = true;
      //  Debug.Log("home");
        Time.timeScale = 1;
        SceneManager.LoadScene("First");
        // StopCoroutine(HomepageDelay());
    }
    
   
   
    public void PopUp_Yes()
    {
            Application.Quit();
    }
    
    public void PopUp_No()
    {
        _PopUpPage.SetActive(false);
       _HomePage.SetActive(true);
    }
    public void Quit()
    {
        Debug.Log("Quit!!");
        _HomePage.SetActive(false);
        _PopUpPage.SetActive(true);
       
    }

   

    
    //        R O A D  S P A W N 

    public void SpawnRoad()
    {
        if (Spawn == true)
        {
            Road2.GetComponent<BoxCollider2D>().enabled = true;
            Road2.transform.position = RdPos1.transform.position;
        }


        else if (Spawn == false)
        {
            Road1.GetComponent<BoxCollider2D>().enabled = true;
            Road1.transform.position = RdPos2.transform.position;
        }
    }
    
    
    //        T R A F F I C  S P A W N 

    IEnumerator TrafficSpawn()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        {
            //  Debug.Log("kk");
            int randomPos = Random.Range(0, 10);
            int x = Random.Range(0, 4);
            int y = Random.Range(41,50);

            if (xvalue == x)
            {
                StartCoroutine(TrafficSpawn());


            }
            else
            {
                xvalue = x;

                Instantiate(Traffic[randomPos], new Vector3(XPosTraffic_Coin[x], y, 0),
                    Traffic[randomPos].transform.rotation,
                    _Traffic.transform);
                StartCoroutine(TrafficSpawn());
            }
        }

    }

    //        C O I N  S P A W N 

    IEnumerator CoinSpawn()
    {
        yield return new WaitForSeconds(Random.Range(3, 6));
        {
            Debug.Log("kk");
            int randomPos = Random.Range(0, 5);
            int x = Random.Range(0, 4);
            int y = Random.Range(55,60);
            Debug.Log("y= "+y);
            if (xcvalue == x)
            {
                StartCoroutine(CoinSpawn());


            }
            else
            {
                xcvalue = x;

                Instantiate(Coins[randomPos], new Vector3(XPosTraffic_Coin[x], y, 0),
                    Coins[randomPos].transform.rotation,
                    _Coins.transform);
                StartCoroutine(CoinSpawn());
            }
        }

    }
    
    //        C O I N B A G  S P A W N 

    IEnumerator MoneyBagSpawn()
    {

        yield return new WaitForSeconds(60);
        {
        int x = Random.Range(0, 4);
        int y = Random.Range(41,50);

        Instantiate(_MoneyBag, new Vector3(XPosTraffic_Coin[x], y, 0), Quaternion.identity);
        StartCoroutine(MoneyBagSpawn());
         }
    }

}