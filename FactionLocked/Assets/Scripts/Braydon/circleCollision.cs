using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using System;

public class circleCollision : MonoBehaviour
{
    public GameObject solveTextObj = null;

    private TMP_Text tmproMuted;

    // Start is called before the first frame update
    void Start()
    {
        tmproMuted = solveTextObj.GetComponent<TMP_Text>();
        tmproMuted.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            Debug.Log("Puzzle Solved!");
            tmproMuted.enabled = true;
        }
    }
}
