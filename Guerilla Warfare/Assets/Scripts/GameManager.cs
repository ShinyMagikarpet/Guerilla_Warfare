using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Board mBoard;

    public PieceManager mPieceManager;

    private bool mIsPlaying = false;
    private bool mIsPlayerOneSetting = true;
    private bool mIsPlayerTwoSetting = true;

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
        while (mIsPlayerOneSetting)
        {
            //Debug.Log("Player one is setting up his board");
            yield return null;
        }

        while (mIsPlayerTwoSetting)
        {
            //Debug.Log("Player two is setting up his board");
            yield return null;
        }
    }

    private IEnumerator GamePlaying()
    {
        
        while (mIsPlaying) {
            //Debug.Log("Game is currently playing");
            yield return null;
        }
        //Debug.Log("Game is done");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mIsPlayerOneSetting)
                mIsPlayerOneSetting = false;
            else if (mIsPlayerTwoSetting)
                mIsPlayerTwoSetting = false;
        }
    }


}
