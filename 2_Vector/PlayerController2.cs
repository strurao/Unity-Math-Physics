using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController2 : MonoBehaviour
{
    public GameObject bulletObject;
    public Transform bulletContainer;
    public GameObject guideLine;
    
    public float ditectionRange = 4f;

    private Camera mainCamera;

    //마우스의 위치에 따라서 가이드라인이 생기고, 미사일이 발사.
    //카메라 위에 마우스의 좌표가 어떻게 나오는지 알기 위해서 카메라 값을 받아온다.

    void Start()
    {
        mainCamera = Camera.main;
        //현재 사용하고 있는 카메라 객체가 들어옴.
    }

    void Update()
    {
        MouseCheck();
        
        if (Input.GetMouseButtonDown(0)) //마우스의 왼쪽버튼을 누ㄹ면,
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = mainCamera.ScreenToWorldPoint(mousePos);

            Vector3 playerPos = transform.position; 

            Vector2 dirVec = mousePos - (Vector2)playerPos; //그냥 벡터를 만듦.
            dirVec = dirVec.normalized; //nomarlized 를 꼭 해줘야 방향벡터!!

            GameObject tempObject = Instantiate(bulletObject, bulletContainer); //총알 생성.
            tempObject.transform.right = dirVec;
            //총알의 right 방향을 dirVec으로 설정.

            tempObject.transform.position = (Vector2)playerPos + dirVec * 0.5f;
            //총알이 플레이어 자신한테 발사하면 조금 어색할 수 있어서.
            //총알이 플레이어 보다 살짝 앞에서 발사.

            transform.Translate(-dirVec);
            //플레이어가 반대 방향으로 이동한다.
            //총알 발사 시 반동하는 듯한 구현.
        }
    }

    void MouseCheck() 
    {
        Vector2 mousePos = Input.mousePosition;
        // 마우스의 위치값을 받음.

        mousePos = mainCamera.ScreenToWorldPoint(mousePos);
        // 현재 화면 좌표에서의 마우스의 위치를 게임내의 월드 좌표 . Position 값으로 변환.
        // 마우스의 위치 mousePos 가 가이드라인 벡터의 끝점

        Vector3 playerPos = transform.position;
        // 플레이어의 위치 playerPos 가 레이저 벡터의 시작점
        
        Vector2 distanceVec = mousePos - (Vector2)playerPos; //가이드라인 벡터 !!

        guideLine.SetActive(distanceVec.magnitude < ditectionRange ? true : false); //삼항연산자.
        // 가이드라인 활성화 할거임?
        // 일정 "거리" 안에 들어가면 활성화하겠다.
        // magnitude 를 이용하면 거리를 바로 알 수 있다.
        // magnitude 뜻 : 크기.
        // sqrMagnitude : 거리의 제곱을 알 수 있다.

        guideLine.transform.right = distanceVec.normalized;
        // 가이드라인의 right 방향을 distanceVec의 방향벡터로 설정하겠다.
        // 방향벡터를 설정하는 것은 벡터.normalized;
        // distanceVec.normalized == distanceVec / distanceVec.magnitude;

        //방향에 관련된 것은 전부 방향벡터로 설정하면 돼!!
    }
}
