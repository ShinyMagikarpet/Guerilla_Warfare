using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState {
    None,
    Friendly,
    Enemy,
    Open,
    NoMansLand,
    OutOfBounds
}

public class Board : MonoBehaviour
{

    public GameObject mCellPrefab;
    [HideInInspector]
    public PieceManager mPieceManager;

    public Cell[,] mAllCells = new Cell[12, 12];


    public Sprite mNoMansLandSprite;

    private string[,] mNoMansLandMap = new string[4, 12] {

        { "x", "x", " ", " ", " ", " ", " ", " ", " ", " ", "x", "x" },
        { "x", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "x" },
        { "x", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "x" },
        { "x", "x", " ", " ", " ", " ", " ", " ", " ", " ", "x", "x" }

    };

    public void Create(PieceManager pieceManager) {

        mPieceManager = pieceManager;
        char colomnLetter = 'A';
        int rowNum;

        for(int y = 0; y < 12; y++) {

            for(int x = 0; x < 12; x++) {

                rowNum = x + 1;
                GameObject newCell = Instantiate(mCellPrefab, transform);

                RectTransform rectTRansform = newCell.GetComponent<RectTransform>();
                rectTRansform.anchoredPosition = new Vector2((x * 100) + 100, (y * 100) + 50);

                mAllCells[x, y] = newCell.GetComponent<Cell>();
                mAllCells[x, y].Setup_Cell(new Vector2Int(x, y), this, mPieceManager);
                mAllCells[x, y].name = colomnLetter.ToString() + rowNum.ToString();

            }

           colomnLetter++;
        }

        for(int x = 0; x < 12; x+= 2) {

            for(int y = 0; y < 12; y++) {

                int offset = (y % 2 != 0) ? 0 : 1;
                int finalX = x + offset;

                mAllCells[finalX, y].GetComponent<Image>().color = new Color32(180, 180, 180, 255);


            }
        }


        //Need to do a seperate loop to setup no man's land
        for(int y = 0; y < 4; y++) {

            for(int x = 0; x < 12; x++) {

                if(mNoMansLandMap[y,x] == "x") {
                    mAllCells[x, y + 4].GetComponent<Image>().sprite = mNoMansLandSprite;
                }
                    
                                    

            }

        }

        
    }

    public CellState Get_Cell_State(BasePiece piece, int targetX, int targetY) {

        if (targetX > 11 || targetX < 0)
            return CellState.OutOfBounds;

        if (targetY > 11 || targetY < 0)
            return CellState.OutOfBounds;

        Cell targetCell = mAllCells[targetX, targetY];

        if(targetCell.mCurrentPiece != null) {

            if (piece.mPieceColor == targetCell.mCurrentPiece.mPieceColor)
                return CellState.Friendly;
            else
                return CellState.Enemy;
        }

        if(targetCell.GetComponent<Image>().sprite == mNoMansLandSprite) {
            return CellState.NoMansLand;
        }

        return CellState.Open;

    }
}
