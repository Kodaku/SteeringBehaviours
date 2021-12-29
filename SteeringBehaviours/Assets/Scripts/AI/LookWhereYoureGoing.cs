using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWhereYoureGoing : MonoBehaviour
{
    public CharacterController controller;
    protected float turnSmoothTime = 0.1f;
    protected float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SteeringOutput steeringOutput = GetSteering();

        transform.rotation = Quaternion.Euler(0.0f, steeringOutput.angularAcceleration, 0.0f);
    }

    public virtual SteeringOutput GetSteering()
    {
        SteeringOutput steeringOutput = new SteeringOutput();
        Vector3 velocity = controller.velocity;
        if(velocity.magnitude <= 0.1f)
        {
            steeringOutput.angularAcceleration = 0.0f;
            return steeringOutput;
        }

        Vector3 normalizedDirection = velocity.normalized;

        if(normalizedDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(normalizedDirection.x, normalizedDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            steeringOutput.angularAcceleration = angle;
        }

        return steeringOutput;
    }
}
