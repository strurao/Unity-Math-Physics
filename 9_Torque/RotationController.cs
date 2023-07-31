using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    private Rigidbody2D rotationRB2D;
    private float movePower = 5f;
    void Start()
    {
        rotationRB2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rotationRB2D.AddForce(transform.right * movePower, ForceMode2D.Impulse);
            // AddForce, VelocityChange , Accelaretion 는 질량에 영향을 받아서 동일하게 힘을 가함.
            // 파랑이들과 하양이들의 속도가 다름을 확인할 수 있다.
            // 직선 방향으로 힘을 줄 때 AddForce 를 씀.
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rotationRB2D.AddTorque(movePower, ForceMode2D.Impulse);
            // 원래는 관성 모멘트에 따라 물체 4개의 회전속도가 모두 달라야하는데 다들 똑~같이 돈다.
            // 토큰은 그냥 동일값.. 모두 도는 속도가 똑같음..
            // 회전에 대해서 힘을 가할 때는 AddTorque 를 씀.
            // Impulse, Force 를 ForceMode2D 에서 설정할 수 있다.
        }
    }
}
