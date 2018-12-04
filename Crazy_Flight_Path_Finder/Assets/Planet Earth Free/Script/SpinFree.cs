using UnityEngine;
using System.Collections;

/// <summary>
/// Spin the object at a specified speed
/// </summary>
public class SpinFree : MonoBehaviour
{
    [Tooltip("Spin: Yes or No")]
    public bool spin;
    [Tooltip("Spin the parent object instead of the object this script is attached to")]
    public bool spinParent;
    public float speed = 10f;

    public float _sensitivity =100;


    [HideInInspector]
    public bool clockwise = true;
    [HideInInspector]
    public float direction = 1f;
    [HideInInspector]
    public float directionChangeSpeed = 2f;

    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            if (direction < 1f)
                direction += Time.deltaTime / (directionChangeSpeed / 2);
            if (spin)
                if (clockwise)
                {
                    if (spinParent)
                        transform.parent.transform.Rotate(Vector3.up, (speed * direction) * Time.deltaTime);
                    else
                        transform.Rotate(Vector3.up, (speed * direction) * Time.deltaTime);
                }
                else
                {
                    if (spinParent)
                        transform.parent.transform.Rotate(-Vector3.up, (speed * direction) * Time.deltaTime);
                    else
                        transform.Rotate(-Vector3.up, (speed * direction) * Time.deltaTime);
                }
        }
        if(Input.GetKey(KeyCode.R)) // 탭키 위에있는걸로 바꾸기 
            transform.rotation = Quaternion.identity;

    }
    void OnMouseDrag()
    {
        float temp_x_axis = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        float temp_y_axis = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
        transform.Rotate(-temp_y_axis, -temp_x_axis, temp_y_axis, Space.World);
    }
}