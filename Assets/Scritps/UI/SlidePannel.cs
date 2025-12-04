using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlidePannel : MonoBehaviour
{
    public float distance = 400f;
    public float duration = 1f;

    private float elapsed = 0f;
    private Vector3 startPos;
    private bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(distance, 0, 0), t);

            if (t >= 1f)
                moving = false;
        }
    }

    public void Move(bool getIn)
    {
        if (getIn)
        {
            elapsed = 0f;
            startPos = transform.position;
            moving = true;
        }
    }
}
