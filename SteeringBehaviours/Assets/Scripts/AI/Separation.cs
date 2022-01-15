using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : MonoBehaviour
{
    public CharacterController controller;
    public GameObject[] targets;
    public float maxAcceleration;
    public float threshold;
    public float decayCoefficient;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SteeringOutput steeringOutput = GetSteering();

        controller.Move(-steeringOutput.linearAcceleration * Time.deltaTime);
    }

    private SteeringOutput GetSteering()
    {
        SteeringOutput steeringOutput = new SteeringOutput();

        foreach(GameObject target in targets)
        {
            Vector3 direction = target.transform.position - transform.position;
            float distance = direction.magnitude;

            if(distance < threshold)
            {
                float strength = Mathf.Min(decayCoefficient / (distance * distance), maxAcceleration);
                direction = direction.normalized;
                steeringOutput.linearAcceleration += strength * direction;
            }
        }

        if(controller.velocity.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(controller.velocity.x, controller.velocity.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            // steeringOutput.linearAcceleration = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            // steeringOutput.linearAcceleration *= maxAcceleration;
        }

        return steeringOutput;
    }
}
