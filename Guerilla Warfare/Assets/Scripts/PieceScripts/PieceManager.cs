using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceManager : MonoBehaviour, IPointerClickHandler
{

    List<BasePiece> mRedPieces;
    List<BasePiece> mBluePieces;
    public GameObject mWarriorPiece;
    public GameObject mArcherPiece;
    public GameObject mWizardPiece;
    public GameObject mKingPiece;
    public GameObject mBarricadePiece;
    [HideInInspector]
    public Color mPlayerColor;


    public void OnPointerClick(PointerEventData eventData) {

        Debug.Log("Fucker");

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
