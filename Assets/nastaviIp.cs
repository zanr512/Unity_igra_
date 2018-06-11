using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class nastaviIp : MonoBehaviour {

    public Text t;

    public void NastaviIp()
    {
        PlayerPrefs.SetString("url", t.text);
        SceneManager.LoadScene("kreiraj_user");
    }
}
