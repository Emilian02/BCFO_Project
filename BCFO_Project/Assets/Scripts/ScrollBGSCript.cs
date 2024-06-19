using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ScrollBGSCript : MonoBehaviour
{
    public float scrollSpeed = 0;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (Time.time * scrollSpeed, 0.0f);
    }
}
