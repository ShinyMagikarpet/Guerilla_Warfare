using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : BasePiece
{
    public override void Setup_Piece(Color teamColor, Color32 spriteColor, PieceManager pieceManager) {

        base.Setup_Piece(teamColor, spriteColor, pieceManager);

        mMaxNum = 3;
        mMove = new Vector3Int(1, 1, 1);
    }
}
