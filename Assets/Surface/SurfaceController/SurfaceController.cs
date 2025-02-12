using UnityEngine;
using System.Collections;

public class SurfaceController : MonoBehaviour {

    public GameObject SURFACE;
    public Vector3 spawnPoint = new Vector3();
    
    private Surface MaSurface = null;
    public Surface getSurface() { return MaSurface; }
    private GameObject DaSurfacePhisicalObject = null; 
   
    void Start()
    {
        DaSurfacePhisicalObject = (GameObject)Instantiate(SURFACE, spawnPoint, Quaternion.identity);
        MaSurface = (DaSurfacePhisicalObject).GetComponent<Surface>();
        
    }


}
