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

    private BasePiece mSelectedPiece;

    public void Place_Piece(Cell cell, Color teamColor, Color32 spriteColor, PieceManager pieceManager) {
        int count = 0;

        if (teamColor == Color.red) {
            count = Get_Piece_Count(mRedPieces, mSelectedPiece);
            if(count >= mSelectedPiece.mMaxNum) {
                Debug.Log("You have maximum amount of this piece");
                return;
            }
            BasePiece newPiece = Instantiate(mSelectedPiece, transform);
            newPiece.Setup_Piece(teamColor, spriteColor, this);
            newPiece.Place_Piece(cell);
            mRedPieces.Add(newPiece);
        } else if(teamColor == Color.blue){
            count = Get_Piece_Count(mBluePieces, mSelectedPiece);
            if (count >= mSelectedPiece.mMaxNum) {
                Debug.Log("You have maximum amount of this piece");
                return;
            }
            BasePiece newPiece = Instantiate(mSelectedPiece, transform);
            newPiece.Setup_Piece(teamColor, spriteColor, this);
            newPiece.Place_Piece(cell);
            mBluePieces.Add(newPiece);
        }
        
    }

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

    public void Select_Warrior() {

        mSelectedPiece = mWarriorPiece;
        
    }

    public void Select_Archer() {
        Debug.Log("Archer is selected");
        mSelectedPiece = mArcherPiece;
    }

    public void Select_Wizard() {
        Debug.Log("Wizard is selected");
        mSelectedPiece = mWizardPiece;
    }

    public void Select_King() {
        Debug.Log("King is selected");
        mSelectedPiece = mKingPiece;
    }

    public void Select_Barricade() {
        Debug.Log("Barricade is selected");
        mSelectedPiece = mBarricadePiece;
    }

    void Start()
    {

        //Set Warrior by Default
        mSelectedPiece = mWarriorPiece;

    }


}
