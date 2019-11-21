using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownCamera : MonoBehaviour
{
    [SerializeField]
    public bool moveFlag = false;       //trueでしゃがみ開始

    private float corePositionY;
    private float PosY;

    private float countFlame;

    // Start is called before the first frame update
    void Start()
    {
        moveFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moveFlag)
        {
            MoveCamera();
        }
    }

    public void MoveCamera()
    {
        if(countFlame == 0)
        {
            corePositionY = this.transform.localPosition.y;
        }

        float DownRate = 0.80f / countFlame;

        if(DownRate < 0.1f)
        {
            DownRate = -0.15f;
        }

        PosY = corePositionY - DownRate;

        if(corePositionY < PosY)
        {
            moveFlag = false;
            countFlame = 0;
            PosY = corePositionY;
        }

        this.transform.localPosition = new Vector3(this.transform.localPosition.x, PosY, this.transform.localPosition.z);

        countFlame++;

    }
}
