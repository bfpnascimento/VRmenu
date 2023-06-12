using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererSettings : MonoBehaviour
{
    //Declare a LineRenderer to store the component attached to the GameObject.
    [SerializeField] LineRenderer rend;

    //Settings for the LineRenderer are stored as a Vector3 array of points. Set up a V3 array to //initialize in Start.
    Vector3[] points;

    public LayerMask layerMask;

    //declare the panel to change
    public Image img;
    public GameObject panel;
    public Button btn;

    public void ColorChangeOnClick()
    {
        if (btn != null)
        {
            if (btn.name == "red_btn")
            {
                img.color = Color.red;
                Debug.Log("Red");
            }
            else if (btn.name == "blue_btn")
            {
                img.color = Color.blue;
                Debug.Log("Blue");
            }
            else if (btn.name == "green_btn")
            {
                img.color = Color.green;
                Debug.Log("Green");
            }
        }
    }

    public void ColorChangeOnClick2(Button btn2)
    {
        if (btn2 != null)
        {
            if (btn2.name == "red_btn")
            {
                img.color = Color.red;
                Debug.Log("Red");
            }
            else if (btn2.name == "blue_btn")
            {
                img.color = Color.blue;
                Debug.Log("Blue");
            }
            else if (btn2.name == "green_btn")
            {
                img.color = Color.green;
                Debug.Log("Green");
            }
        }
    }

    public bool AlignLineRenderer(LineRenderer rend)
    {
        Ray ray;
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool hitBtn = false;

        if (Physics.Raycast(ray, out hit, layerMask))
        {
            points[1] = transform.forward + new Vector3(0, 0, hit.distance);
            rend.startColor = Color.red;
            rend.endColor = Color.red;

            btn = hit.collider.gameObject.GetComponent<Button>();

            hitBtn = true;
        }
        else
        {
            points[1] = transform.forward + new Vector3(0, 0, 20);
            rend.startColor = Color.green;
            rend.endColor = Color.green;

            hitBtn = false;
        }

        rend.SetPositions(points);
        rend.material.color = rend.startColor;

        return hitBtn;
    }

    // Start is called before the first frame update
    void Start()
    {
        img = panel.GetComponent<Image>();

        //get the LineRenderer attached to the gameobject.
        rend = gameObject.GetComponent<LineRenderer>();
        
        //initialize the LineRenderer
        points = new Vector3[2];

        //set the start point of the linerenderer to the position of the gameObject.
        points[0] = Vector3.zero;

        //set the end point 20 units away from the GO on the Z axis (pointing forward)
        points[1] = transform.position + new Vector3(0, 0, 20);

        //finally set the positions array on the LineRenderer to our new values
        rend.SetPositions(points);
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        AlignLineRenderer(rend);

        //Debug.Log("AlignLineRenderer: " + AlignLineRenderer(rend));
        //Debug.Log("Submit: " + Input.GetAxis("Submit"));

        if (AlignLineRenderer(rend) && Input.GetAxis("Submit") > 0)
        {
            btn.onClick.Invoke();
        }
    }
}