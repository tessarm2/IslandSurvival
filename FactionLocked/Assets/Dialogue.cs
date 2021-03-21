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
    public float textWaitSpeed = 0.02f;


    private int index = 0;
    private bool isTextMoving = false;
    private float speedMult = 1f;

    private void Start() {
        textDisplay.text = "";
        StartCoroutine(TypingRoutine());
    }

    IEnumerator TypingRoutine() {

        if (nextDisplay) nextDisplay.text = "";
        isTextMoving = true;

        yield return new WaitForSeconds(textWaitSpeed * speedMult * 20f);

        // displays each letter in a sentences at some display speed
        foreach (var letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(textWaitSpeed * speedMult);
        }

        yield return new WaitForSeconds(textWaitSpeed * speedMult * 20f);

        isTextMoving = false;
        if (nextDisplay) nextDisplay.text = ">";
    }

    public void NextSentence() {
        if (index < sentences.Length - 1) {
            index++;
            textDisplay.text = "";
            StartCoroutine(TypingRoutine());
        } else {
            textDisplay.text = "----  No more Text to Display  ----";
        }
    }

    private void Update() {
        speedMult = 1f;
        if (Input.GetKey("f")) {
            if (Input.GetKeyDown("f") && !isTextMoving) {
                NextSentence();
            } else {
                speedMult = 0.01f;
            }
        }
    }
}
