using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draw : MonoBehaviour
{

    public GameObject square;
    public GameObject circle;
    public GameObject PathParent;

    public float speed = 0.05f;
    public float dx;
    public float dy;

    private Vector2 screenBounds;
    private Quaternion circleRotation;

    // Start is called before the first frame update
    void Start()
    {
        circleRotation = circle.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        circle.transform.rotation = circleRotation;
        circle.GetComponent<Rigidbody>().velocity = Vector3.zero;

        dx = Input.GetAxis("Horizontal") * speed;
        dy = Input.GetAxis("Vertical") * speed;
        circle.transform.Translate(dx, dy, 0.0f);
        // Vector3 clampedPos = circle.transform.position;
        // clampedPos.x = Mathf.Clamp(clampedPos.x, -1.13f, 1.13f);
        // clampedPos.y = Mathf.Clamp(clampedPos.y, 2.25f, 3.35f);
        // circle.transform.position = clampedPos;
        if (dx != 0.0f || dy != 0.0f)
        {
            Transform squarePart = Instantiate(square.transform, circle.transform.position, circle.transform.rotation);
            squarePart.SetParent(PathParent.transform);
        }
    }
}
