using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceManager : MonoBehaviour
{

    [HideInInspector]
    public List<BasePiece> mRedPieces = null;
    [HideInInspector]
    public List<BasePiece> mBluePieces = null;

    public BasePiece mWarriorPiece;
    public BasePiece mArcherPiece;
    public BasePiece mWizardPiece;
    public BasePiece mKingPiece;
    public BasePiece mBarricadePiece;

    [HideInInspector]
    public Color mPlayerColor;

    //This will be used to preset the board
    private string[] mPiecePlacement = new string[48] {

        "W", "B", "A", "A", "W", "B", "B", " ", " ", "A", " ", "W",
        " ", "W", " ", "B", "W", " ", "A", "B", "WI", "W", " ", " ",
        "B", "B", " ", "A", " ", "B", " ", " ", " ", " ", "A", " ",
        " ", " ", "WI", "B", "K", "A", " ", "B", " ", "WI", " ", " "

    };


    public void Setup_Board() {

    }

    public void Place_Piece(Cell cell, Color teamColor, Color32 spriteColor, PieceManager pieceManager) {

        if (teamColor == Color.red) {
            BasePiece newPiece = Instantiate(mSelectedPiece, transform);
            newPiece.Setup_Piece(teamColor, spriteColor, this);
            newPiece.Place_Piece(cell);
            mRedPieces.Add(newPiece);
        } else if(teamColor == Color.blue){
            BasePiece newPiece = Instantiate(mSelectedPiece, transform);
            newPiece.Setup_Piece(teamColor, spriteColor, this);
            newPiece.Place_Piece(cell);
            mBluePieces.Add(newPiece);
        }
        
    }

    //Skipping setup phase as I now learnt that I just have to show the core concept of game
    private int Get_Piece_Count(List<BasePiece> listPieces, BasePiece pieceType) {
        int count = 0;
        foreach(BasePiece piece in listPieces) {
            if (piece.GetType() == pieceType.GetType())
                count++;
        }
        return count;
    }

    public void SetInteractive(List<BasePiece> pieces, bool value) {

        //Debug.Log(pieces[0].mPieceColor + " is" + pieces[0].enabled);

        foreach(BasePiece piece in pieces) {
            piece.enabled = value;
        }
        
    }

    public void SwitchSides(Color teamColor) {

        Debug.Log("switch sides" + teamColor);

        bool isBlueTeamTurn;

        if (teamColor == Color.red)
            isBlueTeamTurn = true;
        else
            isBlueTeamTurn = false;

        foreach(BasePiece piece in mBluePieces) {
            Debug.Log(piece.mPieceColor);
        }
        SetInteractive(mBluePieces, isBlueTeamTurn);
        SetInteractive(mRedPieces, !isBlueTeamTurn);
        
    }

   

    void Start()
    {

    }


}
