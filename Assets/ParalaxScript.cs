using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParalaxScript : MonoBehaviour
{
    [Header("Screen Point")]
    public Camera camera;
    public Vector3 screenPoint;

    [Header("Positions")]
    public Vector3 startPos;
    public Vector3 newPos;
    public float maxParalax;

    void Start() {
        startPos = gameObject.transform.position;
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update() {
        screenPoint = camera.WorldToScreenPoint(gameObject.transform.position);
        screenPoint = new Vector3(((screenPoint.x / Screen.width) - 0.5f) * 2,
            ((screenPoint.y / Screen.height) - 0.5f) * 2, 0f);
        Debug.Log(screenPoint);

        newPos = new Vector3(startPos.x + (maxParalax * screenPoint.x),
            startPos.y + (maxParalax * screenPoint.y),
            startPos.z);
        gameObject.transform.position = newPos;
    }
}
