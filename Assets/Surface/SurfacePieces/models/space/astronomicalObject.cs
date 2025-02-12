using UnityEngine;
using System.Collections;

public abstract class astronomicalObject : MonoBehaviour {

    
    public Transform myStar = null;
    public float orbitationVelocity = 0;
    public float distance = float.MaxValue;
    public float angle = 0;

    //change the size of the planet randomly
    protected void reDim() { reDim(1); }
    protected void reDim(float moltip) { float myScale = Random.Range(0.5f, 2f) * moltip * transform.localScale.x; transform.localScale = new Vector3(myScale, myScale, myScale); }

    //calculate the next position in the orbit
    //NB! this is jus a circle! if you want to use a realist orbit you have to use a muuuuuuch more complex formula
    protected Vector3 nextPositionInMyOrbit(Vector3 orbitPoint, Vector3 myPosition, float velocity)
    {
        angle = (angle + velocity) % (2 * Mathf.PI);
        return new Vector3(Mathf.Cos(angle) * distance + orbitPoint.x, 0 , Mathf.Sin(angle) * distance + orbitPoint.z);
    }

    //find a star that will be used as a center for the orbital movement
    protected bool lookForAStar(string TAG)
    {
        GameObject[] stars = GameObject.FindGameObjectsWithTag(TAG);

        foreach (GameObject o in stars)
            if (Vector3.Distance(this.transform.position, o.transform.position) < distance)
            {
                distance = Vector3.Distance(this.transform.position, o.transform.position);
                myStar = o.transform;
                orbitationVelocity = 2f / distance;
                angle = Mathf.Acos((transform.position.x - o.transform.position.x) / distance);
            }
        return (myStar != null);
    }




   

}