using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public CharacterController controller;
    public float avoidDistance;
    public float lookahead;
    public float maxAcceleration;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;
    private Vector3 target;
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

    public SteeringOutput GetSteering()
    {
        SteeringOutput steeringOutput = new SteeringOutput();
        Vector3 ray = controller.velocity;
        ray = ray.normalized;
        ray *= lookahead;

        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward, Color.red, 2.0f);

        if(!Physics.Raycast(transform.position, transform.forward, out hit, ray.magnitude))
        {
            return steeringOutput;
        }

        target = hit.transform.position + hit.normal * avoidDistance;
        print("Avoiding: " + target);

        steeringOutput.linearAcceleration = (target - transform.position).normalized;

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
