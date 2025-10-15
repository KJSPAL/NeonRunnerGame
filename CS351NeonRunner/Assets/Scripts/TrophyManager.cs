using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TrophyManager : MonoBehaviour
{
    public static bool gameOver;
    public static string winnerName;

    public TMP_Text textbox;  // assign in Inspector

    void Start()
    {
        gameOver = false;
        winnerName = "";
        Time.timeScale = 1f; // ensure unfrozen when scene starts
    }

    void Update()
    {
        if (!gameOver) return;

        // show winner message
        if (textbox)
        {
            textbox.text = $"{winnerName} WINS!\nPress N to Load Next Level";
        }
        // press N to load next level
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
        }
    }

    public static void DeclareWinner(string name)
    {
        winnerName = name;
        gameOver = true;
        Time.timeScale = 0f; // freeze gameplay
    }

    private void LoadNextLevel()
    {
        Time.timeScale = 1f; // Unfreeze before loading next level

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        int nextIndex = currentIndex + 1;

        if (nextIndex >= totalScenes)
        {
            nextIndex = 0; // Wrap back to the first level if at the end
        }

        SceneManager.LoadScene(nextIndex);
    }
}
