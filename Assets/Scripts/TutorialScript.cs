using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

private int loopNum;
private GameObject[] allTutImages;

	void Start () {
		Time.timeScale=0;
		allTutImages=new GameObject[transform.childCount];
		loopNum=0;
		for(int i=0; i<allTutImages.Length; i++){
			allTutImages[i]=transform.GetChild(i).gameObject;
			if(i==loopNum){
				allTutImages[i].SetActive(true);
			}
		}
	}
	
	void Update () {
		if(Input.GetButtonDown("Jump") || Input.GetButtonDown("JumpJerry")){
			
			if(loopNum<allTutImages.Length-1){
				loopNum++;	
				for(int i=0; i<allTutImages.Length; i++){
					if(i==loopNum){
						allTutImages[i].SetActive(true);
					}else{
						allTutImages[i].SetActive(false);
					}
				}
			}else{
				Time.timeScale=1;
				gameObject.SetActive(false);
			}
		}
	}
}
