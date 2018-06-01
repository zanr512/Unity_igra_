using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Igralec : MonoBehaviour {

    private Rigidbody2D rigid;

    public AudioSource skok;

    public int hitrost;

    public Canvas c;
    public Text score;

    bool portal = false;


    bool dotik = true; //ali se je dotaknil tal

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;

        c.enabled = true;
        score.text = "POSKUS " + PlayerPrefs.GetInt("poskus");
        Destroy(score, 1.5f);

    }

    // Update is called once per frame




	void Update () {


        //SKOČI
        if ((Input.GetMouseButtonDown(0) || Input.gyro.userAcceleration.y > 0.3) && dotik == true && portal == false)
        {


            skok.Play();



            dotik = false;
            rigid.AddForce(new Vector2(0, 12), ForceMode2D.Impulse);
            //rigidbody2D.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            //rigid.velocity = new Vector2(7, 8);
            
        }

        //NORMALNO PREMIKANJE
        if (dotik && !portal)
        {
            rigid.velocity = new Vector2(12, -7);
        }

        //PREMIKANJE V ZRAKU
        if(!dotik && !portal)
        {
            Vector3 vel = rigid.velocity;
            vel.y -= 13f * Time.deltaTime;
            rigid.velocity = vel;
        }


        if(portal)
        {
            transform.Translate(0, -Input.acceleration.x, 0);
            rigid.velocity = new Vector2(12, 0);
        }

        if (transform.position.y < -7)
            pristejPoskus();



        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("nastavim true");
        if (coll.transform.tag == "tla")
            dotik = true;
        else if (coll.transform.tag == "ovira")
            pristejPoskus();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "portal" && !portal)
        {
            portal = true;
        }
        else if(other.tag == "portal" && portal)
        {
            portal = false;
        }
    }

    void pristejPoskus()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        int i = PlayerPrefs.GetInt("poskus");
        i++;
        PlayerPrefs.SetInt("poskus", i);
        PlayerPrefs.Save();
        //Debug.Log("Poskus: " + PlayerPrefs.GetInt("poskus"));
    }

}
