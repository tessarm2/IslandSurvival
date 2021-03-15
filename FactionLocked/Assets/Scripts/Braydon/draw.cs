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

    // line tracker variables
    public Stack<int> directions;
    private List<Transform> path;
    private bool goingBackwards = false;


    // Start is called before the first frame update
    void Start()
    {
        // used later to ensure rotation doesn't change
        circleRotation = circle.transform.rotation;

        // erasing lines
        path = new List<Transform>();
        directions = new Stack<int>();
    }

    // Update is called once per frame
    void Update()
    {
        // to ensure rotation doesn't change later
        circle.transform.rotation = circleRotation;
        circle.GetComponent<Rigidbody>().velocity = Vector3.zero;

        dx = Input.GetAxis("Horizontal") * speed * Time.deltaTime;;
        dy = Input.GetAxis("Vertical") * speed * Time.deltaTime;;
        // to restrict diagonal movement
        // if (dx > 0f) {
        //     dy = 0f;
        // } else if (dx < 0f) {
        //     dy = 0f;
        // } else if (dy > 0f) {
        //     dx = 0f;
        // } else if (dy < 0f) {
        //     dx = 0f;
        // }
        lineTracker(dx, dy);

        // 2d movement tracking
        circle.transform.Translate(dx, dy, 0.0f);

        // Vector3 clampedPos = circle.transform.position;
        // clampedPos.x = Mathf.Clamp(clampedPos.x, -1.13f, 1.13f);
        // clampedPos.y = Mathf.Clamp(clampedPos.y, 2.25f, 3.35f);
        // circle.transform.position = clampedPos;
        if ((dx != 0.0f || dy != 0.0f) && !goingBackwards)
        {
            Transform squarePart = Instantiate(square.transform, circle.transform.position, circle.transform.rotation);
            squarePart.SetParent(PathParent.transform);
            path.Add(squarePart);
        }
    }

    // Tessa's work on detecting and erasing drawn lines when moving backwards
    void lineTracker(float dx, float dy) {
        Debug.Log(dx);
        if (dx != 0 && dy != 0 )
        {
            return;
        }
        if (dx > 0)
        {
            if (directions.Count > 0 && directions.Peek() == 3)
            {
                //going backwards
                Debug.Log("Going back");
                goingBackwards = true;
                directions.Pop();
                Destroy(path[path.Count - 1].gameObject);
                path.RemoveAt(path.Count - 1);
            } else
            {
                directions.Push(1);
                Debug.Log("Going forth");

                goingBackwards = false;
            }
        }
        if (dx < 0)
        {
            if (directions.Count > 0 && directions.Peek() == 1)
            {
                //going backwards
                Debug.Log("Going back");
                goingBackwards = true;
                directions.Pop();
                Destroy(path[path.Count - 1].gameObject);
                path.RemoveAt(path.Count - 1);
            } else
            {
                directions.Push(3);
                Debug.Log("Going forth");

                goingBackwards = false;

            }
        }
        if (dy > 0)
        {
            if (directions.Count > 0 && directions.Peek() == 2)
            {
                //going backwards
                Debug.Log("Going back");
                goingBackwards = true;
                directions.Pop();
                Destroy(path[path.Count - 1].gameObject);
                path.RemoveAt(path.Count - 1);
            } else
            {
                directions.Push(0);
                Debug.Log("Going forth");

                goingBackwards = false; 
            }
        }
        if (dy < 0)
        {
            if (directions.Count > 0 && directions.Peek() == 0)
            {
                //going backwards
                Debug.Log("Going back");
                goingBackwards = true;
                directions.Pop();
                Destroy(path[path.Count - 1].gameObject);
                path.RemoveAt(path.Count - 1);
            } else
            {
                directions.Push(2);
                Debug.Log("Going forth");

                goingBackwards = false;
            }
        }
    }
}
