using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

/*Here, we'll have methods for making the player move*/
public class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    private Camera cam; //Add a camera to view at the first person

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Velocity/movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Gets a rotational vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Gets a rotational vector for the camera
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    //Movement and rotation have to be performed at each physics iteration
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //Move our player at the position of the player+velocity
    void PerformMovement()
    {

        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

    }

    //Perform the rotation
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }

}
