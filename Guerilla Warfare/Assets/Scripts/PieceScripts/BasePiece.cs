using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class BasePiece : EventSystem, IDragHandler
{

    Cell mCurrentCell;
    RectTransform mRectTransform;
    public Color mPieceColor;
    protected PieceManager mPieceManager;
    [HideInInspector]
    public int mMaxNum; /*< Maximum number of a piece that is allowed */

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

    public void Set_Max_Count(int num) {
        this.mMaxNum = num;
    }

    

    public void OnBeingDrag(PointerEventData eventData) {
        //Highlight the spaces that the piece can move to
        Debug.Log("Picked up piece");
    }

    public void OnDrag(PointerEventData eventData) {
        //Move the Piece
        
        transform.position = eventData.position;
    }

    public void OnDragEnd(PointerEventData eventData) {
        //Place the piece on the cell
    }

}
