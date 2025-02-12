using UnityEngine;
using System.Collections;

public class OPTIONS {

    private OPTIONS() { }
    private static OPTIONS REF = new OPTIONS();
    public static OPTIONS singleton() { return REF; }

    /// <summary> how many doors will be considered during the creation of new Units. [1..6]  </summary>
    public int numDoor = 6;
    
    /// <summary> Used to manage the number of Units that each one can generate [1..2] {UNIT-SIDE} </summary>
    public float initial_Son_Spawn_Rate = 1.6f;

    /// <summary> Used to decide the amount of Units that the FIRST UNIT of the surface will spawn [1..4] {SURFACE-SIDE} </summary>
    public float initial_Surface_Spawn_Rate = 2.5f;
    /// <summary> Max number of Units {SURFACE-SIDE} </summary>
    public int maxNumUnit = 300;

    /// <summary> Show base Units on construction </summary>
    public bool VisibleRawUnits = true;
    /// <summary> Attach 3d model over the units </summary>
    public bool Attach3Dmodel = true;


    /// <summary> Height gap between a unit and its sons </summary>
    public bool gotGap = false;
    public float heightGap = -3f;

}
