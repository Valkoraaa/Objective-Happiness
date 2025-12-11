using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlidePannel : MonoBehaviour
{
    public float distance = 400f;
    public float duration = 0.3f;

    private float elapsed = 0f;
    private Vector3 startPos;
    private bool moving = false;
    private bool hasBeenMoved = false;
    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        distance = -rt.rect.width;
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

    public void Move(bool getIn) //animation to get in or out of the screen
    {
        if (getIn && !hasBeenMoved && !moving)
        {
            distance = -distance;
            elapsed = 0f;
            startPos = transform.position;
            moving = true;
            hasBeenMoved = true;
        }
        else if (!moving)
        {
            distance = -distance;
            elapsed = 0f;
            startPos = transform.position;
            moving = true;
            hasBeenMoved = false;
        }
    }
}
