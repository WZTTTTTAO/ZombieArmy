using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public GameObject belt;
    public Transform endpoint;//获取终点位置
    public int currentSpeed;//当前速度
    public int maxSpeed;//传送带最大速度
    public bool belton = false;//判断传送带是否开启

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PowerSwitch();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSpeed();
        }
    }
    void ChangeSpeed()
    {
        if (currentSpeed >= maxSpeed)
        {
            currentSpeed = 0;
        }
        else
        {
            currentSpeed++;
        }
    }
    void PowerSwitch()
    {
        if (belton)
        {
            belton = false;
        }
        else
        {
            belton = true;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
       
        
            collider.transform.position = Vector3.MoveTowards(collider.transform.position, endpoint.position, currentSpeed * Time.deltaTime);
        
    }

}
