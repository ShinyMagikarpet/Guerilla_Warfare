using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{

    public GameObject mCellPrefab;
    public PieceManager mPieceManager;

    public Cell[,] mAllCells = new Cell[12, 12];

    public void Create(PieceManager pieceManager) {

        mPieceManager = pieceManager;
        char colomnLetter = 'A';
        int rowNum;

        for(int y = 0; y < 12; y++) {

            for(int x = 0; x < 12; x++) {

                rowNum = x + 1;
                GameObject newCell = Instantiate(mCellPrefab, transform);

                RectTransform rectTRansform = newCell.GetComponent<RectTransform>();
                rectTRansform.anchoredPosition = new Vector2((x * 100) + 50, (y * 100) + 50);

                mAllCells[x, y] = newCell.GetComponent<Cell>();
                mAllCells[x, y].Setup_Cell(new Vector2Int(x, y), this, mPieceManager);
                mAllCells[x, y].name = colomnLetter.ToString() + rowNum.ToString();
                //Debug.Log(mAllCells[x, y].name);

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
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
