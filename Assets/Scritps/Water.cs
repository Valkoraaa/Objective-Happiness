using UnityEngine;

public class Water : MonoBehaviour
{
    private Renderer rend;
    public float speed;
    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        rend.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0f);
    }
}
