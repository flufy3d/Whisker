using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

public Sprite[] catWinImages;
public Sprite[] ratWinImages;
private Image winImage;
    public float OneCheeseTime = 5.0f;

    public float CheckInterval = 0.1f;

    public Text JerryNumText;
    public Text CheeseNumText;
   // public Text TimerText;
    //public Text WinnerText;

public GameObject winCanvas;
    private float timeCountDown;
    private WaitForSeconds checkIntervalTime;
    private bool isInGame;
    private string winner;
    private int pressButton;



    void Start () {
    winImage=winCanvas.transform.GetChild(0).GetComponent<Image>();
    }


    public void StartGame()
    {
        winner = "";
        timeCountDown = OneCheeseTime;
        checkIntervalTime = new WaitForSeconds(CheckInterval);

        isInGame = true;
        StartCoroutine(UpdateState());

    }

    public void EndGame()
    {   
        if(isInGame == false){
        // Time.timeScale=0;
        // winCanvas.SetActive(true);
        if(winner=="Cat"){
           // winImage.sprite=catWinImages[Random.Range(0, catWinImages.Length-1)];
            if(Input.GetButtonDown("Jump")){
                pressButton++;
                if(pressButton>1){
                    Time.timeScale=1;
                    Application.LoadLevel(Application.loadedLevel);  
                }
                
        }
        }else{
           //winImage.sprite=ratWinImages[Random.Range(0, ratWinImages.Length-1)];
            if(Input.GetButtonDown("JumpJerry")){
                if(pressButton>1){
                    Time.timeScale=1;
                    Application.LoadLevel(Application.loadedLevel);  
                }
            }
        }
        
        }
    }
    void UpdataUI(int _jerryNum,int _chesseNum)
    {
        JerryNumText.text = _jerryNum.ToString();

        CheeseNumText.text = _chesseNum.ToString();

        // if (_timeCountDown < OneCheeseTime)
        // {
        //     TimerText.text = string.Format("Timer: {0:F2} s", _timeCountDown);
        // }
        
        // if(_winner != "")
        // {
        //     WinnerText.text = string.Format("Winner: {0}", _winner);
        // }

    }
    void LateUpdate(){
        EndGame();
    }
    IEnumerator UpdateState()
    {
        while (isInGame)
        {
            int JerryNum = GameObject.FindGameObjectsWithTag("Jerry").Length;
            int CheeseNum = GameObject.FindGameObjectsWithTag("Coin").Length;

            if(JerryNum == 0)
            {
                winner = "Cat";
                winImage.sprite=catWinImages[Random.Range(0, catWinImages.Length-1)];
                Time.timeScale=0;
                winCanvas.SetActive(true);
                isInGame=false;
            }
            if(CheeseNum == 0)
            {
                winner = "Rats";
                winImage.sprite=ratWinImages[Random.Range(0, ratWinImages.Length-1)];
                Time.timeScale=0;
                winCanvas.SetActive(true);
                isInGame=false;
            }
            // if (JerryNum >= 1 && CheeseNum == 1)
            // {
            //     timeCountDown = timeCountDown - CheckInterval;
            //     if(timeCountDown < 0)
            //     {
            //         winner = "Rats";
            //         isInGame=false;
            //     }
            // }

            UpdataUI(JerryNum, CheeseNum );




            yield return checkIntervalTime;

        }
    }

}
