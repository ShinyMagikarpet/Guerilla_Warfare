using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : BasePiece
{

    public override void Setup_Piece(Color teamColor, Color32 spriteColor, PieceManager pieceManager) {

        base.Setup_Piece(teamColor, spriteColor, pieceManager);

        mMaxNum = 3;
        mMove = new Vector3Int(1, 1, 1);
        mSpecialCost = 0;
    }


    public override void Special_Move() {
        base.Special_Move();

        ClearPath();
        int numPieces = 0;


        mPieceManager.mSpecialActivated = true;

        Pathing();
        CreatePath(-2, 2, 2);
        CreatePath(-1, 2, 1);
        CreatePath(0, 2, 1);
        CreatePath(1, 2, 1);
        CreatePath(2, 2, 2);

        CreatePath(2, 0, 1);
        CreatePath(-2, 0, 1);

        CreatePath(2, 1, 1);
        CreatePath(-2, 1, 1);


        CreatePath(2, -1, 1);
        CreatePath(-2, -1, 1);

        CreatePath(-2, -2, 2);
        CreatePath(1, -2, 1);
        CreatePath(0, -2, 1);
        CreatePath(-1, -2, 1);
        CreatePath(2, -2, 2);


        foreach (Cell cell in mSelectedCells) {

            if (cell.mCurrentPiece) {
                numPieces++;
                cell.mCurrentPiece.enabled = true;
            }

        }

        if (numPieces == 0) {
            mSelectedCells.Clear();
            Debug.Log("no pieces dount");
            return;
        }
        ShowCells();



    }
}
