using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class faceDetect : MonoBehaviour {

	// Use this for initialization
    public RawImage tst;
	void Start () {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }
        WebCamTexture tex = new WebCamTexture(devices[0].name);
        tst.texture = tex;
        tex.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
