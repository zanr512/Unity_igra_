using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quuit : MonoBehaviour {


    public void novaIgra()
    {
        PlayerPrefs.SetInt("poskus", 0);
        SceneManager.LoadScene("zacetniLvl");
    }

    public void izhod()
    {
        Application.Quit();
    }

}
