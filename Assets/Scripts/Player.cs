using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //移動速度の調整とかはエディタから触ってね

    [SerializeField]
    right rightHand;
    [SerializeField]
    left leftHand;
    [SerializeField]
    float cameraAcc = 1.0f;
    [SerializeField]
    float moveSpeed = 1.0f;
    [SerializeField]
    float jumpValue = 1.0f;

    //VRの時、エディタからチェックいれてくれい
    [SerializeField]
    bool isVR = false;

    Rigidbody rigidbody;
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    Transform forwardObject;

    [SerializeField]
    DownCamera DownCameraScript;

    float jumpTIme = 10;
    bool isJump = false;
    public bool rTrig;
    public bool lTrig;

    enum PlayerStatus
    {
        Idle,
        Move,
        Jump,
        Fall,
    };

    PlayerStatus playerState = PlayerStatus.Idle;

    private void Awake()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        this.StateChange();
        this.Rot();
        this.Move();
        this.Jump();

    }


    void StateChange()
    {
        this.playerState = PlayerStatus.Idle;

        if (this.rigidbody.velocity.y > 0.1f) { this.playerState = PlayerStatus.Jump; }
        else if (this.rigidbody.velocity.y < -0.1f) { this.playerState = PlayerStatus.Fall; }
    }

    void Jump()
    {
        if (this.isJump) { this.jumpTIme += Time.fixedDeltaTime; return; }

        if (!Input.GetKeyDown(KeyCode.Space)) { return; }
        this.rigidbody.AddForce(new Vector3(0, 100 * this.jumpValue, 0));
        this.isJump = true;
        this.jumpTIme = 0;
    }

    void Move()
    {
        var mov = Mathf.Abs(this.leftHand.move_distance) + Mathf.Abs(this.rightHand.move_distance);

        if (this.playerState == PlayerStatus.Idle || this.playerState == PlayerStatus.Move)
        {
            var forward = this.forwardObject.forward;
            forward.y = 0;
            this.transform.position += forward * this.moveSpeed * Time.fixedDeltaTime * mov * 2;
        }
        else
        {
            var forward = this.forwardObject.forward;
            forward.y = 0;
            this.transform.position += forward * this.moveSpeed * Time.fixedDeltaTime * mov * 1;
        }
    }

    void Rot()
    {
        if (this.isVR) { return; }

        float X_Move = 0;
        float Y_Move = 0;
        if (Input.GetKey(KeyCode.A))
        {
            X_Move -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            X_Move += 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            Y_Move += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Y_Move -= 1;
        }

        var rot = this.cameraTransform.eulerAngles;
        rot.x += -Y_Move * Time.fixedDeltaTime * this.cameraAcc * 10;
        rot.y += X_Move * Time.fixedDeltaTime * this.cameraAcc * 10;
        this.cameraTransform.eulerAngles = rot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        DownCameraScript.moveFlag = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        //壁とかに当たったらジャンプできるようにする
        if (this.jumpTIme > 0.1f)
        {
            this.isJump = false;
        }
    }
}
