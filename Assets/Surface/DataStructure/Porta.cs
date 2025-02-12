using UnityEngine;
using System.Collections;

public class Porta {
    public enum Porte { alpha = 0, beta = 1, gamma = 2, delta = 3, epsilon = 4, omega = 5 }
    public const int numPt = 6;

    public const Porte Alpha = Porte.alpha;
    public const Porte Beta = Porte.beta;
    public const Porte Gamma = Porte.gamma;
    public const Porte Delta = Porte.delta;
    public const Porte Epsilon = Porte.epsilon;
    public const Porte Omega = Porte.omega;

    public static Porte Opp(Porte pt)
    {
        switch (pt)
        {
            case Porte.alpha: return Porte.delta;
            case Porte.beta: return Porte.epsilon;
            case Porte.gamma: return Porte.omega;
            case Porte.delta: return Porte.alpha;
            case Porte.epsilon: return Porte.beta;
            case Porte.omega: return Porte.gamma;
        }
        throw(new System.Exception());
    }
    public static Porte intToPorta(int i)
    {
        switch (i)
        {
            case 0: return Porte.alpha;
            case 1: return Porte.beta;
            case 2: return Porte.gamma;
            case 3: return Porte.delta;
            case 4: return Porte.epsilon;
            default: return Porte.omega;
        }
    }

    public static Porte randomPorta() { return randomPorta(OPTIONS.singleton().numDoor); }
    public static Porte randomPorta(int i)
    {
        int k = (int) Random.Range(0, i) % 6;
        switch (k)
        {
            case 0: return Porta.Alpha;
            case 1: return Porta.Beta;
            case 2: return Porta.Omega;
            case 3: return Porta.Gamma;
            case 4: return Porta.Epsilon;
            case 5: return Porta.Delta;
        }
        return Porte.alpha;
    }

    public static Porte Next(Porte pt)
    {
        switch (pt)
        {
            case Porte.alpha: return Porte.beta;
            case Porte.beta: return Porte.gamma;
            case Porte.gamma: return Porte.delta;
            case Porte.delta: return Porte.epsilon;
            case Porte.epsilon: return Porte.omega;
            case Porte.omega: return Porte.alpha;
        }
        throw (new System.Exception());
    }
    public static Porte Prev(Porte pt)
    {
        switch (pt)
        {
            case Porte.alpha: return Porte.omega;
            case Porte.beta: return Porte.alpha;
            case Porte.gamma: return Porte.beta;
            case Porte.delta: return Porte.gamma;
            case Porte.epsilon: return Porte.delta;
            case Porte.omega: return Porte.epsilon;
        }
        throw (new System.Exception());
    }

}
