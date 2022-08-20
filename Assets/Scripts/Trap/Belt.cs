using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Belt : MonoBehaviour
{
    public Transform Endpoint;//获取终点位置
    private float  DirectionX;
    private float  DirectionZ;
    public float parameter;
    private void Start()
    {
        DirectionX = Endpoint.position .x  - gameObject.transform.position .x ;
        DirectionZ = Endpoint.position.z - gameObject.transform.position.z;
    }
    private void Update()
    {
        
    }

 
    

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(DirectionX, 0, DirectionZ, ForceMode.Acceleration);
    }
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(-DirectionX, 0, -DirectionZ, ForceMode.Acceleration);
    }
}
