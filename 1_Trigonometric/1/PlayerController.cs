using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 4가지의 실습.
// 1. Player 원하는 방향으로 이동.
// 2. Player 이동 패턴을 원을 그리며 이동. (싸이클로드 곡선, 꽃모양 패턴)
// 3. 미사일 발사(순차적).
// 4. 미사일 발사(한번에) (특정 범위).

public enum Pattern { One, Two };
public class PlayerController : MonoBehaviour
{
    public GameObject bulletObject;
    public Transform bulletContainer;

    public Pattern shotPattern; // 패턴을 선택할 수 있게 되어 있음.
    public float moveSpeed = 2f;
    public float circleScale = 5f; //원의 크기를 결정. 
    public int angleInterval = 10;
    public int startAngle = 30;
    public int endAngle = 330;

    private int iteration = 0;
    
    private void Start()
    {
        if (shotPattern == Pattern.One)
            StartCoroutine(MakeBullet());
        else if (shotPattern == Pattern.Two)
            StartCoroutine(MakeBullet2());
    }

    private void Update()
    {
        //PlayerMove(30); //30도 (Deg) 방향으로 움직인다.
        //PlayerCircle();
    }

    void PlayerMove(float _angle)
    {
        if (Input.GetKey(KeyCode.Space))//space 키를 누르고 있으면 true.
        {
            //direction 에 우리가 가고자 하는 방향을 설정.
            Vector2 direction = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad));
            //Mathf.cos, Mathf.sin : Rad 0~2pi. , Deg 0~360.
            //우리의 _angled은 라디안값 Rad 입니다!!
            //Deg값을 입력받으면 Rad 값으로 변경해야한 .
            transform.Translate(moveSpeed * direction * Time.deltaTime); //moveSpeed 만큼 direction 방향으로 이동을 시킴.
            // Time.deltatime : 프레임 간의 시간 차이를 곱해줘야, 컴퓨터의 성능과 무관하게 동일한 속도로 이동이 가능.
        }
    }

    void PlayerCircle()
    {
        // iteration : 0 부터 360 까지 1 씩 증가.
        // Deg를 입력받아 Rad 값으로 바꾸기 위해 Deg2Rad 을 사용 !

        Vector2 direction = new Vector2(Mathf.Cos(iteration * Mathf.Deg2Rad), Mathf.Sin(iteration * Mathf.Deg2Rad));
        transform.Translate(direction * (circleScale * Time.deltaTime));
        iteration++; //iteration 뜻 : 반복.
        if (iteration > 360) iteration -= 360; // 계속 0~360 사이에 있도록 !!
    }

    private IEnumerator MakeBullet()
    {
        int fireAngle = 0; // 초기값은 0도

        while (true)
        {
            // bulletContainer 안에 bulletObject 를 생성하겠다.
            GameObject tempObject = Instantiate(bulletObject, bulletContainer, true);

            Vector2 direction = new Vector2(Mathf.Cos(fireAngle*Mathf.Deg2Rad),Mathf.Sin(fireAngle*Mathf.Deg2Rad));
            tempObject.transform.right = direction; //총알 오브젝트의 오른쪽을 direction 방향으로 설정.
            tempObject.transform.position = transform.position; //총알 오브젝트의 위치는 플레이어의 위치로 설정.

            // 0.1초간 기다리고.
            yield return new WaitForSeconds(0.1f);

            // 발사한 각도를 설정한 값(angleInterval)에 따라서 증가.
            fireAngle += angleInterval ;
            if (fireAngle > 360) fireAngle -= 360;//0~360
        }
    }
    
    private IEnumerator MakeBullet2()
    {
        while (true)
        {
            //한번에 미사일을 만들어줌.
            for (int fireAngle = startAngle; fireAngle < endAngle; fireAngle += angleInterval)
            {
                GameObject tempObject = Instantiate(bulletObject, bulletContainer, true);
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle*Mathf.Deg2Rad),Mathf.Sin(fireAngle*Mathf.Deg2Rad));
               
                tempObject.transform.right = direction;
                tempObject.transform.position = transform.position;
            }

            yield return new WaitForSeconds(4f); //4초간 대기..
        }
    }
}
