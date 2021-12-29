using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour
{
    public CharacterController controller;
    public GameObject target;
    public float maxAcceleration;
    public float maxSpeed;
    public float targetRadius;
    public float slowRadius;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float timeToTarget = 0.1f;
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

    private SteeringOutput GetSteering()
    {
        SteeringOutput steeringOutput = new SteeringOutput();
        Vector3 moveDirection = Vector3.zero;

        Vector3 direction = target.transform.position - transform.position;
        float distance = direction.magnitude;

        if(distance < targetRadius)
        {
            steeringOutput.linearAcceleration = moveDirection;
            steeringOutput.angularAcceleration = 0.0f;
            return steeringOutput;
        }

        float currentSpeed = 0.0f;

        if(distance > slowRadius)
        {
            currentSpeed = maxSpeed;
        }
        else
        {
            currentSpeed = maxSpeed * distance / slowRadius;
        }

        Vector3 targetSpeed = direction.normalized;
        targetSpeed *= currentSpeed;

        steeringOutput.linearAcceleration = targetSpeed - controller.velocity;
        steeringOutput.linearAcceleration /= timeToTarget;

        if(steeringOutput.linearAcceleration.magnitude > maxAcceleration)
        {
            steeringOutput.linearAcceleration = steeringOutput.linearAcceleration.normalized * maxAcceleration;
        }

        if(steeringOutput.linearAcceleration.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(steeringOutput.linearAcceleration.x, steeringOutput.linearAcceleration.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            // steeringOutput.linearAcceleration = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            // steeringOutput.linearAcceleration *= maxAcceleration;
        }

        steeringOutput.angularAcceleration = 0.0f;

        return steeringOutput;
    }
}
