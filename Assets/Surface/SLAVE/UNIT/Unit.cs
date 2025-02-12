using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : UnitBase {

    // -- VARIABILI

    public int ID;

    public VirtualPosition virtualPos;
    public GameObject ControlledObj;
    public PhisicUnit phisicUnitScript;
    public int nJoins = 0;
    public JoiningPoint[] Giunture;
    private Surface S;
    
    //percorso

    

    // -- BORN

    /// <summary> set of operations necessary to the creation of a unit </summary>
    private void SimpleCreation(VirtualPosition vt, GameObject Phis, GameObject[] Joins, Surface s) 
    {
        //Debug.Log("MaPositionIs: " + vt.ToString());
        S = s;
        ID = FixedId++;
        ControlledObj = Phis;
        Phis.GetComponent<Renderer>().enabled = OPTIONS.singleton().VisibleRawUnits;
        phisicUnitScript = ControlledObj.GetComponent<PhisicUnit>();
        virtualPos = vt;
        Giunture = new JoiningPoint[Joins.Length];
        
        for (int i = 0; i < Joins.Length; i++)
        {
            JoiningPoint tmp = new JoiningPoint();
            tmp.NextElement = UnitNULL.singleton();
            tmp.Join = Joins[i];
            Giunture[i] = tmp;
        }
        phisicUnitScript.SetUnit(this);
    }
    /// <summary> MINIMAL constructor (just for the first unit) </summary>
    public Unit(GameObject Phis, GameObject[] Joins, Surface s, float tassoFigliazione) {
        this.tassoFigliazione = tassoFigliazione;
        tassoRimanente = tassoFigliazione;
        SimpleCreation(new VirtualPosition(0, 0), Phis, Joins, s); 
    }
    /// <summary> DEFAULT constructor </summary>
    public Unit(Unit Daddy, Porta.Porte WayIn, VirtualPosition vPos, GameObject Phis, GameObject[] Joins)
    {
        SimpleCreation(vPos, Phis, Joins, Daddy.S);
        tassoFigliazione = Daddy.getNextFigliazione();
        tassoRimanente = tassoFigliazione;
        //JOIN(Daddy, WayIn);
        SIMPLEJOIN();

    }



    // -- JUNCTURE

    /// <summary> Join algorithm </summary>
    private void SIMPLEJOIN()
    {
        Porta.Porte ptTmp = Porta.Porte.alpha;
        for (int i = 0; i < Porta.numPt; i++)
        {
            joinWith(S.searchUnit(virtualPos.getNextPosition(ptTmp)), ptTmp);
            ptTmp = Porta.Next(ptTmp);
        }

    }
    /// <summary> execute a simple Join + u.reJoin </summary>
    private void joinWith(UnitBase u, Porta.Porte door)
    {
        GetJuncture(door).NextElement = u;
        if (!u.isEmpty())
        {
            nJoins++;
            GetJuncture(door).stato = doorStats.singleton().getDefaultStat();
        }
        else GetJuncture(door).stato = doorStats.singleton().getNoWayStat();
        u.reJoin(this, Porta.Opp(door));

    }
    /// <summary> function called by the unit that has just joined with this one (in its joinWith() function) </summary>
    public override void reJoin(Unit u, Porta.Porte pt) { GetJuncture(pt).NextElement = u; nJoins++; GetJuncture(pt).stato = doorStats.singleton().getDefaultStat(); }

    public JoiningPoint GetJuncture(Porta.Porte nomePt) { return Giunture[portaToInt(nomePt)]; }
    public int portaToInt(Porta.Porte nome)
    {
        switch (nome)
        {
            case Porta.Alpha: return 0;
            case Porta.Beta: return 1;
            case Porta.Gamma: return 2;
            case Porta.Delta: return 3;
            case Porta.Epsilon: return 4;
            case Porta.Omega: return 5;
        }
        throw (new System.Exception());
    }
    public UnitBase PointingTo(Porta.Porte door) { return GetJuncture(door).NextElement; }
    public int oppositeDoorNr(int dr) { return (dr + 3) % 6; }

    public override string toString() { return ("[ Id : " + ID + " VirtPos: " + virtualPos.ToString() + " ]"); }
    public override string PrintJoins() { string tmp = ""; for (int i = 0; i < 6; i++) tmp = tmp + " " + Porta.intToPorta(i) + "->" + GetJuncture(Porta.intToPorta(i)).ToString() + "// "; return tmp; }
    public override bool isEmpty() { return false; }

    public Vector3 getPhisicPosition() { return new Vector3(); }
    public VirtualPosition getKey() { return virtualPos; }


    // -- SONS SPAWN

    public float tassoFigliazione;
    public float tassoRimanente;

    /// <summary> function called by surface to generate a son from this unit. If that is not possible it returns UNITNULL </summary>
    public UnitBase generateSon() {
        // se non ho abbastanza TASSO o se ho tutti i join occupati restituisco un'unità vuota
        // verrò così rimosso dalla lista fertili
        //Questa è una situazione di emergenza; solitamente viene fatto un check per rimuovere le unità scariche da FERTILI.list
        if (!canGenerateASon()) return UnitNULL.singleton();

        Porta.Porte p = Porta.randomPorta();
        int tentativirimanenti = 10;

        while (!GetJuncture(p).NextElement.isEmpty() && (tentativirimanenti-- > 0))
            p = Porta.randomPorta();
        if (tentativirimanenti <= 0) return UnitNULL.singleton();


        GameObject phisicObj = S.spawnPhisicObj(virtualPos.getNextPosition(p));
        GameObject[] joins = S.getPhisicJoins(phisicObj);

        tassoRimanente--;
        return new Unit(this, p, virtualPos.getNextPosition(p), phisicObj, joins);
    }

    public float getNextFigliazione() { return OPTIONS.singleton().initial_Son_Spawn_Rate + Mathf.Max(tassoFigliazione - 2, 0); }
    public bool canGenerateASon() { return (tassoRimanente >= 1 && nJoins < Giunture.Length); }


    // -- PATHS

    public bool isSource = false;
    private Unit father = null;
    public Unit getFather() { return father; }
    public int value = 1;
    public int stima;
    public bool hasBeenChecked = false;

    /// <summary> A </summary>
    public void inizializzaStima() { stima = int.MaxValue; hasBeenChecked = false; isSource = false; father = null; }
    /// <summary> Execute a Relax on the adjacents units, changing their value if necessary </summary>
    private void relax(Unit u)
    {
        //Relax chiamata da ogni cella, esclusa la sorgente
        hasBeenChecked = true;
        this.stima = this.value + u.stima;
        father = u;

        for (int i = 0; i < Giunture.Length; i++)
            if (!Giunture[i].NextElement.isEmpty())
            {
                Unit son = (Unit)Giunture[i].NextElement;
                if (!son.hasBeenChecked) son.relax(this);
                else if (son.stima > this.stima + son.value) son.relax(this);
            }

    }
    public void relax()
    {
        //Relax della sorgente
        stima = 0;
        isSource = true;
        hasBeenChecked = true;
        for (int i = 0; i < Giunture.Length; i++)
            if (!Giunture[i].NextElement.isEmpty())
            {
                Unit son = (Unit)Giunture[i].NextElement;
                if (!son.hasBeenChecked) son.relax(this);
                else if (son.stima > this.stima + son.value) son.relax(this);
            }

    }


    // -- OBJECT SPAWNING

    public GameObject spawnThis(GameObject obj) { return phisicUnitScript.spawna(obj); }
    public void moveThis(GameObject obj) { obj.transform.position = ControlledObj.transform.position + Vector3.up * 4; }
    private void fixPiece(pieceContainer.boxContainer bc, GameObject spawnedObj) { pieceContainer.boxContainer.rotateModel(spawnedObj, bc.rotationCoeff); }
    public void attachPiece(pieceContainer.boxContainer bx) {

        GameObject spawnedObj = spawnThis(bx.b.da3dModel);
        if (bx.rotationCoeff != 0) fixPiece(bx, spawnedObj);
        int indxPieceDoor = (6 - bx.rotationCoeff) % 6;
        //string PreSituation = PrintJoins();
        for (int i = 0; i < Giunture.Length; i++)
        {
            Giunture[i].stato = bx.b.doors[indxPieceDoor];
            if (!Giunture[i].NextElement.isEmpty())
            {
                Unit u = (Unit)Giunture[i].NextElement;
                u.Giunture[oppositeDoorNr(i)].stato = bx.b.doors[indxPieceDoor];                
            }
            indxPieceDoor = (indxPieceDoor + 1) % 6;
        }

        //Debug.Log(StampaNodo() + "Choosen piece: " + bx.b.name + "rotation: " + bx.rotationCoeff * 60 + "° \nPRE " + PreSituation + " \nPOST " + StampaJoins());
    }

    // -- DOORS

    public doorStats.casi[] getDoorState() { 
        doorStats.casi[] vt = new doorStats.casi[6];
        for (int i = 0; i < Giunture.Length; i++)
            vt[i] = Giunture[i].stato;
        return vt;
    }

    

 }
