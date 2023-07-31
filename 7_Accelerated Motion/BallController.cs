using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float shotVelocity;
    public float shotAngle;

    private Rigidbody2D ballRB2D;
    private bool isGround = true; //공이 땅에 붙어있음.
    private bool isCenter = false;
    private float totalTime = 0f;

    void Start()
    {
        ballRB2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShotBall());
        }
    }
    IEnumerator ShotBall() //공 발사.
    {
        Debug.Log("=== Simulation ===");

        isGround = false;
        //땅에 공이 있지 않음.
        transform.right = new Vector2(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
        //공의 오른쪽 방향을 이러한 각도로 설정하겠다.
        ballRB2D.velocity = transform.right * shotVelocity;
        //설정된 공의 각도로 shotVelocity 속도로 발사를 하겠다.
        totalTime = 0f;
        //처음 시간은 0으로 초기화.
        while (true)
        {
            yield return null;
            if (isGround) break; //착지하면 while 문이 끝남.
            totalTime += Time.deltaTime; //착지를 하기 전까지는 시간이 계속 쌓이는 중.
            if (Mathf.Abs(ballRB2D.velocity.y) < 0.1f && !isCenter)
                //y축의 속도의 절대값이 0.1보다 작을 때 && isCenter 가 false 일 때 (딱 한 번만 발동하기 위해 센터확인).
            {
                isCenter = true;
                Debug.Log("CenterHeight: " + transform.position.y); //최고 높이.
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D _col)
    {
        if(isGround == false) 
        {
            isGround = true; //착지.
            ballRB2D.velocity = Vector2.zero; //속도는 0 (더 이상 움직이지 않게 하기 위해).
            Debug.Log("Totaltime: " + totalTime); //총 걸린 시간.
            Debug.Log("TotalMeter: " + (transform.position.x + 8)); //초기 위치가 -8에서 시작을 했기 때문에 +8로 보정.

            Verification();
        }
    }

    private void Verification() //등가속도 공식..
    {
        Debug.Log("=== Verification ===");

        float totalTime = 2 * shotVelocity * Mathf.Sin(shotAngle * Mathf.Deg2Rad) / 9.81f;
        // 총 걸린 시간은 2t 이다.
        // 2*V*sin(theta)/g ;
        float centerHeight = Mathf.Pow(shotVelocity * Mathf.Sin(shotAngle * Mathf.Deg2Rad), 2) / (2*9.81f);
        // 최고 높이.
        // (V*sin(theta))^2 / 2g
        // ^2는 제곱...Mathf.Pow; 몇제곱을 할지는 인자에 넣어주면 돼.
        float totalMeter = Mathf.Pow(shotVelocity,2) / 9.81f * Mathf.Sin(2 * shotAngle * Mathf.Deg2Rad);
        // 총 날라간 거리.
        // v^2/g*sin(2*theta)

        Debug.Log("Totaltime: " + totalTime);
        Debug.Log("CenterHeight: " + centerHeight);
        Debug.Log("TotalMeter: " + totalMeter);
    }
}
