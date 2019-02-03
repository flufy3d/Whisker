using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

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



    void Start () {
    
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
        Time.timeScale=0;
        winCanvas.SetActive(true);
        if(winner=="Cat"){
            winCanvas.transform.GetChild(1).gameObject.SetActive(true);
            winCanvas.transform.GetChild(0).gameObject.SetActive(false);
            if(Input.GetButtonDown("Jump")){
                Time.timeScale=1;
                Application.LoadLevel(Application.loadedLevel);
        }
        }else{
            
            winCanvas.transform.GetChild(1).gameObject.SetActive(false);
            winCanvas.transform.GetChild(0).gameObject.SetActive(true);
            if(Input.GetButtonDown("JumpJerry")){
                Time.timeScale=1;
                Application.LoadLevel(Application.loadedLevel);
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
                isInGame=false;
            }
            if(CheeseNum == 0)
            {
                winner = "Rats";
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
