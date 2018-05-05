using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremikanjeOdzadja : MonoBehaviour {

    public GameObject igralec;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 x = new Vector3(igralec.transform.position.x, 0, -10);
        transform.position = x;
	}
}
