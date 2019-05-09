using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Board mBoard;
    public PieceManager mPieceManager;

    private BasePiece mSelectedPiece;


    private bool IsPlayerSetting(List<BasePiece> basePieces) {

        //There should be 26 pieces in total
        return basePieces.Count < 26;
    }

    // Start is called before the first frame update
    void Start()
    {
        mBoard.Create(mPieceManager);
        
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        // Player set up their side of the board First
        yield return StartCoroutine(SetupPhase());

        // Game is finished and start ending the game
        yield return StartCoroutine(GamePlaying());

        
    }

    private IEnumerator SetupPhase()
    {
        Debug.Log("Red player is setting");
        //Check if red player is setting up board
        while (IsPlayerSetting(mPieceManager.mRedPieces))
        {
            //Debug.Log("Player one is setting up his board");
            yield return null;
        }
        Debug.Log("Red player has finished setting");
        Debug.Log("Blue player has started setting");
        //Check if Blue player is setting up board
        while (IsPlayerSetting(mPieceManager.mBluePieces))
        {
            //Debug.Log("Player two is setting up his board");
            yield return null;
        }
        Debug.Log("Blue Player has finished setting");
    }

    private IEnumerator GamePlaying()
    {

        while (1 > 0) {
            //Debug.Log("Game is currently playing");
            yield return null;
        }
        //Debug.Log("Game is done");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
