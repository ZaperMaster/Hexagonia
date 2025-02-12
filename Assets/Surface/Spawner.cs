using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public float countdown = 1f;
    public GameObject CONTROLLER;
    private SurfaceController sCntrl = null;
    public SurfaceController getSurfaceController() {return sCntrl;}

    private bool Shooted = false;

    int camIndx = 0;
    public Camera[] cams;
    public void switchCam() { for (int i = 0; i < cams.Length; i++) cams[i].enabled = (i == camIndx); }

    void Generate()
    {
        GameObject tmp = (GameObject) Instantiate(CONTROLLER, transform.position, Quaternion.identity);
        Shooted = true;
        transform.LookAt(tmp.GetComponent<SurfaceController>().spawnPoint);
        transform.position = tmp.GetComponent<SurfaceController>().spawnPoint + Vector3.forward * -150;
        sCntrl = tmp.GetComponent<SurfaceController>();
    }

    void OnGUI()
    {

        GUI.Label(new Rect(10, 10, 200, 50), "lista figliazione : " + sCntrl.getSurface().fertiliFigliazione.Count.ToString());
        GUI.Label(new Rect(10, 30, 200, 50), "lista modellazione : " + sCntrl.getSurface().fertiliModellazione.Count.ToString());

        if (GUI.Button(new Rect(Screen.width - 100, 10, 80, 20), "Restart")) Application.LoadLevel(Application.loadedLevel);
        if (GUI.Button(new Rect(Screen.width - 100, 40, 80, 20), "Switch Cam")) { camIndx = (camIndx + 1 ) % cams.Length; switchCam(); }
    }


    bool takeLongScreen = false; 
    int photoNr = 1;

    void Update() { 
        if (countdown > 0) countdown -= Time.deltaTime; else if (!Shooted) Generate(); 
        if (Input.GetKeyDown(KeyCode.K)) takeScreen();
        if (Input.GetKeyDown(KeyCode.J) && takeLongScreen) { takeLongScreen = false; photoNr += 100; Debug.Log("Phototime end ):"); }
        if (Input.GetKeyDown(KeyCode.L) && !takeLongScreen) { takeLongScreen = true; Debug.Log("Phototime!"); }
        
        
        if (takeLongScreen) takeLongScreenshots();
    }

    
    void takeScreen()
    {
        ScreenCapture.CaptureScreenshot("Screenshot.png");
        Debug.Log("DONE!");
    }

    float timer = 0;
    public float interval = 0.25f;
    void takeLongScreenshots()
    {
        if (timer > 0) 
            timer -= Time.deltaTime; 
        else {            
            ScreenCapture.CaptureScreenshot("Screenshot" + photoNr + ".png");
            Debug.Log("DONE! photo nr:" + photoNr);
            photoNr++;
            
            timer += interval;
        }
    }

}
