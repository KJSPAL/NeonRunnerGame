using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TrophyManager : MonoBehaviour
{
    public static bool gameOver;
    public static string winnerName;

    public TMP_Text textbox;  // assign a TextMeshProUGUI in the scene

    void Start()
    {
        gameOver = false;
        winnerName = "";
        Time.timeScale = 1f; // make sure we’re unfrozen on reload
    }

    void Update()
    {

        if (gameOver)
        {
            textbox.text = $"{winnerName} WINS!\nPress R to Restart";
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public static void DeclareWinner(string name)
    {
        winnerName = name;
        gameOver = true;
        Time.timeScale = 0f;          // freeze the game
    }
}

