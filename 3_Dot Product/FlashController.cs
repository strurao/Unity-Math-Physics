using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class FlashController : MonoBehaviour
{
    public GameObject[] ghostObjectArray; //에디터에서 넣어줌...

    public float moveSpeed = 3f;
    public float rangeAngle = 25f;
    public float rangeDistance = 4f;
    void Update()
    {
        PlayerMove();
        CheckGhost();
    }

    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");  // X축.
        float y = Input.GetAxis("Vertical");    // Y축.
        // GetAxis 조이스틱 컨트롤러를 만들 수 있다.
        
        transform.Translate(new Vector3(x,y) * (moveSpeed * Time.deltaTime));
    }

    void CheckGhost() //x1y1 + x2y2 --> Vector3.Dot --> 유니티의 내적 함수 !!!
    { 
        int i = 0; // 유령의 수 초기값 : 0 !!
        foreach (var ghost in ghostObjectArray) // 각 오브젝트를 전부 체크.
        {   // distanceVec 의 시작점 : 나(Flash Light)의 현재 position // 끝점 : Ghost 의 현재 position
            Vector3 distanceVec = ghost.transform.position - transform.position; // (유령위치) - (나의 현재위치)
            if (distanceVec.magnitude < rangeDistance) //magnitude은 거리 값 (벡터의 크기).
            {
                Vector3 dirVec = distanceVec.normalized; //방향벡터여야 합니다. 내적을 하기 위해서 !! 
                
                if(Vector3.Dot(transform.up, dirVec) > Mathf.Cos(rangeAngle*Mathf.Deg2Rad)) //공식 실행 !!!
                    i++;
            } //참고로, transform.right/up Vector2.right,left,up,down 전부 방향벡터 (크기가 1).
        }
        
        Debug.Log("감지된 유령의 수: "+i);
    }
}
