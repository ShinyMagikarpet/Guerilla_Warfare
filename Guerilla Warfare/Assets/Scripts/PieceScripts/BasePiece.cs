using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class BasePiece : EventSystem, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{

    [SerializeField]
    private Cell mCurrentCell;
    RectTransform mRectTransform;
    public Color mPieceColor;
    public PieceManager mPieceManager;

    public int mMaxNum;
    [HideInInspector]
    public Vector2Int mCellLocation;

    protected Vector3Int mMove = Vector3Int.one;
    protected Vector3Int mSpecialMove = Vector3Int.one;
    protected List<Cell> mSelectedCells = new List<Cell>();

    Cell mTargetCell;

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

    public void Reset_To_Current_Cell() {

        transform.position = mCurrentCell.transform.position;

    }

    public virtual void Kill() {

        mCurrentCell.mCurrentPiece = null;

        gameObject.SetActive(false);

    }

    public void Move() {

        mTargetCell.Remove_Piece();

        mCurrentCell.mCurrentPiece = null;

        mCurrentCell = mTargetCell;
        mCurrentCell.mCurrentPiece = this;

        transform.position = mCurrentCell.transform.position;
        mTargetCell = null;

    }

    public void CreatePath(int xDirection, int yDirection, int movement) {

        CellState cellState;

        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        for(int i = 0; i < movement; i++) {

            currentX += xDirection;
            currentY += yDirection;

            cellState = mCurrentCell.mBoard.Get_Cell_State(this, currentX, currentY);

            if(cellState == CellState.Enemy) {
                mSelectedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
                break;
            }

            if (cellState != CellState.Open)
                break;

            mSelectedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);

        }


    }

    public void Pathing() {

        //Horizontal
        CreatePath(1, 0, mMove.x);
        CreatePath(-1, 0, mMove.x);

        //Vertical
        CreatePath(0, 1, mMove.y);
        CreatePath(0, -1, mMove.y);

        //
        CreatePath(1, 1, mMove.z);
        CreatePath(-1, 1, mMove.z);

        CreatePath(-1, -1, mMove.z);
        CreatePath(1, -1, mMove.z);

    }

    public void ShowCells() {

        foreach(Cell cell in mSelectedCells) {
            cell.mOutlineImage.gameObject.SetActive(true);
            cell.mOutlineImage.GetComponent<Image>().color = Color.yellow;
        }

    }

    public void ClearPath() {

        foreach (Cell cell in mSelectedCells) {
            cell.mOutlineImage.GetComponent<Image>().color = Color.green;
            cell.mOutlineImage.gameObject.SetActive(false);
        }
        mSelectedCells.Clear();

    }

    public void OnPointerClick(PointerEventData eventData) {

        if (mPieceManager.mSelectedPiece == null) {
            mPieceManager.mSelectedPiece = this;
            Pathing();
            ShowCells();
        } 
        else {
            mPieceManager.mSelectedPiece.ClearPath();
            mPieceManager.mSelectedPiece = this;
            Pathing();
            ShowCells();
        }

    }

    public void OnBeginDrag (PointerEventData eventData) {
        //Highlight the spaces that the piece can move to
        Pathing();
        ShowCells();
        
    }

    public void OnDrag(PointerEventData eventData) {

        //Move the Piece
        transform.position = eventData.position;

        foreach(Cell cell in mSelectedCells) {

            if (RectTransformUtility.RectangleContainsScreenPoint(cell.mRectTransform, Input.mousePosition)) {
                mTargetCell = cell;
                break;
            }

            mTargetCell = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {

        ClearPath();

        if(mTargetCell == null) {
            Reset_To_Current_Cell();
        } else {
            Move();
            mPieceManager.SwitchSides(mPieceColor);
        }
    }
}
