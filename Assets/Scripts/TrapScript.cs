using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour {


private Animator myAnimator;
private GameObject myParticle;
private bool isShaking;
private bool isAttack;
public float attackTime=1f;
private float setBackTime=2f;
private bool canAttack=true;
private float time;
private List<GameObject> ratsInTrap=new List<GameObject>();
	
	void Start () {
		SetInitialReferences();
	}
	
	void Update () {
		DoShaking();
		ResetTrap();
	}

	void SetInitialReferences(){
		myAnimator=GetComponent<Animator>();
		myParticle=transform.GetChild(5).gameObject;
	}

	void OnTriggerStay(Collider col){
		if(col.CompareTag("Jerry") && !ratsInTrap.Contains(col.gameObject) && canAttack){
			isShaking=true;
			myAnimator.SetBool("isShaking", isShaking);
			ratsInTrap.Add(col.gameObject);
		}
	}


	void OnTriggerExit(Collider col){
		if(col.CompareTag("Jerry")){
			ratsInTrap.Remove(col.gameObject);
			if(ratsInTrap.Count==0){
				isShaking=false;
				myAnimator.SetBool("isShaking", isShaking);
			}
		}
	}

	void DoShaking(){
		if(isShaking){
			time+=Time.deltaTime;
			if(time>=attackTime){
				isAttack=true;
				myAnimator.SetBool("isAttack", isAttack);
				myParticle.SetActive(true);
				for(int i=0; i<ratsInTrap.Count;i++){
					Destroy(ratsInTrap[i]);
				}
				canAttack=false;
				time=0;	
				isShaking=false;
			}
		}
	}

void ResetTrap(){
	if(!canAttack){
		time+=Time.deltaTime;
		if(time>=setBackTime){
			isAttack=false;
			isShaking=false;
			myAnimator.SetBool("isShaking", isShaking);
			myAnimator.SetBool("isAttack", isAttack);
			time=0;
			myParticle.SetActive(false);
			canAttack=true;
			
		}		
	}
}

}
