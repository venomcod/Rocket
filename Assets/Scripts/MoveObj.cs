using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class MoveObj : MonoBehaviour
{
    [SerializeField] Vector3 movePos;
    [SerializeField] float moveSpd;
    [SerializeField] [Range(0,1)] float movePrg; // ���� 0 �� �� �������� - ���� 1 �� �������� ���������
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movePrg = Mathf.PingPong(Time.time*moveSpd, 1);

        Vector3 offset = movePos * movePrg; // movePrg 0.5 - 50% offset = movePos * 0,5 
        transform.position = startPos + offset;

    }
}
