using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Board mBoard;
    public PieceManager mPieceManager;

    private BasePiece mSelectedPiece;

    private int mBluePlayerMana;
    private int mRedPlayerMana;

    private string mWinnerMessage;


    private bool IsPlayerSetting(List<BasePiece> basePieces) {

        //There should be 26 pieces in total
        return basePieces.Count < 26;
    }

    private bool IsOneKingAlive() {

        King king;
        int aliveCount = 0;

        foreach(BasePiece piece in mPieceManager.mBluePieces) {

            if(piece.GetType() == typeof(King)) {

                king = (King)piece;
                if (king.mIsAlive)
                    aliveCount++;
                else {
                    mWinnerMessage = "Red has won!";
                    break;
                }
                    
                

            }

        }

        foreach (BasePiece piece in mPieceManager.mRedPieces) {

            if (piece.GetType() == typeof(King)) {

                king = (King)piece;
                if (king.mIsAlive)
                    aliveCount++;
                else {
                    mWinnerMessage = "Blue has won!";
                    break;
                }
                    


            }

        }

        return aliveCount < 2;


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

        //TODO: Create NML here

        //Debug.Log("Red player is setting");
        //Check if red player is setting up board
        while (IsPlayerSetting(mPieceManager.mRedPieces))
        {
            //Debug.Log("Player one is setting up his board");
            yield return null;
        }
        //Debug.Log("Red player has finished setting");
        //Debug.Log("Blue player has started setting");
        //Check if Blue player is setting up board
        while (IsPlayerSetting(mPieceManager.mBluePieces))
        {
            //Debug.Log("Player two is setting up his board");
            yield return null;
        }
        //Debug.Log("Blue Player has finished setting");

    }

    private IEnumerator GamePlaying()
    {

        
        while (!IsOneKingAlive()) {
            Debug.Log("Game is currently playing");
            yield return null;
        }
        Debug.Log(mWinnerMessage);
        mPieceManager.SetInteractive(mPieceManager.mBluePieces, false);
        mPieceManager.SetInteractive(mPieceManager.mRedPieces, false);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
