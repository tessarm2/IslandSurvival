using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// followed by tutorial on YouTube by BlackThornProd
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI speakerDisplay;
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI nextDisplay;
    public string[] sentences;
    public float textWaitTime = 0.02f;

    Coroutine currRoutine;



    private int index = 0;
    private bool isTextMoving = false;
    private float speedMult = 1f;

    public void ResetDialogue() {
        StopCoroutine(currRoutine);
        textDisplay.text = "";
        index = 0; // starting index for character's dialogue
        currRoutine = StartCoroutine(TypingRoutine());
    }

    private void Start() {
        textDisplay.text = "";
        index = 0;
        currRoutine = StartCoroutine(TypingRoutine());
    }

    IEnumerator TypingRoutine() {

        if (nextDisplay) nextDisplay.text = "";
        isTextMoving = true;
        speedMult = 1f;

        yield return new WaitForSeconds(textWaitTime * speedMult * 5f);

        // displays each letter in a sentences at some display speed
        foreach (var letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(textWaitTime * speedMult);
        }

        yield return new WaitForSeconds(textWaitTime * speedMult * 15f);

        isTextMoving = false;
        if (nextDisplay) nextDisplay.text = ">";
    }

    public void NextSentence() {
        if (index < sentences.Length - 1) {
            index++;
            textDisplay.text = "";
            currRoutine = StartCoroutine(TypingRoutine());
        } else {
            textDisplay.text = "----  No more Text to Display  ----";
        }
    }

    // private void onCanvasEnabled() {
    //     textDisplay.text = "";
    //     index = 0;
    //     StartCoroutine(TypingRoutine());
    // }

    private void Update() {
        if (Input.GetKeyDown("f")) {
            if (!isTextMoving) {
                NextSentence();
            } else {
                toggleSpeedMult();
            }
        }
    }

    private void toggleSpeedMult() {
        if (speedMult == 0.01f) {
            speedMult = 1f;
        } else {
            speedMult = 0.01f;
        }
    }
}
