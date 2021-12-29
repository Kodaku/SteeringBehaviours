using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public CharacterController controller;
    public GameObject target;
    public float maxAcceleration;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SteeringOutput steeringOutput = GetSteering();

        controller.Move(steeringOutput.linearAcceleration * Time.deltaTime);
    }

    public virtual SteeringOutput GetSteering()
    {
        SteeringOutput steeringOutput = new SteeringOutput();

        steeringOutput.linearAcceleration = (target.transform.position - transform.position).normalized;

        if(steeringOutput.linearAcceleration.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(steeringOutput.linearAcceleration.x, steeringOutput.linearAcceleration.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            steeringOutput.linearAcceleration = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            steeringOutput.linearAcceleration *= maxAcceleration;
        }

        steeringOutput.angularAcceleration = 0.0f;

        return steeringOutput;
    }
}
