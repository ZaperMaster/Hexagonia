using UnityEngine;
using System.Collections;

public class pieceFactory {
    
    pieceContainer piecesContainer = null;
    //singleton
    private static pieceFactory REF = new pieceFactory();
    public static pieceFactory singleton() { return REF; }
    private pieceFactory() { piecesContainer = GameObject.FindGameObjectWithTag("pieceContainer").transform.GetComponent<pieceContainer>();  }

    public pieceContainer.boxContainer getCompetiblePiece(doorStats.casi[] stats) { return piecesContainer.getCompetiblePiece(stats); }

}
