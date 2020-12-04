using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    private Rigidbody body;
    public float grav = 1f, velTransfer = 0.85f, startPush = 500f, tiltRate = 0.9f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;
    public Transform pen;
    private Transform last;
    public Vector3 penPos;

    private float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter;

    public int score = 0;
    public int missed = 0;
    public float time = 0f;

    private Text missedText;
    private Text scoreText;
    private Text timer;

    public bool invertVertical = true;
    public bool invertHorizontal = true;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;
        body.AddForce(transform.forward * startPush);
        last = pen;

        missedText = GameObject.Find("Missed").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        timer = GameObject.Find("Timer").GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        timer.text = time.ToString("0.00s");

        Vector3 p = pen.position;
        penPos = transform.position - p;

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        // float roll = penPos.x / 5;
        // float tilt = penPos.y / 5;

        float roll = (invertHorizontal? 1 : -1) * (lookInput.x - screenCenter.x) / screenCenter.x;
        float tilt = (invertVertical? 1 : -1) * (lookInput.y - screenCenter.y) / screenCenter.y;


        if (body.velocity.magnitude < 1.5){
            //tilt += tiltRate * Time.deltaTime;
            tilt = ((Screen.height * 0.6f) - screenCenter.y) / screenCenter.y;
        }

        if(Input.GetKeyDown("space")){
            body.AddForce(transform.forward * startPush);
        }

        //transform.Rotate(-tilt * lookRateSpeed * Time.deltaTime, roll * lookRateSpeed * Time.deltaTime, 0f, Space.Self);
        transform.Rotate(transform.right, tilt * Time.deltaTime * lookRateSpeed, Space.World);
        transform.Rotate(transform.forward, -roll * Time.deltaTime * lookRateSpeed, Space.World);

        //transform.Rotate(pen.rotation.eulerAngles.x, pen.rotation.eulerAngles.y, pen.rotation.eulerAngles.z, Space.World);

        // Covert falling speed to regular speed 
        Vector3 vertvel = body.velocity - Vector3.Exclude(transform.up, body.velocity);
        body.velocity -= vertvel * Time.deltaTime;
        body.velocity += vertvel.magnitude * transform.forward * Time.deltaTime * velTransfer;
        //body.velocity = body.velocity.magnitude * Vector3.Normalize(transform.forward);

        Vector3 forwardDrag = body.velocity - Vector3.Exclude(transform.forward, body.velocity);
        body.AddForce(-forwardDrag * forwardDrag.magnitude * Time.deltaTime);

        Vector3 sideDrag = body.velocity - Vector3.Exclude(transform.right, body.velocity);
        body.AddForce(-sideDrag * sideDrag.magnitude * Time.deltaTime);

        // Gravity
        body.velocity -= Vector3.up * Time.deltaTime * grav;

        //Debug.Log(body.velocity.magnitude);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ring") {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            time = 0f;
            score = missed = 0;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "missed") {
            missedText.text = "Missed: " + ++missed;
        } else if (collision.tag == "inside ring") {
            //this is the cylinder
            scoreText.text = "Score: " + ++score;
        }
        Destroy(collision.gameObject);
    }
}
