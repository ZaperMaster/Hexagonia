using UnityEngine;
using System.Collections;

public class PhisicUnit : MonoBehaviour {

    public int ID = 0;
    public Unit AssociatedUnit;
    public void SetUnit(Unit u) { AssociatedUnit = u; ID = u.ID; }

    public void autodestruction() { Destroy(this.gameObject); }


    internal GameObject spawna(GameObject obj) { return (GameObject) Instantiate(obj, transform.position + Vector3.up * 3, Quaternion.identity);   }
    public Transform getWayPoint() { return transform.GetChild(1); }
}
