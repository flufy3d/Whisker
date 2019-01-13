using UnityEngine;
using System.Collections;

public class TomController : MonoBehaviour {

	private float verticalVelocity;
	private float horizontalVelocity;

	public float tomMaxSpeed=12f;
	public AudioClip JumpSound = null;
	public AudioClip HitSound = null;
	public AudioClip CoinSound = null;

	private Rigidbody mRigidBody = null;
	private AudioSource mAudioSource = null;
	private bool mFloorTouched = false;

	public float tomSpeedScale=100f;
	void Start () {
		mRigidBody = GetComponent<Rigidbody> ();
		mAudioSource = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {
		verticalVelocity=Mathf.Abs(mRigidBody.velocity.z);
		horizontalVelocity=Mathf.Abs(mRigidBody.velocity.x);
		if (mRigidBody != null) {
			if (Input.GetButton ("VerticalTom") && verticalVelocity<tomMaxSpeed) {
				mRigidBody.AddForce(Vector3.forward * Input.GetAxis("VerticalTom")*tomSpeedScale);
			}
			if (Input.GetButton ("HorizontalTom") && horizontalVelocity<tomMaxSpeed) {
				mRigidBody.AddForce(Vector3.right * Input.GetAxis("HorizontalTom")*tomSpeedScale);
			}
			if(Input.GetAxis("VerticalTom")==0){
				mRigidBody.velocity=new Vector3(mRigidBody.velocity.x,mRigidBody.velocity.y,0);
			}
			if(Input.GetAxis("HorizontalTom")==0 ){
				mRigidBody.velocity=new Vector3(0,mRigidBody.velocity.y,mRigidBody.velocity.z);
			}
		}
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = true;
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.y > .5f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}
		} else {
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}
		}
	}

	void OnCollisionExit(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Coin")) {
			if(mAudioSource != null && CoinSound != null){
				mAudioSource.PlayOneShot(CoinSound);
			}
			Destroy(other.gameObject);
		}
	}
}
