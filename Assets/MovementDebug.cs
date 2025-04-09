using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDebug : MonoBehaviour
{
    public float speed;
    public Vector3 moveBy;

    // Update is called once per frame
    void Update()
    {
        moveBy = new Vector3(0.0f, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.D)) {
            moveBy = new Vector3(moveBy.x + speed * Time.deltaTime, moveBy.y, 0.0f);
        }
        if (Input.GetKey(KeyCode.A)) {
            moveBy = new Vector3(moveBy.x - speed * Time.deltaTime, moveBy.y, 0.0f);
        }
        if (Input.GetKey(KeyCode.W)) {
            moveBy = new Vector3(moveBy.x, moveBy.y + speed * Time.deltaTime, 0.0f);
        }
        if (Input.GetKey(KeyCode.S)) {
            moveBy = new Vector3(moveBy.x, moveBy.y - speed * Time.deltaTime, 0.0f);
        }

        gameObject.transform.position += moveBy;
    }
}
