using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Surface : MonoBehaviour  {

    bool GeneratorCreated = false;
    bool gogo = true;
    public int lastNum = 0;
    int modeled = 0;
    
    float doEvery = 0f;
    float time = 2;

    void Update()
    {
        if (gogo && allDaUnits.Count < OPTIONS.singleton().maxNumUnit) { gogo = grow(); lastNum = allDaUnits.Count; }
        applyModelling();
    }

    // -- VARIABLES

    // Object spawned as Unit
    public GameObject DaPhisicObject;
    
    // List of all the Units that can generate a son
    public LinkedList<Unit> fertiliFigliazione = new LinkedList<Unit>();
    // List of all the Units that can spawn a model over them
    public LinkedList<Unit> fertiliModellazione = new LinkedList<Unit>();
    // Data structures that contain all the unit
    public Hashtable UNITS = new Hashtable();
    public LinkedList<Unit> allDaUnits = new LinkedList<Unit>();


    // -- UNITS

    /// <summary> Search a unit in the hashtable, using its virtual position as key </summary>
    public UnitBase searchUnit(VirtualPosition vP) { return (UNITS.ContainsKey(vP) ? (UnitBase) UNITS[vP] : UnitNULL.singleton()); }
    /// <summary> Verify if a unit exists in the hashtable, using its virtual position as key </summary>
    public bool existsUnit(VirtualPosition vP) { return (UNITS.ContainsKey(vP)); }


    // -- SONS SPAWNING

    /// <summary> Expand the surface, calling spawning functions of the units </summary>
    public bool grow() {
        UnitBase tmpU = null;
        Unit father = null;

        // first of all create the first unit
        if (fertiliFigliazione.Count == 0 && !GeneratorCreated) 
            tmpU = createNewUnit();        
        // then let the Units create its sons
        else
        {
            father = fertiliFigliazione.First.Value;
            tmpU = father.generateSon();
        }
        
        // now we have two possibilities:
        // 1 : [UNITNULL (NoSpace/NoTassoFigl)] --> remove the Units from the list
        // 2 : [existing son] --> Add it to the data structure and leave the father on the list, but I'll check if he is sterile

        if (tmpU.isEmpty())
            // deletes the sterile Unit. you get rarely here
            fertiliFigliazione.RemoveFirst();
        else
        {
            Unit u = (Unit)tmpU;
            fertiliModellazione.AddLast(u);
            fertiliFigliazione.AddLast(u);
            if (!fertiliFigliazione.First.Value.canGenerateASon()) fertiliFigliazione.RemoveFirst();            
            allDaUnits.AddLast(u);
            UNITS.Add(u.virtualPos, u);
        }

        if (fertiliFigliazione.Count == 0) return false;
        else return true;
    }
    /// <summary> Spawn the FIRST Unit </summary>
    private Unit createNewUnit()
    {
        // Spawno l'oggetto fisico
        GameObject tmp = spawnPhisicObj();
        // Intercetto i suoi Joins
        GameObject[] phisicJoins = getPhisicJoins(tmp);
        
        // Creo la classe UNIT
        Unit tmpOne = new Unit(tmp, phisicJoins, this, OPTIONS.singleton().initial_Surface_Spawn_Rate);
        return tmpOne;
    }
    /// <summary> Spawn a PHISICOBJ </summary>
    public GameObject spawnPhisicObj(VirtualPosition vp) {
        Vector3 phisicalPosition = newPiecePlace(vp, DaPhisicObject.transform.localScale.x);
        if (OPTIONS.singleton().gotGap) {
            float Y = (Math.Abs(vp.getX()) + Math.Abs(vp.getY())) * OPTIONS.singleton().heightGap;
            phisicalPosition += new Vector3(0, Y, 0); 
        }
        return (GameObject)Instantiate(DaPhisicObject, phisicalPosition, Quaternion.LookRotation(Vector3.up)); 
    }
    public GameObject spawnPhisicObj()
    {
        return (GameObject)Instantiate(DaPhisicObject, new Vector3(0, 0, 0), Quaternion.LookRotation(Vector3.up));
    }
    /// <summary> given a PHISICOBJ, returns its Joins </summary>
    public GameObject[] getPhisicJoins(GameObject obj)
    {
        GameObject[] phisicJoins = new GameObject[6];
        for (int i = 0; i < 6; i++)
            phisicJoins[i] = obj.transform.GetChild(i).gameObject;

        return phisicJoins;
    }
    /// <summary> Given a virtualPosition and the size of the model, it returns the phisical position </summary>
    protected Vector3 newPiecePlace(VirtualPosition vp, float PhisicScale)
    {
        Unit REF = (Unit) UNITS[new VirtualPosition(0,0)];
        float diagonale = (REF.GetJuncture(Porta.Porte.beta).Join.transform.localPosition.x) * PhisicScale;
        float verticale = (REF.GetJuncture(Porta.Porte.alpha).Join.transform.localPosition.y) * PhisicScale;


        float x = diagonale * 2 * vp.getX();
        float y = 0;
        float z = verticale * vp.getY();
        
        return new Vector3(x, y, z);
    }


    // -- MODELLING

    public void model() {
        if (fertiliModellazione.Count == 0) return;

        Unit tmp = fertiliModellazione.First.Value;
        doorStats.casi[] statiP = tmp.getDoorState();
        tmp.attachPiece(pieceFactory.singleton().getCompetiblePiece(statiP));
        fertiliModellazione.RemoveFirst();
        modeled++;
    }
    void applyModelling()
    {
        if (OPTIONS.singleton().Attach3Dmodel && time < 0)
        {
            if ((lastNum - modeled > 20 || !gogo || allDaUnits.Count == OPTIONS.singleton().maxNumUnit))
            {
                model();
                time = doEvery;
            }
        }
        else time -= Time.deltaTime;

    }

    // -- PATHS

    private void init_SS() { foreach (Unit u in allDaUnits) u.inizializzaStima(); }
    private void Relax(Unit u) { u.relax(); }

    public void pathFromS(Unit S)
    {
        init_SS();
        Relax(S);
    }


}
