using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private Rigidbody2D ballRB2D;
    private float moveVelocity = 5f;
    void Start()
    {
        ballRB2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) //스페이스 누르면.
            ballRB2D.velocity = transform.right * moveVelocity; //오른쪽으로 moveVelocity 속도 만큼 이동.
    }
}
// 반발계수는 마찰력Friction 과 달리 반발계수가 큰 값을 따라간다.
// case 1 : e=0     : 완전비탄성.
// case 2 : e=1     : 완전탄성.
// case 3 : e=0.5   : 비탄성.