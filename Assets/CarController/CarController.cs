using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;
    [SerializeField] private Transform frontLeftTransform, frontRightTransform, rearLeftTransform, rearRightTransform;
    public Transform steeringWheel;
    
    [SerializeField] private float maxMotorTorque;   // Increased torque for off-road
    [SerializeField] private float maxSteeringAngle;  // Slightly increased steering angle
    [SerializeField] private float brakeTorque;     // Increased brake torque
    [SerializeField] private float maxSpeed;         // Max speed in km/h
    [SerializeField] private float brakeWhenNoAcc;  //brake troque multipler when the car is not acclerating
    public bool IsDriving;
    public float steeringWheelTurnAngle = 180f;
    
    private Rigidbody rb;
    [SerializeField] private GameObject checkP;
    public Transform carSeat;
    public Transform playerOutsideOffset;
    public Transform target;
    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
       // rb.mass = 3000f;
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // Lower the center of mass for better stability
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Enable interpolation

        ConfigureWheelCollider(frontLeftWheel);
        ConfigureWheelCollider(frontRightWheel);
        ConfigureWheelCollider(rearLeftWheel);
        ConfigureWheelCollider(rearRightWheel);
        IsDriving = false;
    }

    private void FixedUpdate()
    {
        if (IsDriving)
        {
            driving();
        }
        else
        {
            if(rb.velocity.magnitude > 1f)
            {
                Debug.Log(rb.velocity.magnitude);
                frontLeftWheel.brakeTorque = 14000f;
                frontRightWheel.brakeTorque = 14000;
                rearLeftWheel.brakeTorque = 14000f;
                rearRightWheel.brakeTorque = 14000f;
            }
        }
    }

    private void driving()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        float brake = Input.GetKey(KeyCode.Space) ? brakeTorque : 0;

        // Calculate the car's speed in km/h
        float currentSpeed = rb.velocity.magnitude * 3.6f;

        if (currentSpeed > maxSpeed)
        {
            motor = 0; // Prevent exceeding max speed
            brake = brakeTorque; // Apply a small braking force when exceeding max speed
        }

        // Set the steering angles for the front wheels
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;

        // Apply motor torque to the front wheels
        frontLeftWheel.motorTorque = motor;
        frontRightWheel.motorTorque = motor;


        // Apply braking force if there's no throttle input or if the car exceeds the speed limit
        if (Input.GetAxis("Vertical") == 0 || currentSpeed > maxSpeed)
        {
            brake = Mathf.Max(brake, brakeTorque * brakeWhenNoAcc); // Apply a small brake force
        }


        // Set brake torque for all wheels
        frontLeftWheel.brakeTorque = brake;
        frontRightWheel.brakeTorque = brake;
        rearLeftWheel.brakeTorque = brake;
        rearRightWheel.brakeTorque = brake;

        // Animate the steering wheel
        AnimateSteeringWheel(steering);

        //For rotating the compass

        //// Get the position of the next checkpoint
        //Vector3 targetPosition = CheckpointManager.Instance.GetPos();


        //checkP.transform.rotation=Quaternion.Slerp(checkP.transform.rotation, Quaternion.LookRotation(targetPosition),5f * Time.deltaTime);
        //checkP.transform.LookAt(targetPosition);
    }
    private void LateUpdate()
    {
        if (IsDriving)
        {
            UpdateWheelPoses();
        }
    }

    private void AnimateSteeringWheel(float steering)
    {
        // Rotate the steering wheel based on the steering angle
        float wheelRotation = (steering / maxSteeringAngle) * steeringWheelTurnAngle;
        steeringWheel.localRotation = Quaternion.Euler(0, 0, +wheelRotation);  // Negative for correct direction
    }

    private void ConfigureWheelCollider(WheelCollider wheelCollider)
    {
        wheelCollider.suspensionDistance = 0.3f;  // Increased suspension distance for off-road

        JointSpring spring = wheelCollider.suspensionSpring;
        spring.spring = 10000f;
        spring.damper = 1000f;
        spring.targetPosition = 0.5f;
        wheelCollider.suspensionSpring = spring;

        WheelFrictionCurve forwardFriction = wheelCollider.forwardFriction;
        forwardFriction.extremumSlip = 0.4f;
        forwardFriction.extremumValue = 1f;
        forwardFriction.asymptoteSlip = 0.8f;
        forwardFriction.asymptoteValue = 0.5f;
        forwardFriction.stiffness = 2.0f;  // Increased stiffness for off-road
        wheelCollider.forwardFriction = forwardFriction;

        //WheelFrictionCurve sidewaysFriction = wheelCollider.sidewaysFriction;
        //sidewaysFriction.extremumSlip = 0.2f;
        //sidewaysFriction.extremumValue = 1f;
        //sidewaysFriction.asymptoteSlip = 0.5f;
        //sidewaysFriction.asymptoteValue = 0.75f;
        //sidewaysFriction.stiffness = 2.0f;  // Increased stiffness for off-road
        //wheelCollider.sidewaysFriction = sidewaysFriction;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftWheel, frontLeftTransform, true);
        UpdateWheelPose(frontRightWheel, frontRightTransform, true);
        UpdateWheelPose(rearLeftWheel, rearLeftTransform, false);
        UpdateWheelPose(rearRightWheel, rearRightTransform, false);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform trans, bool isFrontWheel)
    {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos, out quat);
     //  trans.position = pos;

      //   Apply the wheel rotation
       trans.rotation = quat;

        // Apply the steering angle to the front wheels
       if (isFrontWheel)
       {
            trans.localRotation = Quaternion.Euler(trans.localRotation.eulerAngles.x, collider.steerAngle, trans.localRotation.eulerAngles.z);
            
       }

        // Rotate the wheels based on the car's speed and wheel radius
        float rotationAngle = (collider.rpm / 60) * 360 * Time.deltaTime;  // Convert RPM to degrees per second
        trans.Rotate(Vector3.right, rotationAngle);
    }
}
