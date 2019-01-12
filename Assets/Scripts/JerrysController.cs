using UnityEngine;
using System.Collections;

//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>
public class JerrysController : MonoBehaviour {

	public AudioClip JumpSound = null;
	public AudioClip HitSound = null;
	public AudioClip CoinSound = null;

	private Rigidbody mRigidBody = null;
	private AudioSource mAudioSource = null;
	private bool mFloorTouched = false;

	public float jerrySpeed=200f;


	void Start () {
		mRigidBody = GetComponent<Rigidbody> ();
		mAudioSource = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {
		if (mRigidBody != null) {
			if (Input.GetKey(KeyCode.LeftArrow)) {
				mRigidBody.AddTorque(Vector3.back * -1.0f * jerrySpeed);
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				mRigidBody.AddTorque(Vector3.back * 1.0f * jerrySpeed);
			}
			if (Input.GetKey(KeyCode.UpArrow)) {
				mRigidBody.AddTorque(Vector3.right * 1.0f * jerrySpeed);
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				mRigidBody.AddTorque(Vector3.right * -1.0f * jerrySpeed);
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
