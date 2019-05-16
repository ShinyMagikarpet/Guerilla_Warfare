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
    public int mSpecialCost;
    public List<Cell> mSelectedCells = new List<Cell>();

    public Cell mTargetCell;
    public bool mIsMoveable;


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
        mPieceManager.mPieceDrag = null;

    }

    public virtual void Kill() {

        mCurrentCell.mCurrentPiece = null;

        gameObject.SetActive(false);

    }

    public void Move() {

        mPieceManager.mPieceDrag.mTargetCell.Remove_Piece();

        mPieceManager.mPieceDrag.mCurrentCell.mCurrentPiece = null;

        mPieceManager.mPieceDrag.mCurrentCell = mTargetCell;
        mPieceManager.mPieceDrag.mCurrentCell.mCurrentPiece = this;

        mPieceManager.mPieceDrag.transform.position = mCurrentCell.transform.position;
        
        if(mPieceManager.mPieceDrag.mCurrentCell.GetComponent<Image>().sprite == mCurrentCell.mBoard.mNoMansLandSprite) {
            Debug.Log("landed in nml");
            mCurrentCell.mCurrentPiece = null;
            gameObject.SetActive(false);
        }
        mPieceManager.mPieceDrag.mTargetCell = null;

    }

    public virtual void Special_Move() {


        
    }

    public void CreatePath(int xDirection, int yDirection, int movement) {

        CellState cellState;

        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        for(int i = 0; i < movement; i++) {

            currentX += xDirection;
            currentY += yDirection;

            cellState = mCurrentCell.mBoard.Get_Cell_State(this, currentX, currentY);

            if (mPieceManager.mSelectedPiece != null && mPieceManager.mSelectedPiece.GetType() == typeof(Wizard) && mPieceManager.mSpecialActivated) {
                if (cellState == CellState.OutOfBounds) break;
                mSelectedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
                break;
            }

            if((mPieceManager.mSpecialActivated || mPieceManager.mSpecialUsed) && cellState == CellState.Barricade) {
                mSelectedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
                break;
            }

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

        if (mPieceManager.mSpecialUsed || mPieceManager.mSpecialActivated) return;

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
        if (mPieceManager.mSpecialActivated || mPieceManager.mSpecialUsed) return;
        Pathing();
        ShowCells();
        
    }

    public void OnDrag(PointerEventData eventData) {


        if (mPieceManager.mSpecialActivated && mPieceManager.mSelectedPiece.GetType() == typeof(Wizard)) {
            if (this != mPieceManager.mSelectedPiece) {
                mPieceManager.mPieceDrag = eventData.pointerDrag.GetComponent<BasePiece>();
                mSelectedCells = mPieceManager.mSelectedPiece.mSelectedCells;
                goto MOVE;
            }

            else
                return;

        }
            

        if (this != mPieceManager.mSelectedPiece && mPieceManager.mSpecialUsed) return;


        if (this == mPieceManager.mSelectedPiece && mPieceManager.mSpecialActivated && GetType() == typeof(Archer)) return;

        mPieceManager.mPieceDrag = eventData.pointerDrag.GetComponent<BasePiece>();

        //Prevent player from dragging pieces that aren't meant to move
        if (mMove == Vector3Int.zero)
            return;

        MOVE:
        //Move the Piece
        mPieceManager.mPieceDrag.transform.position = eventData.position;

        foreach(Cell cell in mSelectedCells) {
            
            //Check if mouse is hovering over a rect tranform of a cell from selected cells and set the target to that cell
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.mRectTransform, Input.mousePosition)) {
                mPieceManager.mPieceDrag.mTargetCell = cell;
                break;
            }
            mPieceManager.mPieceDrag.mTargetCell = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {

        if (this == mPieceManager.mSelectedPiece && mPieceManager.mSpecialActivated && GetType() == typeof(Archer)) return;

        ClearPath();

        if(mTargetCell == null) {
            Reset_To_Current_Cell();

        } 
        else if (mPieceManager.mSpecialActivated == true) {
            
            if(mPieceManager.mPieceDrag.GetType() == typeof(Warrior) && mPieceManager.mSelectedPiece.GetType() != typeof(Wizard)) {
                Move();
                mPieceManager.mSpecialActivated = false;
                mPieceManager.mSpecialUsed = true;
                ClearPath();
                Pathing();
                ShowCells();
            }

            if(mPieceManager.mSelectedPiece.GetType() == typeof(Wizard)) {
                Move();
                mPieceManager.mSelectedPiece.mSelectedCells.Clear();
                mPieceManager.SwitchSides(mPieceManager.mSelectedPiece.mPieceColor);
                mPieceManager.mSelectedPiece = null;
                mPieceManager.mSpecialUsed = false;
                mPieceManager.mSpecialActivated = false;
                mPieceManager.mPieceDrag = null;

            }
        }
        else {
            Move();
            mPieceManager.SwitchSides(mPieceColor);
            mPieceManager.mSelectedPiece = null;
            mPieceManager.mSpecialUsed = false;
            mPieceManager.mSpecialActivated = false;
            mPieceManager.mPieceDrag = null;
        }

        
    }

}
