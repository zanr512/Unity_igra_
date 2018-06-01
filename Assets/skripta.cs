using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class skripta : MonoBehaviour {

    // Use this for initialization
    public Button b;

    // Use this for initialization
    void Start()
    {

        //ŠTevilo poskusov
        PlayerPrefs.GetInt("poskus", 0);
        //Če je vključen debug senozorjev
        PlayerPrefs.SetInt("senzorD", 0);

        b.interactable = false;
        if (PlayerPrefs.GetInt("poskus") > 0)
        {
            b.interactable = true;
        }





    }

    public void nadaljujIgro()
    {
        SceneManager.LoadScene("zacetniLvl");
    }
}
