using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    public GameObject arrowObject;
    public Transform arrowContainer;

    public float shotInterval = 2f; //간격.
    
    void Start()
    {
        StartCoroutine(FireArrow());
    }

    IEnumerator FireArrow()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++) //20 + 20i //20 40 60
            {
                GameObject tempObject = Instantiate(arrowObject, arrowContainer);
                Vector3 direction = new Vector2(Mathf.Cos((20+20*i)*Mathf.Deg2Rad), Mathf.Sin((20+20*i)*Mathf.Deg2Rad));

                tempObject.transform.right = direction;
                tempObject.transform.position = transform.position + shotInterval * direction; //간격을 더해줄 때, 나아가고자 하는 방향으로 간격을 띄운다.
            }

            yield return new WaitForSeconds(5f); //5초간 쉬고 발사.
        }
    }

}
