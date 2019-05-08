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
        //cell.mCurrentPiece =  Instantiate(mWarriorPiece, cell.transform);

        if (teamColor == Color.red) {
            count = Get_Piece_Count(mRedPieces, mSelectedPiece);
            if(count >= mSelectedPiece.mMaxNum) {
                Debug.Log("You have maximum amount of this piece");
                return;
            }
            mRedPieces.Add(mSelectedPiece);
        } else if(teamColor == Color.blue){
            count = Get_Piece_Count(mBluePieces, mSelectedPiece);
            if (count >= mSelectedPiece.mMaxNum) {
                Debug.Log("You have maximum amount of this piece");
                return;
            }
            mBluePieces.Add(mSelectedPiece);
        }
        cell.mCurrentPiece = Instantiate(mSelectedPiece, cell.transform);
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

    public void Select_Warrior() {
        Debug.Log("Warrior is selected");
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

    // Start is called before the first frame update
    void Start()
    {

        //Set the max number of allowed 
        mWarriorPiece.Set_Max_Count(5);
        mArcherPiece.Set_Max_Count(5);
        mWizardPiece.Set_Max_Count(3);
        mKingPiece.Set_Max_Count(1);
        mBarricadePiece.Set_Max_Count(10);
        //Set this by Default
        mSelectedPiece = mWarriorPiece;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit() {
        
    }
}
