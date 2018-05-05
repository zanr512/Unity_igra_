using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igralec : MonoBehaviour {

    private Rigidbody2D rigid;

    bool dotik = true; //ali se je dotaknil tal

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && dotik == true)
        {/*
            Vector3 misPoz3D = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector2 misPoz2D = new Vector2(misPoz3D.x, misPoz3D.y);



            Vector2 dir = Vector2.zero;
            Debug.Log(misPoz2D.ToString());
            RaycastHit2D hit = Physics2D.Raycast(misPoz2D, dir);
            */
            rigid.velocity = new Vector2(7, 7);
            dotik = false;
        }
        else if(dotik)
        {
            rigid.velocity = new Vector2(7, 0);
        }
            



        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("nastavim true");
        dotik = true;
    }

}
