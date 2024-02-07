using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   GameObject Target; Vector3 TargetPosition;
    public float CameraFollowSpeed,PosY,NegLimitX,PosLimitX,NegLimitY,PosLimitY;

    void CameraPositionActualization()
    {   TargetPosition=new Vector3(Target.transform.position.x, Target.transform.position.y+PosY,transform.position.z);
        transform.position=Vector3.Lerp(transform.position,TargetPosition,Time.deltaTime*CameraFollowSpeed);
        if(transform.position.y>PosLimitY){transform.position=new Vector3(transform.position.x,PosLimitY,transform.position.z);}
        if (transform.position.x<=NegLimitX){transform.position=new Vector3(NegLimitX,transform.position.y,transform.position.z);}
        if (transform.position.x>=PosLimitX){transform.position=new Vector3(PosLimitX,transform.position.y,transform.position.z);}
    }

    void Start()
    {Target = GameObject.Find("Player");}

    void Update()
    {CameraPositionActualization();}
}