using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundController : MonoBehaviour
{
    public GameObject target;
    public Vector3 targetPosition;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTarget()
    {
        //Instantiate(target, targetPosition, new Quaternion(0,0,0,0), gameObject.transform);
        target.transform.position = targetPosition;
    }
}
