using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceManager : MonoBehaviour
{
    [SerializeField]
    public List<BasePiece> mRedPieces = null;
    [SerializeField]
    public List<BasePiece> mBluePieces = null;
    public BasePiece mWarriorPiece;
    public BasePiece mArcherPiece;
    public BasePiece mWizardPiece;
    public BasePiece mKingPiece;
    public BasePiece mBarricadePiece;
    [HideInInspector]
    public Color mPlayerColor;

    public void Place_Piece(Cell cell, Color teamColor, Color32 spriteColor, PieceManager pieceManager) {
        int count = 0;
        cell.mCurrentPiece =  Instantiate(mWarriorPiece, cell.transform);
        cell.mCurrentPiece.Setup_Piece(teamColor, spriteColor, this);
        if (teamColor == Color.red) {
            //TODO: keep track how many of piece type have been instanciated
            mRedPieces.Add(cell.mCurrentPiece);
            Debug.Log(mRedPieces.Count);
        } else if(teamColor == Color.blue){
            mBluePieces.Add(cell.mCurrentPiece);
            Debug.Log(mBluePieces.Count);
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

    private void OnApplicationQuit() {
        
    }
}
