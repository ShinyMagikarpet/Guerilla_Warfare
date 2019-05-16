using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer : BasePiece
{
    public override void Setup_Piece(Color teamColor, Color32 spriteColor, PieceManager pieceManager) {

        base.Setup_Piece(teamColor, spriteColor, pieceManager);

        mMaxNum = 6;
        mIsMoveable = true;
        mMove = new Vector3Int(0, 0, 1);
        mSpecialCost = 3;

    }

    public override void Special_Move() {
        base.Special_Move();

        int numEnemies = 0;

        mPieceManager.mSpecialActivated = true;

        ClearPath();
        CreatePath(1, 2, 1);
        CreatePath(0, 2, 1);
        CreatePath(-1, 2, 1);

        CreatePath(-2, 1, 1);
        CreatePath(-2, 0, 1);
        CreatePath(-2, -1, 1);


        CreatePath(1, -2, 1);
        CreatePath(0, -2, 1);
        CreatePath(-1, -2, 1);

        CreatePath(2, 1, 1);
        CreatePath(2, 0, 1);
        CreatePath(2, -1, 1);

        Cell cell;
        //Loop to check if the cells in archer's range contains a piece
        for (int i = mSelectedCells.Count - 1; i >= 0; i--) {
            cell = mSelectedCells[i];

            if (cell.mCurrentPiece == null) {
                mSelectedCells.RemoveAt(i);
                continue;
            }

            if (cell.mCurrentPiece) {
                cell.mCurrentPiece.GetComponent<Image>().raycastTarget = false;
                numEnemies++;
            }

        }

        if (numEnemies == 0) {
            mSelectedCells.Clear();
            return;
        }

        ShowCells();
    }
}
