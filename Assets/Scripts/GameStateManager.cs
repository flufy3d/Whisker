using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {

    public float OneCheeseTime = 5.0f;

    public float CheckInterval = 0.1f;

    public Text JerryNumText;
    public Text CheeseNumText;
    public Text TimerText;
    public Text WinnerText;


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
        isInGame = false;
    }
    void UpdataUI(int _jerryNum,int _chesseNum, float _timeCountDown , string _winner)
    {
        JerryNumText.text = string.Format("Jerry: {0}", _jerryNum);

        CheeseNumText.text = string.Format("Cheese: {0}", _chesseNum);

        if (_timeCountDown < OneCheeseTime)
        {
            TimerText.text = string.Format("Timer: {0:F2} s", _timeCountDown);
        }
        if(_winner != "")
        {
            WinnerText.text = string.Format("Winner: {0}", _winner);
        }

    }
    IEnumerator UpdateState()
    {
        while (isInGame)
        {
            int JerryNum = GameObject.FindGameObjectsWithTag("Jerry").Length;
            int CheeseNum = GameObject.FindGameObjectsWithTag("Coin").Length;

            if(JerryNum == 0)
            {
                winner = "Tom";
                EndGame();
            }
            if(CheeseNum == 0)
            {
                winner = "Jerry";
                EndGame();
            }
            if (JerryNum >= 1 && CheeseNum == 1)
            {
                timeCountDown = timeCountDown - CheckInterval;
                if(timeCountDown < 0)
                {
                    winner = "Jerry";
                    EndGame();
                }
            }

            UpdataUI(JerryNum, CheeseNum ,timeCountDown , winner);




            yield return checkIntervalTime;

        }
    }

}
