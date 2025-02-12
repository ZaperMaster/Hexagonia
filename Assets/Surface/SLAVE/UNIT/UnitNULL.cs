using UnityEngine;
using System.Collections;

public class UnitNULL : UnitBase {

    private static UnitNULL REF = new UnitNULL();
    private UnitNULL() { }
    public static UnitNULL singleton() { return REF; }

    public override bool isEmpty() { return true; }
    public override string toString() { return "NullNode"; }
    public override string PrintJoins() { return "NullJoins"; }
    public override void reJoin(Unit u, Porta.Porte pt) { }
}
