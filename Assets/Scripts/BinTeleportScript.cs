using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinTeleportScript : MonoBehaviour {

private List<GameObject> allBins=new List<GameObject>();
private ParticleSystem myParticle;
private Animator myAnimator;
private List<GameObject> jerriesInBin=new List<GameObject>();
private GameObject tomInBin;
private int randomNumber;
	void Start () {
		allBins.AddRange(GameObject.FindGameObjectsWithTag("bin"));
		allBins.Remove(gameObject);
		myAnimator=GetComponent<Animator>();
		myParticle=transform.GetChild(0).GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
			if(Input.GetButtonDown("Jump") && tomInBin!=null){
				tomInBin.transform.position=allBins[Random.Range(0,allBins.Count)].transform.position;
			}
			if(Input.GetButtonDown("JumpJerry") && jerriesInBin.Count>0){
				for(int i=0; i<jerriesInBin.Count; i++){
					if(jerriesInBin[i].activeSelf){
						jerriesInBin[i].transform.position=allBins[Random.Range(0,allBins.Count)].transform.position;
					}else{
						Destroy(jerriesInBin[i]);
					}
				}
			}
	}

	void OnTriggerStay(Collider col){
		if(col.gameObject.CompareTag("Jerry") && !jerriesInBin.Contains(col.gameObject)){
			jerriesInBin.Add(col.gameObject);
			myAnimator.SetBool("isMoving",true);
		}
		if(col.gameObject.CompareTag("Tom") && tomInBin==null){
			tomInBin=col.gameObject;
			myAnimator.SetBool("isMoving",true);
		}
	}

	void OnTriggerExit(Collider col){
		if(tomInBin==col.gameObject){
			myParticle.emissionRate=0;
			//myAnimator.SetBool("isMoving", false);
			//inTriggerObj=null;
			myAnimator.SetBool("isMoving",false);
			tomInBin=null;
		}else if(col.CompareTag("Jerry")){
			jerriesInBin.Remove(col.gameObject);
			if(jerriesInBin.Count==0){
				myAnimator.SetBool("isMoving",false);
				myParticle.emissionRate=0;
			}
		}
	}



}
