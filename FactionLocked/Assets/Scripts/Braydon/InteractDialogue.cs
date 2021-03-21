using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDialogue : MonoBehaviour
{

    bool inRange = false;
    bool outRange = false;
    public Behaviour halo;
    public GameObject dialogueCanvas;
    public GameObject dialogueManager;
    // public GameObject puzzleCam;
    // public GameObject playerBody;

    // Start is called before the first frame update
    void Start()
    {
        // playerBody.SetActive(true);
        // puzzleCam.SetActive(false);
        dialogueCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 3D player to dialogue
        if (Input.GetKeyDown(KeyCode.E) && inRange && !dialogueCanvas.activeSelf)
        {
            // outRange = false;
            SwapEnabled();
            dialogueManager.GetComponent<Dialogue>().ResetDialogue();
            Debug.Log("User interacted with a character. Brings up Dialogue Box");
        }

        // leave dialogue box
        if (Input.GetKeyDown(KeyCode.X))
        {
            dialogueManager.GetComponent<Dialogue>().ResetDialogue();
            SwapEnabled();
            Debug.Log("User finished the interaction with the character");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if (other.GetType() == typeof(SphereCollider)) {
            if(other.gameObject.tag == "Player")
            {
                inRange = true;
                halo.enabled = true;
            }
        // }
    }
    void OnTriggerExit(Collider other)
    {
        // if (other.GetType() == typeof(SphereCollider)) {
            if (other.gameObject.tag == "Player")
            {
                inRange = false;
                halo.enabled = false;
            }
        // }
        // if (other.GetType() == typeof(BoxCollider)) {
        //     if(other.gameObject.tag == "Player")
        //     {
        //         outRange = true;
        //     }
        // }
        
    }

    void SwapEnabled() {
        // playerBody.SetActive(!playerBody.activeSelf);
        // puzzleCam.SetActive(!puzzleCam.activeSelf);
        dialogueCanvas.SetActive(!dialogueCanvas.activeSelf);
    }
}
