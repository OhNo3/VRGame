using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runHand : MonoBehaviour
{
    public enum handType
    {
        left,
        right
    }

    public handType hand;

    bool isVR = false;
    float moveDistance;
    Vector3 oldPosition;

    void Start()
    {
        this.oldPosition = this.transform.position;
    }

    void FixedUpdate()
    {
        //VRじゃなかった場合マウスカーソルの移動量で加速度取得用のキューブを回転させる
        if (!this.isVR)
        {
            float mouse_x_delta = Input.GetAxis("Mouse X") * 0.04f;
            float mouse_y_delta = Input.GetAxis("Mouse Y") * 0.04f;

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
        this.moveDistance = dif.magnitude * 10;

        this.oldPosition = this.transform.position;
    }

    public float GetMoveDistance()
    {
        return this.moveDistance;
    }

    public void SetIsVR(bool b)
    {
        this.isVR = b;
    }
}
