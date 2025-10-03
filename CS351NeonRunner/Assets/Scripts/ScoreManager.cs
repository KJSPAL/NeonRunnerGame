using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    //notice public static variables can be accessed from any script but cannot be scene in the inspector panel.
    public static bool gameOver;
    public static bool won;
    public static int score;

    //Set this in the inspector
    public TMP_Text textbox;
    public int scoreToWin;
    public GameObject player1; // added by Johcori 
    public GameObject player2; //  added by Johcori


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameOver = false;
        won = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            textbox.text = "Score: " + score;
        }
        if (score >= scoreToWin)
        {
            gameOver = true;
            won = true;
        }

        if(gameOver)
        {
            if (won)
            {
                //display "You Win!"
                textbox.text = "You Win!\nPress R to Try Again";
            }
            else
            { 
                //display you lose!
                textbox.text = "You Lose!\nPress R to Try Again";
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);



            }
        }
    }
}
