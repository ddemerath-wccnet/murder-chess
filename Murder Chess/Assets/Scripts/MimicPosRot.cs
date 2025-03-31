using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicPosRot : MonoBehaviour
{
    public Transform target;
    public float pLerp = .01f;
    public float rLerp = .02f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rLerp);
    }
}
