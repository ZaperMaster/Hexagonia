using UnityEngine;
using System.Collections;

public abstract class UnitBase {
    protected static int FixedId = 0;

    public abstract bool isEmpty();
    public abstract string toString();
    public abstract string PrintJoins();

    [System.Serializable]
    public class JoiningPoint
    {
        public GameObject Join;
        public UnitBase NextElement = UnitNULL.singleton();
        public doorStats.casi stato = doorStats.singleton().getNoWayStat();
        public new string ToString() { string tmp = "" + (NextElement.isEmpty() ? "Nl" : " Id:" + ((Unit)NextElement).ID) + ",Stat:" + stato.ToString(); return tmp; }
    }
    public abstract void reJoin(Unit u, Porta.Porte pt);
}
