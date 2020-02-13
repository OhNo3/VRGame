using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalAreaCollider : MonoBehaviour
{
    //ゴール時に出てくるもの
    [SerializeField] private  GameObject GoalNameObj;
    //
    [SerializeField] private float Timer;
    private float counter;
    private bool GoalFlag;


    //プレイヤー判定
    void Awake()
    {
        //ゴール時に出てくるものを非表示
        GoalNameObj.SetActive(false);
        counter = 0;
        GoalFlag = false;
    }


    void Update()
    {
        if(GoalFlag)
        {
            counter++;
        }

        if(GoalFlag && counter <= Timer)
        {
            //シーンの再読み込み
            Scene loadScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadScene.name);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (GoalFlag == true) return;

        if(collision.gameObject.tag == "Player")
        {//プレイヤーが衝突
            GoalNameObj.SetActive(true);
            GoalFlag = true;
        }
    }
}
