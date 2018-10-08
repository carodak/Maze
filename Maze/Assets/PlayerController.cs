using UnityEngine;
using System.Threading;
//Adding a PlayerController will add a PlayerMotor that would add a RigidBody
[RequireComponent(typeof(PlayerMotor))]

//Controller of the player
public class PlayerController : MonoBehaviour
{

    [SerializeField] //show up in the inspector even if it is private
    private float speed = 35f; //Movement speed

    [SerializeField]
    private float lookSensitivity = 5f; //Sensitivity when we look around

    [SerializeField]
    private float jumpVelocity = 55f; //Height of the jump

    private PlayerMotor motor;

    private System.Timers.Timer _delayTimer; //Jump delay(during that one the player can not move)

    void Start()
    {
        _delayTimer = new System.Timers.Timer();
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //Calculate our movement velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal"); //going to go between -1 and 1 as using the keyboard
        float zMov = Input.GetAxisRaw("Vertical"); // same

        /* Move horizontally/vertically
         * 
         * take the actual view then move horizontally or vertically
         * transform.right = vector with the values (1,0,0)
         * we multiply it by xMov:
         * not moving (0,0,0), moving forward (1,0,0), moving backward (-1,0,0)
         * 
         * same logic for transform.forward...
         */
        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        //Combine both to obtain our final movement vector = the velocity (direction * speed)
        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

        //Move
        motor.Move(velocity);

        //Calculate rotation as a 3D vector (turning around). Pretty much the same as previously
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        //Rotate
        motor.Rotate(rotation);

        //Calculate camera rotation as a 3D vector (turning around)
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        //Rotate the camera
        motor.RotateCamera(cameraRotation);

    }
    void FixedUpdate()
    {

        //The player can't move during the jump
        if (Input.GetButtonDown("Jump"))
        {
            speed = 0f;

            Vector3 pos = GetComponent<Transform>().position;
            GetComponent<Rigidbody>().velocity = Vector2.up * jumpVelocity;
            _delayTimer.Interval = 1300;
            //_delayTimer.Enabled = true;
            _delayTimer.Elapsed += _delayTimer_Elapsed;
            _delayTimer.Start();

        }


    }

    private void _delayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        speed = 35f;
    }
}

