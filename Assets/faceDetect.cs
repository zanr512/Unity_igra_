using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class faceDetect : MonoBehaviour {

    // Use this for initialization
    string url = "http://192.168.1.22:8080/upload";
    public RawImage tst;
    WebCamTexture tex = null;
    void Start () {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }
        tex = new WebCamTexture();
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
        byte[] bytes = texture.EncodeToPNG();
        Debug.Log(Application.dataPath + "/Images/testimg.png");
        tex.Stop();
        //Save it in a file.
        File.WriteAllBytes(Application.dataPath + "/Images/testimg.png", bytes);
        StartCoroutine(UploadFileCo(Application.dataPath + "/Images/testimg.png", url));
    }

    IEnumerator UploadFileCo(string localFileName, string uploadURL)
    {
        WWW localFile = new WWW("file:///" + localFileName);
        yield return localFile;
        if (localFile.error == null)
            Debug.Log("Loaded file successfully");
        else
        {
            Debug.Log("Open file error: " + localFile.error);
            yield break; // stop the coroutine here
        }
        WWWForm postForm = new WWWForm();
        // version 1
        //postForm.AddBinaryData("theFile",localFile.bytes);
        // version 2
        postForm.AddBinaryData("sampleFile", localFile.bytes, localFileName, "multipart/form-data");
        postForm.AddField("tst", PlayerPrefs.GetString("name"));
        WWW upload = new WWW(uploadURL, postForm);
        yield return upload;
        if (upload.error == null)
            Debug.Log("upload done :" + upload.text);
        else
            Debug.Log("Error during upload: " + upload.error);
    }
}
