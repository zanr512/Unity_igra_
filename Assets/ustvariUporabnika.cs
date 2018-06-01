using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ustvariUporabnika : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey("name"))
        {
            SceneManager.LoadScene("glavniMeni");
        }
	}
	
	// Update is called once per frame

    public void PreveriUser()
    {
        GameObject inputFieldGo = GameObject.Find("user_name_text");
        InputField inFi = inputFieldGo.GetComponent<InputField>();


        string name = inFi.text;

        var url = "http://localhost:8080/ime/" + name;

        var www = new WWW(url);

        StartCoroutine(WaitForRequest(www,name));
    }

    IEnumerator WaitForRequest(WWW www, string ime)
    {
        
        GameObject textFieldGo = GameObject.Find("error");
        Text t = textFieldGo.GetComponent<Text>();
        t.text = "";
        yield return www;
        

        var res = www.text;

        if(www.error != null)
        {
            t.text = "Streznik nedosegljiv";
        }
        else
        {
            if (res == "ok")
            {
                PlayerPrefs.SetString("name", ime);
            }
            else
            {
                t.text = "Ime je ze v uporabi";
            }

        }

        

 
    }

}
