using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PremikanjeOdzadja : MonoBehaviour {

    public GameObject igralec;
    public GameObject test;

    public Text x_gyro;
    public Text y_gyro;
    public Text z_gyro;

    public Text acc;



    // Use this for initialization
    void Start () {
        Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 x = new Vector3(igralec.transform.position.x, 0, -10);
        transform.position = x;

        //Vector3 pozicija_objekta = Camera.main.WorldToViewportPoint(test.transform.position);

        //pozicija_objekta.x = Mathf.Clamp01(pozicija_objekta.x);
        //pozicija_objekta.y = Mathf.Clamp01(pozicija_objekta.y);
        //pozicija_objekta.z = 1;

        //test.transform.position = Camera.main.ViewportToWorldPoint(pozicija_objekta);

        test.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.90f,0.88f,1));


        //test.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0,1,1));

        //test.transform.localPosition = new Vector3(0, 0, 1);

        if (PlayerPrefs.GetInt("senzorD") == 1)
        {
            test.SetActive(true);
            prikaziSenzorje();
        }
        else
            test.SetActive(false);

    }

    private void prikaziSenzorje()
    {
        x_gyro.text = "Gyro x: " + Input.gyro.userAcceleration.x.ToString();
        y_gyro.text = "Gyro y: " + Input.gyro.userAcceleration.y.ToString();
        z_gyro.text = "Gyro z: " + Input.gyro.userAcceleration.z.ToString();

        acc.text = "Acc: " + Input.acceleration.ToString();
        //y_gyro.text = Input.gyro.attitude.ToString();
    }
}
