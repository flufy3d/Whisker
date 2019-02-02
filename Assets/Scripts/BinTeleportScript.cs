using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinTeleportScript : MonoBehaviour {

private List<GameObject> allBins=new List<GameObject>();
private ParticleSystem myParticle;
private Animator myAnimator;
private float time;
private GameObject inTriggerObj;
private int randomNumber;
	void Start () {
		allBins.AddRange(GameObject.FindGameObjectsWithTag("bin"));
		allBins.Remove(gameObject);
		myAnimator=GetComponent<Animator>();
		myParticle=transform.GetChild(0).GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider col){
		if(col.gameObject.CompareTag("Jerry") || col.gameObject.CompareTag("Tom")){
			time+=Time.deltaTime;
			if(inTriggerObj==null){
				myParticle.emissionRate=30;
				//myAnimator.SetBool("isMoving", true);
				randomNumber=Random.Range(0,allBins.Count);
				inTriggerObj=col.gameObject;
			}
			if(time>=2.5f){
				myParticle.emissionRate=0;
				time=0;
				inTriggerObj.transform.position=allBins[randomNumber].transform.position;
				//myAnimator.SetBool("isMoving", false);
				inTriggerObj=null;
			}
		}
	}

	void OnTriggerExit(Collider col){
		if(inTriggerObj==col.gameObject){
			Debug.Log("exit bin");
			myParticle.emissionRate=0;
			//myAnimator.SetBool("isMoving", false);
			inTriggerObj=null;
		}
	}



}
