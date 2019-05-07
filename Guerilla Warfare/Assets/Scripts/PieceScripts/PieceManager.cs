using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceManager : MonoBehaviour
{

    protected List<BasePiece> mRedPieces = new List<BasePiece>();
    protected List<BasePiece> mBluePieces = new List<BasePiece>();
    public BasePiece mWarriorPiece;
    public BasePiece mArcherPiece;
    public BasePiece mWizardPiece;
    public BasePiece mKingPiece;
    public BasePiece mBarricadePiece;
    [HideInInspector]
    public Color mPlayerColor;

    public void Place_Piece(Cell cell, Color teamColor) {
        int count = 0;
        cell.mCurrentPiece =  Instantiate(mWarriorPiece, cell.transform);
        if (teamColor == Color.red) {
            //TODO: keep track how many of piece type have been instanciated
        } else {

        }
            
    }

    private int Get_Piece_Count(List<BasePiece> listPieces, BasePiece pieceType) {
        int count = 0;
        foreach(BasePiece piece in listPieces) {
            Debug.Log("Piece Name: " + piece.name);
            Debug.Log("Piece Type: " + pieceType.name);
            if (piece.transform.name == pieceType.transform.name)
                count++;
        }

        return count;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
