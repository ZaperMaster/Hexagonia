using UnityEngine;
using System.Collections;

public class pieceContainer : MonoBehaviour {
    
    [SerializeField] 
    public Box[] availablePieces;

    /// <summary> Used to mess the position of the elements of the array to avoid primary agglomeration </summary>
    void makeMess()
    {
        Box tmp = null;
        int j;
        for (int i = 0; i < availablePieces.Length; i++)
        {
            j = Random.Range(0, availablePieces.Length);
            tmp = availablePieces[i];
            availablePieces[i] = availablePieces[j];
            availablePieces[j] = tmp;
        }
    }

    void Start() { makeMess(); }

    [System.Serializable]
    /// <summary> Class used to store a model and its variables </summary>
    public class Box
    {
        public string name;
        public GameObject da3dModel = null;
        //public Vector3 wayPointPosition = new Vector3();
        public float pathValue = 0;
        //public int maxNumber = -1;
        
        public doorStats.casi[] doors;
        public Box() { doors = new doorStats.casi[6]; }

        public Vector2 evaluateWith(doorStats.casi[] DORZ)
        {
            float[,] matchingTable = new float[doors.Length, doors.Length];

            for (int i = 0; i < doors.Length; i++)
                for (int j = 0; j < doors.Length; j++)
                    matchingTable[i, j] = doorStats.singleton().match(DORZ[j], this.doors[i]);

            float maxMatching = 0;
            int steps = 0;
            int r = 0;
            int c = 0;
            float sommatoria = 0;

            //string debugSS = "matching.." + name;

            for (int l = 0; l < doors.Length; l++)
            {
                //debugSS += "\nstep:" + l;
                for (int k = 0; k < doors.Length; k++)
                {
                    sommatoria += matchingTable[r, c];
                    //debugSS += "[r:" + r + "c:" + c + "]";
                    r = (r + 1) % doors.Length;
                    c = (c + 1) % doors.Length;
                }
                //debugSS += "tot:" + sommatoria;
                if (sommatoria > maxMatching)
                {
                    maxMatching = sommatoria;
                    steps = l;
                }
                sommatoria = 0;
                r = 0;
                c++;
            }

            //Debug.Log(debugSS);
            return new Vector2((float)maxMatching, (float)steps);
        }
    }
    /// <summary> Class used to save boxes infos, used just for the spawning and than deleted </summary>
    public class boxContainer
    {
        public Box b;
        public float matchingLevel;
        public int rotationCoeff;
        public boxContainer(Box b, float matchingLevel, int rotationCoeff)
        {
            this.b = b;
            this.matchingLevel = matchingLevel;
            this.rotationCoeff = rotationCoeff;
        }
        public static void rotateModel(GameObject spawnedObj, int steps) { spawnedObj.transform.Rotate(new Vector3(0, -60 * steps, 0)); }
    

    }

    /// <summary> function used to get a Model and his infos (inside the boxContainer), compatible with the Unit, that will be spawned over it </summary>
    public boxContainer getCompetiblePiece(doorStats.casi[] stats) {
        // here will be stored:         infos.x == matchingLevel;         infos.y == rotationCoefficent
        Vector2[] infos = new Vector2[availablePieces.Length];
        
        int indx = 0;
        float maxMatching = 0;

        // random + sequencial access to the array
        int count = 0;
        int i = Random.Range(0, availablePieces.Length);
        while (count < availablePieces.Length)
        {
            infos[i] = availablePieces[i].evaluateWith(stats);
            if (maxMatching < infos[i].x) { maxMatching = infos[i].x; indx = i; }

            i = (i + 1) % availablePieces.Length;
            count++;
        }
        
        //string debugS = "Compatibility: ";
        //for (i = 0; i < avaiablePieces.Length; i++) debugS += avaiablePieces[i].name + ": " + infos[i].x + "/";
        //Debug.Log(debugS);

        return new boxContainer(availablePieces[indx], infos[indx].x, (int) infos[indx].y);
    }
    

}
