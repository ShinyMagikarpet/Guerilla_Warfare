using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : BasePiece
{

    public bool mIsAlive;

    public override void Setup_Piece(Color teamColor, Color32 spriteColor, PieceManager pieceManager) {

        base.Setup_Piece(teamColor, spriteColor, pieceManager);

        mMaxNum = 1;
        mMove = new Vector3Int(0, 0, 0);
        mIsAlive = true;

    }

    public override void Kill() {
        base.Kill();

        mIsAlive = false;
    }
}
