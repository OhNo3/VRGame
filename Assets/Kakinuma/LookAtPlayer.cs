using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject Target; // 注視するオブジェ

    private static float Range = 3.0f;      //追尾判定距離

    private static float Leave = 2.5f;      //近接限界距離

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの方向を向く
        this.transform.LookAt(Target.transform);

        Vector3 TargetPosition = Target.transform.position;

        //プレイヤーと注視するオブジェクトとの距離
        float Length = (TargetPosition.x - this.transform.position.x) * (TargetPosition.x - this.transform.position.x)
                        + (TargetPosition.z - this.transform.position.z) * (TargetPosition.z - this.transform.position.z);

        //プレイヤーを一定距離で追尾
        if(Range * Range < Length)
        {
            this.transform.position = this.transform.position + this.transform.forward * Time.deltaTime;
        }

        //プレイヤーから一定距離で離れる
        if (Length < Leave * Leave)
        {
            this.transform.position = this.transform.position - this.transform.forward * Time.deltaTime;
        }

    }

}
