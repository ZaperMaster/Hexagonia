using UnityEngine;
using System.Collections;

public class VirtualPosition {

    int x, y;
    
    public int getX() { return x; }
    public int getY() { return y; }

    public VirtualPosition(int x, int y) { this.x = x; this.y = y; }

    public override string ToString() { return ("[" + x + ":" + y + "]"); }
    public override int GetHashCode() { return ToString().GetHashCode(); }
    public override bool Equals(object o) {
        if (ReferenceEquals(this, o))
            return true;
        else if (o == null)
            return false;
        else if (o.GetType().Equals(this.GetType()))
        {
            VirtualPosition vp = (VirtualPosition)o;
            if (vp.x == this.x && vp.y == this.y)
            return true;
        }
        return false;
    }

    public int getDeepness() { return (Mathf.Abs(x) + Mathf.Abs(y)) / 2; }

    public static VirtualPosition getNewPosition(VirtualPosition vP, Porta.Porte p) {
        int x = vP.x;
        int y = vP.y;

        switch (p)
        {
            case Porta.Porte.alpha: 
                return new VirtualPosition(x, y + 2);
            case Porta.Porte.beta:
                return new VirtualPosition(x + 1, y + 1);
            case Porta.Porte.gamma:
                return new VirtualPosition(x + 1, y - 1);
            case Porta.Porte.delta:
                return new VirtualPosition(x, y - 2);
            case Porta.Porte.epsilon:
                return new VirtualPosition(x - 1, y - 1);
            default:
                return new VirtualPosition(x - 1, y + 1);
        }
    
    
    }
    public VirtualPosition getNextPosition(Porta.Porte p) { return VirtualPosition.getNewPosition(this, p); }

}
