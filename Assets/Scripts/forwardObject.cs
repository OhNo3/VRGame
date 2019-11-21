using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardObject : MonoBehaviour
{
    [SerializeField]
    Transform vrCamera;

    void Update()
    {
        var rot = this.transform.localEulerAngles;
        rot.y = vrCamera.localEulerAngles.y;
        this.transform.localEulerAngles = rot;
    }
}
