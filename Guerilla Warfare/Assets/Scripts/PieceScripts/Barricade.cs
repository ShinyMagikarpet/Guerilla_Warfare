using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : BasePiece
{
    public override void Setup_Piece(Color teamColor, Color32 spriteColor, PieceManager pieceManager) {

        base.Setup_Piece(teamColor, spriteColor, pieceManager);

        mMaxNum = 10;
        mMove = new Vector3Int(0, 0, 0);
    }
}
