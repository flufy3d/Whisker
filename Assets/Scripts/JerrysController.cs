using UnityEngine;
using System.Collections;

//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>
public class JerrysController : MonoBehaviour {

public GameObject cheeseParticle;
private float verticalVelocity;
private float horizontalVelocity;
public float jerryMaxSpeed=12f;
private Animator mAnimator;
	public AudioClip JumpSound = null;
	public AudioClip HitSound = null;
	public AudioClip CoinSound = null;
	private Rigidbody mRigidBody = null;
	private AudioSource mAudioSource = null;
	private bool mFloorTouched = false;
	public float jerrySpeedScale=400f;
	private Vector3 previousPos;
	private float distance;
	private Vector3 lookRot;
	private Quaternion LookTo;


	void Start () {
		mRigidBody = GetComponent<Rigidbody> ();
		mAudioSource = GetComponent<AudioSource> ();
		mAnimator= transform.GetChild(0).GetComponent<Animator>();
		mAnimator.SetBool("isWalk", false);
		previousPos=transform.position;
		distance=Vector3.Distance(transform.position, previousPos);
	}

	void FixedUpdate () {
		verticalVelocity=Mathf.Abs(mRigidBody.velocity.z);
		horizontalVelocity=Mathf.Abs(mRigidBody.velocity.x);
		if (mRigidBody != null) {
			if (Input.GetAxis ("VerticalJerry")!=0 && verticalVelocity<jerryMaxSpeed) {
				mRigidBody.AddForce(Vector3.forward * Input.GetAxis("VerticalJerry")*jerrySpeedScale);
			
			}
			if (Input.GetAxis ("HorizontalJerry")!=0 && horizontalVelocity<jerryMaxSpeed) {
				mRigidBody.AddForce(Vector3.right * Input.GetAxis("HorizontalJerry")*jerrySpeedScale);
			
			}
			if(Input.GetAxis("VerticalJerry")==0){
				mRigidBody.velocity=new Vector3(mRigidBody.velocity.x,mRigidBody.velocity.y,0);			
			}
			if(Input.GetAxis("HorizontalJerry")==0 ){
				mRigidBody.velocity=new Vector3(0,mRigidBody.velocity.y,mRigidBody.velocity.z);
			}
			if(mRigidBody.velocity.magnitude<0.1f){
				mAnimator.SetBool("isWalk", false);
			}else{
				mAnimator.SetBool("isWalk", true);
			}
			
			RotateCharacter();

		}
	}

void RotateCharacter(){
	if(Input.GetAxis("HorizontalJerry")!=0 || Input.GetAxis("VerticalJerry")!=0){
	lookRot=new Vector3( Input.GetAxis("HorizontalJerry"), 0,Input.GetAxis("VerticalJerry"));
	LookTo=Quaternion.LookRotation(lookRot);
	transform.rotation = Quaternion.Slerp(transform.rotation, LookTo, 10 * Time.deltaTime);
	}
	

}

	void OnCollisionEnter(Collision coll){
		// if (coll.gameObject.tag.Equals ("Floor")) {
		// 	mFloorTouched = true;
		// 	if (mAudioSource != null && HitSound != null && coll.relativeVelocity.y > .5f) {
		// 		mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
		// 	}
		// } else {
		// 	if (mAudioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f) {
		// 		mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
		// 	}
		// }
	}

	void OnCollisionExit(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Coin")) {
			//jerryMaxSpeed+=1.5f;
			if(mAudioSource != null && CoinSound != null){
				mAudioSource.PlayOneShot(CoinSound);
			}
			Instantiate(cheeseParticle, other.transform.position, other.transform.rotation);
			Destroy(other.gameObject);
		}
	}
}
