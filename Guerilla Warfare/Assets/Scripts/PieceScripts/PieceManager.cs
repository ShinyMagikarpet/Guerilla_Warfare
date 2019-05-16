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

    public Image mana;

    public int mBlueManaCount = 0, mRedManaCount = 0;

    public BasePiece mWarriorPiece;
    public BasePiece mArcherPiece;
    public BasePiece mWizardPiece;
    public BasePiece mKingPiece;
    public BasePiece mBarricadePiece;

    public BasePiece mSelectedPiece;

    public bool mSpecialActivated = false;

    //This will be used to preset the board
    //Will have to make x and y switch here to make it look nice
    private string[,] mRedPiecePlacement = new string[4, 12] {

        { "W", "B", " ", "A", "W", "B", "B", " ", " ", "A", " ", "W" },
        { " ", "W", " ", "B", "W", " ", "A", "B", "WI", "W", " ", " " },
        { "B", "B", " ", "A", " ", "B", " ", " ", " ", " ", "A", " " },
        { " ", " ", "WI", "B", "K", "A", " ", "B", " ", "WI", " ", " " }

    };
    
    //This will be used to preset the board
    //Not going to both reversing this with code
    private string[,] mBluePiecePlacement = new string[4, 12] {

        { " ", " ", "WI", "B", "K", "A", " ", "B", " ", "WI", " ", " " },
        { "B", "B", " ", "A", " ", "B", " ", " ", " ", " ", "A", " " },
        { " ", "W", " ", "B", "W", " ", "A", "B", "WI", "W", " ", " " },
        { "W", "B", " ", "A", "W", "B", "B", " ", " ", "A", " ", "W" }

    };


    public void Setup_Board(Board board) {

        mBluePieces = CreatePieces(Color.blue, new Color32(0, 0, 255, 255), board, mBluePiecePlacement);
        mRedPieces = CreatePieces(Color.red, new Color32(255, 0, 0, 255), board, mRedPiecePlacement);

        Place_Pieces(0, mBluePieces, board, true);
        Place_Pieces(8, mRedPieces, board, false);

        SwitchSides(Color.red);

    }

    public List<BasePiece> CreatePieces(Color teamColor, Color32 spriteColor, Board board, string[,] piecePlacement) {

        List<BasePiece> pieceList = new List<BasePiece>();
        int i, j;

        for(j = 0; j < 4; j++) {

            for (i = 0; i < 12; i++) {

                BasePiece newPiece;

                switch (piecePlacement[j, i]) {
                    case "W":
                        newPiece = Instantiate(mWarriorPiece, transform);
                        break;
                    case "A":
                        newPiece = Instantiate(mArcherPiece, transform);
                        break;
                    case "WI":
                        newPiece = Instantiate(mWizardPiece, transform);
                        break;
                    case "B":
                        newPiece = Instantiate(mBarricadePiece, transform);
                        break;
                    case "K":
                        newPiece = Instantiate(mKingPiece, transform);
                        break;
                    default:
                        continue;
                }

                newPiece.Setup_Piece(teamColor, spriteColor, this);
                newPiece.mCellLocation = new Vector2Int(i, j);
                pieceList.Add(newPiece);

            }
        }

        

        return pieceList;
    }

    public void Place_Pieces(int startRow, List<BasePiece> pieces, Board board, bool reverse) {

        int i;

        if (!reverse) {
            for (i = 0; i < pieces.Count; i++) {
                pieces[i].Place_Piece(board.mAllCells[pieces[i].mCellLocation.x, pieces[i].mCellLocation.y + startRow]);
            }
        } 
        else { 

            for (i = 0; i < pieces.Count; i++) {
                pieces[i].Place_Piece(board.mAllCells[pieces[i].mCellLocation.x, pieces[i].mCellLocation.y + startRow]);
            }
        }
        

    }

    //public void Place_Piece(Cell cell, Color teamColor, PieceManager pieceManager) {

    //    if (teamColor == Color.red) {
    //        BasePiece newPiece = Instantiate(mSelectedPiece, transform);
    //        newPiece.Setup_Piece(teamColor, spriteColor, this);
    //        newPiece.Place_Piece(cell);
    //        mRedPieces.Add(newPiece);
    //    } else if(teamColor == Color.blue){
    //        BasePiece newPiece = Instantiate(mSelectedPiece, transform);
    //        newPiece.Setup_Piece(teamColor, spriteColor, this);
    //        newPiece.Place_Piece(cell);
    //        mBluePieces.Add(newPiece);
    //    }

    //}

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

        if (teamColor == Color.red) {
            isBlueTeamTurn = true;
            if(mBlueManaCount < 5)
                mBlueManaCount++;
        } else {
            isBlueTeamTurn = false;
            if(mRedManaCount < 5)
                mRedManaCount++;
        }
        //Debug.Log("Blue player has " + mBlueManaCount + " mana.");    
        //Debug.Log("Red player has " + mRedManaCount + " mana.");    

        SetInteractive(mBluePieces, isBlueTeamTurn);
        SetInteractive(mRedPieces, !isBlueTeamTurn);
        
    }

    public void Special_Activate() {
        mSpecialActivated = mSpecialActivated ? false : true;
        Debug.Log(mSpecialActivated);
    }


}
