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
    private float storedTimeDelta;
    private bool buttonHeld = false;



    public int startIndex = 0;

    private int index = 0;
    private int endingIndex = 10;
    private bool isTextMoving = false;
    private float speedMult = 1f; // decrease to move text faster !!

    private bool isDone = false; // check is a dialogue scenario has reached it's current end

    public void ResetDialogue() {
        StopCoroutine(currRoutine);
        textDisplay.text = "";
        index = startIndex; // starting index for character's dialogue
        speedMult = 1f;
        isDone = false;
        currRoutine = StartCoroutine(TypingRoutine());
    }

    private void Start() {
        textDisplay.text = "";
        index = startIndex;
        speedMult = 1f;
        currRoutine = StartCoroutine(TypingRoutine());
    }

    IEnumerator TypingRoutine() {

        if (nextDisplay) nextDisplay.text = "";
        isTextMoving = true;
        if (!buttonHeld) speedMult = 1f;

        if (!buttonHeld) yield return new WaitForSeconds(textWaitTime * speedMult * 5f);

        // displays each letter in a sentences at some display speed
        foreach (var letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(textWaitTime * speedMult);
        }

        if (!buttonHeld) yield return new WaitForSeconds(textWaitTime * speedMult * 15f);

        isTextMoving = false;
        if (nextDisplay && !isDone) nextDisplay.text = ">";
    }

    public void NextSentence() {
        if (index < sentences.Length - 1) {
            if (index >= endingIndex) { // ends for a given dialogue text scenario
                textDisplay.text = "----  No more Text to Display (by index) -- Press X to leave ----";
                isDone = true;
                return;
            }
            index++;
            textDisplay.text = "";
            currRoutine = StartCoroutine(TypingRoutine());
        } else {
            textDisplay.text = "----  No more Text to Display (by end of array) ----";
        }
    }

    private void Update() {
        if (!isDone) {
            if (Input.GetKeyDown("f")) {
                if (!isTextMoving) {
                    NextSentence();
                } else {
                    toggleSpeedMult();
                }
            }

            // This allows us to hold down f and skip stuff even faster after a couple seconds
            if (Input.GetKey("f")) {
                storedTimeDelta += Time.deltaTime;
                if (storedTimeDelta >= 1.7f) {
                    buttonHeld = true;
                } else {
                    buttonHeld = false;
                }
                if (buttonHeld) {
                    if (!isTextMoving) {
                        NextSentence();
                    }
                    if (speedMult == 1f) toggleSpeedMult();
                }
            } else {
                storedTimeDelta = 0f;
            }
        }
    }

    private void toggleSpeedMult() {
        if (speedMult != 1f) {
            speedMult = 1f;
        } else {
            speedMult = 0.02f;
        }
    }
}
