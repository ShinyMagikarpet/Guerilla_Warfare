using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{


    public Image mOutlineImage;
    public Vector2Int mBoardPosition = Vector2Int.zero;
    public Board mBoard = null;
    public RectTransform mRectTransform = null;
    public BasePiece mCurrentPiece;

    public void Setup_Cell(Vector2Int newBoardPosition, Board newBoard) {
        mBoard = newBoard;
        mBoardPosition = newBoardPosition;

        mRectTransform = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData) {
        mOutlineImage.gameObject.SetActive(true);
        Debug.Log(gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData) {
        mOutlineImage.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) {

        if(mBoardPosition.y > 3) {
            Debug.Log("This is not your territory");
        } else {
            mCurrentPiece = Instantiate(mCurrentPiece, transform);
            mCurrentPiece.GetComponent<RectTransform>().position = mRectTransform.position;
        }
            
        
        
    }
}
