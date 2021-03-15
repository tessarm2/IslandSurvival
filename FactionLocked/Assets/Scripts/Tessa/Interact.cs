using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    bool inRange = false;
    public Behaviour halo;
    public GameObject mazeCanvas;
    public GameObject puzzleCam;
    public GameObject playerBody;

    // Start is called before the first frame update
    void Start()
    {
        playerBody.SetActive(true);
        puzzleCam.SetActive(false);
        mazeCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 3D player to puzzle
        if (Input.GetKeyDown(KeyCode.E) && inRange && playerBody.activeSelf)
        {
            swapEnabled();
            Debug.Log("User interacted with puzzle");
        }

        // puzzle to 3D player
        if (Input.GetKeyDown(KeyCode.X) && inRange && !playerBody.activeSelf)
        {
            swapEnabled();
            Debug.Log("User exited the puzzle");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inRange = true;
            halo.enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            halo.enabled = false;
        }
    }

    void swapEnabled() {
        playerBody.SetActive(!playerBody.activeSelf);
        puzzleCam.SetActive(!puzzleCam.activeSelf);
        mazeCanvas.SetActive(!mazeCanvas.activeSelf);
    }
}
