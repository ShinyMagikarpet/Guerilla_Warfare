using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class BasePiece : EventSystem
{

    Cell mCurrentCell;
    RectTransform mRectTransform;
    public Color mPieceColor;
    PieceManager mPieceManager;

    public virtual void Setup_Piece(Color teamColor, Color32 spriteColor, PieceManager pieceManager) {

        mPieceColor = teamColor;

        mPieceManager = pieceManager;
        GetComponent<Image>().color = spriteColor;
        mRectTransform = GetComponent<RectTransform>();

    }

    public void Place_Piece(Cell targetCell) {
        mCurrentCell = targetCell;
        mCurrentCell.mCurrentPiece = this;
        
        transform.position = mCurrentCell.transform.position;
    }


}
