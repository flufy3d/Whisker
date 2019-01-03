using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSpawner : MonoBehaviour {

	public GameObject TomPrefab = null;

	public GameObject JerryPrefab = null;

	public int jerrys_num = 4;

	public float spawn_x_min = 0;

	public float spawn_x_max = 30;

	public float spawn_z_min = 0;

	public float spawn_z_max = 30;

	Vector3 PickRandomPosition()
	{
		Vector3 pos = new Vector3(Random.Range(spawn_x_min, spawn_x_max), 2, Random.Range(spawn_z_min, spawn_z_max));
		return pos;
	}
	// Use this for initialization
	void Start () {
		GameObject tmp;
		tmp = Instantiate(TomPrefab,PickRandomPosition(), Quaternion.Euler(0,0,0)) as GameObject;
		tmp.transform.parent = transform;

		for(int i = 0;i < jerrys_num ;  i++)
		{
			tmp = Instantiate(JerryPrefab,PickRandomPosition(), Quaternion.Euler(0,0,0)) as GameObject;
			tmp.transform.parent = transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
