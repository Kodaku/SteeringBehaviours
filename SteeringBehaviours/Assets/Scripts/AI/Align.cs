using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour
{
    public GameObject target;
    public float targetRadius;
    private float timeToTarget = 0.1f;
    private float turnSmoothVelocity;
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

        steeringOutput.angularAcceleration = transform.eulerAngles.y;

        Vector3 direction = (target.transform.position - transform.position);
        Vector3 normalizedDirection = direction.normalized;

        if(direction.magnitude < targetRadius)
        {
            if(normalizedDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(normalizedDirection.x, normalizedDirection.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, timeToTarget);
                steeringOutput.angularAcceleration = angle;
            }
        }

        return steeringOutput;
    }
}
