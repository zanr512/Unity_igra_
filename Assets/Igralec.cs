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
    bool portalG = false;


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
        if ((Input.GetMouseButtonDown(0) || Input.gyro.userAcceleration.y > 0.3) && dotik == true && portal == false && portalG == false)
        {


            skok.Play();



            dotik = false;
            rigid.AddForce(new Vector2(0, 12), ForceMode2D.Impulse);
            //rigidbody2D.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            //rigid.velocity = new Vector2(7, 8);
            
        }

        //NORMALNO PREMIKANJE
        if (dotik && !portal && !portalG)
        {
            rigid.velocity = new Vector2(12, -7);
        }

        //PREMIKANJE V ZRAKU
        if(!dotik && !portal  && !portalG)
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

        if (portalG)
        {
            if((Input.GetMouseButtonDown(0) || Input.gyro.userAcceleration.y > 0.3) && dotik == true)
            {
                skok.Play();
                dotik= false;
                rigid.velocity = new Vector2(12, 7);
                //rigid.AddForce(new Vector2(0, -12), ForceMode2D.Impulse);
            }
            if(!dotik)//v zraku
            {
                rigid.velocity = new Vector2(12, -7);
            }
            if(dotik)//noralno premikanje
            {
                rigid.velocity = new Vector2(12, -7);
            }
            
        }




        if (transform.position.y < -7)
            pristejPoskus();



        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        string url = "http://" + PlayerPrefs.GetString("url") + ":8080/koncan";
        //Debug.Log("nastavim true");
        if (coll.transform.tag == "tla")
            dotik = true;
        else if (coll.transform.tag == "ovira")
            pristejPoskus();
        else if (coll.transform.tag == "konec")
        {
            SceneManager.LoadScene("glavniMeni");
            WWWForm postForm = new WWWForm();
            postForm.AddField("tst", PlayerPrefs.GetString("name"));
            postForm.AddField("poskus", PlayerPrefs.GetInt("poskus"));
            WWW upload = new WWW(url, postForm);
        }
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
        else if(other.tag == "portalReverse" && !portalG)
        {
            portalG = true;
            Debug.Log("reverse gravitacija");
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(0, 12);
        }
        else if (other.tag == "portalReverse" && portalG)
        {
            portalG = false;
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
