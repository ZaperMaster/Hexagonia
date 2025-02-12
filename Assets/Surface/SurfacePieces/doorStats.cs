using UnityEngine;
using System.Collections;

public class doorStats {
    //singleton
    private static doorStats REF = new doorStats();
    public static doorStats singleton() { return REF; }
    private doorStats() { }

    //public enum casi {/*Null,*/ Free, Busy, Indiff}

    //public casi getDefault() { return casi.Free; }
    //public casi getEmpty() { return casi.Busy; }

    //public float match(casi a, casi b)
    //{
    //    if (a == casi.Indiff || b == casi.Indiff) return 5;
    //    else if (a == b) return 10;
    //    else return 0;
    //}

    public enum casi { Empty, Busy, SmallStreet, LargeStreet, Canal, Forest, Land, Height0, Height1, Height2 }

    /// <summary>  </summary>
    public casi getDefaultStat() { return casi.Empty; }
    public casi getNoWayStat() { return casi.Busy; }

    /// <summary>  </summary>
    public float match(casi unitStat, casi modelStat)
    {
        /*
           HERE YOU HAVE SOME EXAMPLES: 
         For the scenes I provided, the 'SIMPLE' is enough
        */

        /* --- DESCENDING MOUNTAIN ---
        if (unitStat == casi.H0 && modelStat == casi.H2) return 15;
        else if (unitStat == casi.H2 && modelStat == casi.H2) return 0;
        else if (unitStat == casi.Empty && modelStat != casi.Busy) return 5;
        else if (unitStat == modelStat) return 0;
        else return 0;
        */

        /* --- SIMPLE --- */
        if (unitStat == modelStat) return 10;
        else return 0;
        

        /* --- CITY --- 
        if (unitStat == modelStat) return 10;
        else if (unitStat == casi.Busy && modelStat != casi.H0) return -5;
        else if (unitStat == casi.Empty && modelStat == casi.H1) return 5;
        else if (unitStat == casi.Empty && modelStat == casi.H2) return 5;
        else return 0;
        */

        /* --- CANAL --- 
        if (unitStat == casi.H0 && modelStat == casi.H2) return 15;
        else if (unitStat == casi.Busy && modelStat == casi.H2) return 10;
        else if (unitStat == casi.H2 && modelStat == casi.H2) return 0;
        else if (unitStat == casi.Empty && modelStat != casi.Busy) return 5;
        else if (unitStat == modelStat) return 0;
        else return 0;
        */

        /* --- WALLS --- 
        if (modelStat == unitStat) return 30;
        else if (unitStat == casi.Empty && unitStat == casi.H0) return 45;
        else return 0;
        */

    }
}
