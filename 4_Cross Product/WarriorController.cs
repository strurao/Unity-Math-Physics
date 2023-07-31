using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    private Rigidbody2D warriorRigidbody2D;

    public float jumpPower;
    public float speed;
    void Start(){
        warriorRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update(){
        PlayerMove(); 
        PlayerJump();
    }
    
    void PlayerMove(){
        float  x = Input.GetAxis("Horizontal"); //x축만 값을 받아서 사용.
        transform.Translate(Vector3.right * (x * speed * Time.deltaTime));
    }//transform.right 와 Vector3.right 는 엄연히 다릅니다 !!

    void PlayerJump(){
        if(Input.GetKeyDown(KeyCode.Space)) //스페이스키를 눌렀을 때.
            warriorRigidbody2D.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
        //힘을 월드좌표의 up 방향으로 jumpPower만큼 줄 것인데, Impulse(순간적인 힘)로 점프시킬 것입니다.
    }

    private void OnCollisionEnter2D(Collision2D _col){
        if (_col.gameObject.tag != "Ground") //이 스크립트의 주인인 warrior에게 충돌한 _col 이 "Ground" 태그가 아닐 때.
        {
            UpOrDown(_col);
        }
    }

    void UpOrDown(Collision2D _col) {//벽의 위쪽에? 아래쪽에? 부딪혔나?!
        Vector3 distVec = transform.position - _col.transform.position; //Warrior - Wall //Wall->Warrior 방향의 벡터.
        if (Vector3.Cross(_col.transform.right, distVec).z > 0) //_col.transform.right : 충돌체의 오른쪽 방향 벡터.
        { //오른손 법칙에서 반시계 방향이라면 - 엄지의 방향이 화면에서 밖으로 나옴 - 외적 결과가 양수라는 뜻~! - 벽의 위
          //오른손 법칙에서 시계 방향이라면 - 엄지의 방향이 밖에서 화면으로 들어감 - 외적 결과가 음수라는 뜻~! - 벽의 아래
            Debug.Log("Up");
            return;
        }
        Debug.Log("Down");
    }
}
