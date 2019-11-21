using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left : MonoBehaviour
{
    [SerializeField]
    bool vr = false;
    Vector3 oldPosition;
    public float move_distance = 1.0f;

    void Start()
    {
        this.oldPosition = this.transform.position;
    }

    void FixedUpdate()
    {
        if (!this.vr)
        {
            float mouse_x_delta = Input.GetAxis("Mouse X") * 0.1f;
            float mouse_y_delta = Input.GetAxis("Mouse Y") * 0.1f;

            var pos = this.transform.localPosition;
            pos.x = Mathf.Clamp(pos.x + mouse_x_delta, -0.5f, 0.5f);
            pos.y = Mathf.Clamp(pos.y + mouse_y_delta, -0.5f, 0.5f);

            this.transform.localPosition = pos;
        }

        //加速度を求める
        this.MoveValue();
    }

    void MoveValue()
    {
        var dif = this.transform.position - this.oldPosition;
        this.move_distance = dif.magnitude;

        this.oldPosition = this.transform.position;
    }
}
