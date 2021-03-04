using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draw : MonoBehaviour
{

    public GameObject square;
    public GameObject circle;
    float speed = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * speed;
        float dy = Input.GetAxis("Vertical") * speed;
        circle.transform.Translate(dx, dy, 0.0f);
        if (dx != 0.0f || dy != 0.0f) {
            Instantiate(square.transform, circle.transform.position, circle.transform.rotation);
        }
    }
}
