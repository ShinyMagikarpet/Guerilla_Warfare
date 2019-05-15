using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Board mBoard;
    public PieceManager mPieceManager;

    public Image mPlayerMana;

    public Image[] mRedPlayerMana = new Image[5];
    public Image[] mBluePlayerMana = new Image[5];

    private BasePiece mSelectedPiece;

    public Text mWinnerText;
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
                    mWinnerMessage = "Red Player ";
                    mWinnerText.color = Color.red;
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
                    mWinnerMessage = "Blue Player ";
                    mWinnerText.color = Color.blue;
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
        mPieceManager.Setup_Board(mBoard);

        int i;
        for(i = 0; i < mRedPlayerMana.Length; i++) {
            mRedPlayerMana[i] = Instantiate(mPlayerMana, transform);
            mRedPlayerMana[i].color = Color.red;
            mRedPlayerMana[i].rectTransform.position += new Vector3(40 * i, 200, 0);
            mRedPlayerMana[i].enabled = false;
            mBluePlayerMana[i] = Instantiate(mPlayerMana, transform);
            mBluePlayerMana[i].color = Color.blue;
            mBluePlayerMana[i].rectTransform.position += new Vector3(40 * i, -200, 0);
            mBluePlayerMana[i].enabled = false;
        }

        
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        // Player set up their side of the board First
        //Not being used anymore
       // yield return StartCoroutine(SetupPhase());

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
        mWinnerText.text = mWinnerMessage + " Wins!";
        mWinnerText.enabled = true;
        mPieceManager.SetInteractive(mPieceManager.mBluePieces, false);
        mPieceManager.SetInteractive(mPieceManager.mRedPieces, false);
    }

    // Update is called once per frame
    void Update()
    {

        for(int i = 0; i < 5; i++) {
            mRedPlayerMana[i].enabled = mPieceManager.mRedManaCount > i ? true : false;
            mBluePlayerMana[i].enabled = mPieceManager.mBlueManaCount > i ? true : false;
        }

    }


}
