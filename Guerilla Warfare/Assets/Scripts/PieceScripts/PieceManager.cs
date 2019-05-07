using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceManager : MonoBehaviour
{

    List<BasePiece> mRedPieces;
    List<BasePiece> mBluePieces;
    public BasePiece mWarriorPiece;
    public BasePiece mArcherPiece;
    public BasePiece mWizardPiece;
    public BasePiece mKingPiece;
    public BasePiece mBarricadePiece;
    [HideInInspector]
    public Color mPlayerColor;

    public void Place_Piece(Cell cell) {
        cell.mCurrentPiece =  Instantiate(mWarriorPiece, cell.transform);
        Debug.Log("Instantiated piece???");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
