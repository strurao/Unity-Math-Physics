using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    private Matrix4x4 worldMat;
    void Start()
    {
        MakeWorldMatrix();
        ExtractionMatrix();
    }

    void MakeWorldMatrix(){
        Vector3 tran = new Vector3(2, 1, 5);
        Quaternion rot = Quaternion.Euler(45, 0, 45);
        //오일러 각 - 직관적으로 각도를 이용한 것.
        //짐벌락 이라는 현상은 각 회전에 대해 서로가 종속적이여서 생기는 문제..
        //짐벌락을 피하기 위해 쿼터니언(사원수)을 사용.
        //오일러 각을 쿼터니언으로 변환해서 사용한다는 코드 였다.
        //회전에 관한 것은 전부 쿼터니언을 걸쳐서 연산을 해야함.

        Vector3 scal = new Vector3(3, 3, 3);

        //Unity에서 TRS 계산 알아서 해줌.
        worldMat = Matrix4x4.TRS(tran, rot, scal);

        //이동행렬, 회전행렬, 크기변환행렬을 순서에 맞게 곱해줘0 직접 만듬.
        //worldMat = Matrix4x4.Translate(new Vector3(2, 1, 0)) * Matrix4x4.Rotate(rot) * Matrix4x4.Scale(new Vector3(3,3,3));

        Debug.Log("=== Made Matrix ===");
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(worldMat.GetRow(i));
        }
    }

    private void ExtractionMatrix(){
        Matrix4x4 matrix = transform.localToWorldMatrix; //TRS 결과값 == localToWorldMatrix.
        Debug.Log("=== Extracted Matrix ===");
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(matrix.GetRow(i));
        }

        //Column 열 , Row 행
        //추출이 가능해요.
        Vector3 position = matrix.GetColumn(3);
        Debug.Log("=== Position ===");
        Debug.Log(position);

        Quaternion rotation = Quaternion.LookRotation(
            matrix.GetColumn(2),
            matrix.GetColumn(1)
        );

        Debug.Log("=== Rotation ===");
        Debug.Log(rotation.eulerAngles);

        Debug.Log("=== Scale ===");
        Vector3 scale = new Vector3(
            matrix.GetColumn(0).magnitude,
            matrix.GetColumn(1).magnitude,
            matrix.GetColumn(2).magnitude
        );
        Debug.Log(scale);
    }
}
