using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentenses;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dialoguePanel;
    private void OnEnable()
    {
        continueButton.SetActive(false);
        StartCoroutine(Type());
    }

    //Coroutine to type one letter at a time in the dialogue box
    IEnumerator Type()
    {
        //Start the textbox as empty
        textbox.text = "";
        //Loop through each letter in the current sentense
        foreach (char letter in sentenses[index].ToCharArray())
        {
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        continueButton.SetActive(true);
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentenses.Length - 1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textbox.text = "";
            dialoguePanel.SetActive(false);
        }
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
