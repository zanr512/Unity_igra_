using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class glavniMeniSkripta : MonoBehaviour {

    public Button b;
    public Toggle t;

    // Use this for initialization
    void Start () {

        //ŠTevilo poskusov
        int i = PlayerPrefs.GetInt("poskus", 0);
        //Če je vključen debug senozorjev
        PlayerPrefs.SetInt("senzorD", 0);

        b.interactable = false;
        Debug.Log(PlayerPrefs.GetInt("senzorD"));
        if (PlayerPrefs.GetInt("poskus") > 0)
        {
            b.interactable = true;
        }

        if (PlayerPrefs.GetInt("senzorD") == 1)
            t.isOn = true;
        else
            t.isOn = false;

        t.onValueChanged.AddListener(delegate { spremeniDebugMode(t.isOn); });
        
	}

    public void novaIgra()
    {
        PlayerPrefs.SetInt("poskus", 0);
        SceneManager.LoadScene("zacetniLvl");
    }

    public void izhod()
    {
        Application.Quit();
    }

    public void spremeniDebugMode(bool a)
    {
        if (a)
            PlayerPrefs.SetInt("senzorD", 1);
        else
            PlayerPrefs.SetInt("senzorD", 0);
    }
	
}
