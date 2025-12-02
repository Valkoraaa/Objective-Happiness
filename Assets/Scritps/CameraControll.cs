using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControll : MonoBehaviour
{
    public float speed;
    public float detectionTreshold;
    public float borderRight;
    public float borderUp;
    public float limitZoomIn;
    public float limitZoomOut;
    private float scroll;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Input.mousePosition.x >= Screen.width - detectionTreshold && transform.position.x < borderRight)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
        else if (Input.mousePosition.x <= detectionTreshold && transform.position.x > -borderRight)
        {
            transform.position -= Vector3.right * Time.deltaTime * speed;
        }

        if (Input.mousePosition.y >= Screen.height - detectionTreshold && transform.position.z < borderUp)
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }
        else if (Input.mousePosition.y <= detectionTreshold && transform.position.z > -borderUp)
        {
            transform.position -= Vector3.forward * Time.deltaTime * speed;
        }

        if((scroll > 0 && transform.position.y < limitZoomIn) || (scroll < 0 && transform.position.y > limitZoomOut))
        {
            transform.position += new Vector3(0, Input.GetAxis("Mouse ScrollWheel") * speed, 0);
        }
    }
}
