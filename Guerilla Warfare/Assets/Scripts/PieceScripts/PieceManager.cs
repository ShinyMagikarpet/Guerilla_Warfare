using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceManager : MonoBehaviour
{
    [SerializeField]
    private List<BasePiece> mRedPieces = null;
    [SerializeField]
    private List<BasePiece> mBluePieces = null;
    public BasePiece mWarriorPiece;
    public BasePiece mArcherPiece;
    public BasePiece mWizardPiece;
    public BasePiece mKingPiece;
    public BasePiece mBarricadePiece;
    [HideInInspector]
    public Color mPlayerColor;

    public void Place_Piece(Cell cell, Color teamColor, Color32 spriteColor, PieceManager pieceManager) {
        int count = 0;
        //cell.mCurrentPiece =  Instantiate(mWarriorPiece, cell.transform);
        BasePiece newPiece = mWarriorPiece;
        if (teamColor == Color.red) {
            //TODO: keep track how many of piece type have been instanciated
            count = Get_Piece_Count(mRedPieces, mWarriorPiece);
            if(count >= 5) {
                Debug.Log("You have maximum amount of this piece");
                return;
            }
            mRedPieces.Add(newPiece);
        } else if(teamColor == Color.blue){
            count = Get_Piece_Count(mBluePieces, mWarriorPiece);
            if (count >= 5) {
                Debug.Log("You have maximum amount of this piece");
                Destroy(cell.mCurrentPiece);
                return;
            }
            mBluePieces.Add(newPiece);
        }
        cell.mCurrentPiece = Instantiate(newPiece, cell.transform);
        cell.mCurrentPiece.Setup_Piece(teamColor, spriteColor, this);

    }

    private int Get_Piece_Count(List<BasePiece> listPieces, BasePiece pieceType) {
        int count = 0;
        foreach(BasePiece piece in listPieces) {
            if (piece.GetType() == pieceType.GetType())
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
