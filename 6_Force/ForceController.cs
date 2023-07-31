using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour
{
    private Rigidbody boxRigidbody;
    private float movePower = 5f;
    void Start()
    {
        boxRigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Debug.Log(boxRigidbody.velocity);

        if (Input.GetKeyDown(KeyCode.A))
            boxRigidbody.AddForce(transform.right * movePower, ForceMode.Impulse);
            // Impulse : 게임에서 순간적인 힘을 가할 때.

        else if (Input.GetKey(KeyCode.S))
            boxRigidbody.AddForce(transform.right * movePower, ForceMode.Force);
            // Force : 게임에서 지속적으로 힘을 가할 때.

        else if (Input.GetKeyDown(KeyCode.D))
            boxRigidbody.AddForce(transform.right * movePower, ForceMode.VelocityChange);
        // VelocityChange : 게임에서 순간적인 힘을 가할 때. 

        else if (Input.GetKey(KeyCode.F))
            boxRigidbody.AddForce(transform.right * movePower, ForceMode.Acceleration);
        // Acceleration : 게임에서 지속적으로 힘을 가할 때.


        // Impulse 와 Force 는 전부 질량에 영향을 받습니다.  F=ma.
        // VelocityChange 와 Acceleration 는 전부 질량과 무관하게 힘을 가합니다.
            // 예: 질량이 달라도 중력은 똑같이 받는 것처럼.
            // 따라서 가속도가 똑같이 가해진다.
        // A와 D의 차이 : A는 질량이 가벼울수록 빨리 간다. D는 동일한 속도로 간다.

    }
}
