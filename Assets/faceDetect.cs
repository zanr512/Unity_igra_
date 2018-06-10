using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class faceDetect : MonoBehaviour {

    // Use this for initialization
    string url = "http://192.168.137.1:8080/upload";
    public RawImage tst;
    public Text txt;
    byte[] bytes;
    WebCamTexture tex = null;
    void Start () {
        if(PlayerPrefs.HasKey("ok"))
            url = "http://192.168.137.1:8080/upload";
        else
            url = "http://192.168.137.1:8080/uploadFirst";
        string tmp = "";
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            {
                tmp = devices[i].name;
                break;
            }
        }
        tex = new WebCamTexture(tmp);
        tst.texture = tex;
        tex.Play();
    }

    // Update is called once per frame
    public void SaveImage()
    {
        
        //Create a Texture2D with the size of the rendered image on the screen.
        Texture2D texture = new Texture2D(tst.texture.width, tst.texture.height, TextureFormat.ARGB32, false);

        //Save the image to the Texture2D
        texture.SetPixels(tex.GetPixels());
        texture.Apply();

        //Encode it as a PNG.
        bytes = texture.EncodeToPNG();
        //Debug.Log(Application.dataPath + "/Images/testimg.png");
        tex.Stop();
        //Save it in a file.
        //File.WriteAllBytes(Application.dataPath + "/Images/testimg.png", bytes);
        StartCoroutine(UploadFileCo(Application.dataPath + "/Images/testimg.png", url));
    }

    IEnumerator UploadFileCo(string localFileName, string uploadURL)
    {
        txt.text = "";
        //WWW localFile = new WWW("file:///" + localFileName);
        //yield return localFile;
        //if (localFile.error != null)
        //{
        //    Debug.Log("Open file error: " + localFile.error);
        //    yield break; // stop the coroutine here
        //}
        WWWForm postForm = new WWWForm();
        // version 1
        //postForm.AddBinaryData("theFile",localFile.bytes);
        // version 2
        postForm.AddBinaryData("sampleFile", bytes, localFileName, "multipart/form-data");
        postForm.AddField("tst", PlayerPrefs.GetString("name"));
        WWW upload = new WWW(uploadURL, postForm);
        txt.text = "Čakam na odziv strežnika";
        yield return upload;
        var res = upload.text;
        Debug.Log(res);
        if (res == "ni ok")
        {
            txt.text = "Obraz se ne ujema z registriranim uporabnikom";
            tex.Play();
        }
        else if (res != "ok")
        {
            tex.Play();
            txt.text = res;
        }
        else if (res == "ok")
        {
            PlayerPrefs.SetString("ok", "ok");
            txt.text = "Obraz pravilen";
            SceneManager.LoadScene("glavniMeni");
            
        }
        if (upload.error != null)
            Debug.Log("Error during upload: " + upload.error);
    }
}
