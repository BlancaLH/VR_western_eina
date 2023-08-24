using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMovement : MonoBehaviour
{

    changeBlindnessExperiments cb;

    const float translation_speed = 0.025f;
    const float rotation_speed = 2.5f;
    const float camera_y_pos = 1.0f;

    float throw_force = 20.0f;

    bool has_bow = false;
    Vector3 bow_pose;
    GameObject bow = null;
    private Color bow_color;

    bool has_arrow = false;

    bool buttonPressed = false;
    string keyPressed = "";
    KeyCode previousKeyCode;

    Vector3 arrow_pose;
    GameObject arrow = null;

    public GameObject score_text_object;
    Text score_text;

    string game_mode = "";
    string lang = "";

    // Start is called before the first frame update
    void Start()
    {

        score_text = score_text_object.GetComponent<Text>();
        PlayerPrefs.SetInt("numShots", 0);
        lang = PlayerPrefs.GetString("lang", "es");
        UpdateShotText();

        game_mode = PlayerPrefs.GetString("mode", "");

        // If in experiment mode, start with the bow an arrow
        if (game_mode == "experiment")
        {
            bow = GameObject.FindWithTag("bow");
            arrow = GameObject.FindWithTag("arrow");
            TakeBow();
            TakeArrow();

            cb = GameObject.FindGameObjectWithTag("cb").GetComponent<changeBlindnessExperiments>();
        }
    }

    void cameraMovement()
    {
        // Allow movement in free method
        if (game_mode == "free")
        {
            var translation_vector = new Vector3(Input.GetAxis("Axis5"), 0, Input.GetAxis("Axis6")) * translation_speed;  

            transform.Translate(translation_vector);
            transform.position = new Vector3(transform.position.x, camera_y_pos, transform.position.z);
        }

    }

    void TakeBow()
    {
        // Function to take the bow

        // Set the bow in the first plane
        has_bow = true;
        bow.transform.position = Camera.main.transform.position + 0.8f * Camera.main.transform.forward + 0.4f * Camera.main.transform.right;

        bow.transform.rotation = Camera.main.transform.rotation;
        bow.transform.Rotate(90.0f, -90.0f, 0.0f, Space.Self);
        
        // Disable the collisions of the bow
        bow.GetComponent<Rigidbody>().useGravity = false;
        bow.GetComponent<Rigidbody>().detectCollisions = false;

        bow.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bow.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        
        // The CameraParent becomes the parent of the bow too
        bow.transform.SetParent(Camera.main.transform);
    }

    /*
    // Functions used in the first desktop version
    // That version enables taking and leaving the bow

    void LeaveBow()
    {
        bow_pose = bow.transform.position;
        bow.transform.SetParent(null);
        //bow.GetComponent<Rigidbody>().useGravity = true;
        bow.GetComponent<Rigidbody>().detectCollisions = true;

        // Leave the bow a little bit further not to collide with it
        bow.transform.position = bow_pose + transform.forward * 1.25f;
        bow = null;

        has_bow = false;
    }

    void ManageBow()
    {
        
        float bow_distance = 0.0f;

        if (bow != null) 
        {
            bow_distance = Vector3.Distance(bow.transform.position, transform.position);
        }

        if (bow_distance >= 2.0f)
        {
            bow_is_close = false;
        }

        if (bow_is_close && !has_bow && Input.GetKeyDown("q"))
        {
            TakeBow();
        }
        if (has_bow && !has_arrow && Input.GetKeyDown("q"))
        {
            LeaveBow();
        }
    }
    */
    
    void TakeArrow()
    {
        has_arrow = true;

        // Set the arrow in the first plane
        arrow.transform.position = Camera.main.transform.position + 0.8f * Camera.main.transform.forward + 0.4f * Camera.main.transform.right;
        arrow.transform.rotation = Camera.main.transform.rotation;
        
        // Keep gravity and collision to false while the arrow is in the bow
        arrow.GetComponent<Rigidbody>().useGravity = false;
        arrow.GetComponent<Rigidbody>().detectCollisions = false;
        arrow.GetComponent<Rigidbody>().isKinematic = false;

        arrow.GetComponent<Collider>().isTrigger = false;

        arrow.GetComponent<Rigidbody>().velocity = Vector3.zero;
        arrow.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        arrow.transform.SetParent(Camera.main.transform);
    }

    /*
    // Function to leave arrows in the desktop version

    void LeaveArrow()
    {
        arrow_pose = arrow.transform.position;
        arrow.transform.SetParent(null);
        
        arrow.GetComponent<Rigidbody>().detectCollisions = true;
        arrow.transform.position = arrow_pose  + transform.forward * 1.25f;
        arrow = null;

        has_arrow = false;
        
    }
    */

    void DrawTrajectory()
    {
        // Function to draw the trajectory of an arrow
        LineRenderer lr = arrow.GetComponent<LineRenderer>();
        Rigidbody rb = arrow.GetComponent<Rigidbody>();

        int i=0;
        int numberOfPoints = 50;
        float timer = 0.1f;

        lr.positionCount=numberOfPoints;
        lr.enabled=true;
        Vector3 startPosition= arrow.transform.position;

        var cam = Camera.main;
        Quaternion rot = Quaternion.Euler(cam.transform.localEulerAngles.x, 0, 0);

        Vector3 startVelocity= (rot*(throw_force*cam.transform.forward))/rb.mass;
        lr.SetPosition(i,startPosition);
        for(float j=0;i<lr.positionCount-1;j+=timer)
        {
            i++;
            Vector3 linePosition=startPosition+j*startVelocity;
            linePosition.y=startPosition.y+startVelocity.y*j+0.5f*Physics.gravity.y*j*j;
            lr.SetPosition(i,linePosition);
        }
    }


    void UpdateShotText()
    {
        // Function to update the counter of the shots
        int num_shots = PlayerPrefs.GetInt("numShots", 0);
        string shots_msg = "Número de disparos:";

        if (lang == "eng")
        {   
            shots_msg = "Number of shots:";
        }

        score_text.text = shots_msg + " " + num_shots.ToString();
    }


    void ThrowArrow()
    {
        // Function to shoot arrows
        arrow.transform.SetParent(null);
        arrow.GetComponent<Rigidbody>().useGravity = true;
        arrow.GetComponent<Rigidbody>().detectCollisions = true;

        var cam = Camera.main;
        Quaternion rot = Quaternion.Euler(cam.transform.localEulerAngles.x, 0, 0);
        arrow.GetComponent<Rigidbody>().AddForce(rot * (cam.transform.forward * throw_force), ForceMode.Impulse);

        arrow.GetComponent<LineRenderer>().enabled = false;
        Physics.IgnoreCollision(arrow.GetComponent<Collider>(), GetComponent<Collider>());
        arrow = null;

        has_arrow = false;

        int num_shots = PlayerPrefs.GetInt("numShots", 0);
        num_shots = num_shots + 1;
        PlayerPrefs.SetInt("numShots", num_shots);

        UpdateShotText();

    }


    void ManageArrow()
    {
        /*
        // This piece of code enables taking and leaving arrows in the desktop version

        float arrow_distance = 0.0f;

        if (arrow != null) 
        {
            arrow_distance = Vector3.Distance(arrow.transform.position, transform.position);
        }

        if (arrow_distance >= 2.0f)
        {
            arrow_is_close = false;
        }
        
        
        if (arrow_is_close && !has_arrow && has_bow && Input.GetKeyDown("e"))
        {
            TakeArrow();
        } 
        else if (has_arrow && Input.GetKeyDown("e"))
        {
            LeaveArrow();
        }*/

        // Draw the trajetory of the arrow (Notice that is with the key bindings of the bluetooth controller)
        if (has_arrow && Input.GetKey(KeyCode.JoystickButton2) && !IsExceptionKey())
        {
            buttonPressed = true;
            keyPressed = Input.inputString;
            StartCoroutine(DrawTrajectoryCoroutine());
        }

        // Shoot the arrow (Notice that is with the key bindings of the bluetooth controller)
        if (has_arrow && buttonPressed && Input.GetKeyUp(KeyCode.JoystickButton2) && !IsExceptionKey())
        {
            buttonPressed = false;
            StopCoroutine(DrawTrajectoryCoroutine());
            ThrowArrow();
        }

    }

    IEnumerator DrawTrajectoryCoroutine()
    {
        while (buttonPressed)
        {
            previousKeyCode = GetPressedKeyCode();
            DrawTrajectory();
            yield return null;
        }
    }

    KeyCode GetPressedKeyCode()
    {
        foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(keyCode))
            {
                return keyCode;
            }
        }

        return KeyCode.None;
    }

    bool IsExceptionKey()
    {
    return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) 
    || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D);
    }

    // Update is called once per frame
    void Update()
    {
        
        cameraMovement();
         
        // Only the desktop version
        // ManageBow(); 

        ManageArrow();
        
        if (game_mode == "experiment")
        {
            // In experiment mode, take an arrow a produce a change every time the 
            // user rotates
            float camera_y_rot = Camera.main.transform.localEulerAngles.y;
            if (!has_arrow && camera_y_rot > 115.0f && camera_y_rot < 160.0f)
            {
                arrow = GameObject.FindWithTag("arrow");
                TakeArrow();

                cb.performChange();
            }
        }
        
    }

    // This is triggered when colliding with arrows that are stuck
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "arrow" && has_bow && !has_arrow)
        {
            arrow = collider.gameObject;
            TakeArrow();
            
        }
    }

    void OnCollisionEnter(Collision obj)
    {
        // Take the bow when colliding with it
        if (obj.gameObject.tag == "bow")
        {
            bow = obj.gameObject;

            TakeBow();
        }

        // Take the arrows when colliding with it
        if (obj.gameObject.tag == "arrow" && has_bow && !has_arrow)
        {  
            arrow = obj.gameObject;

            TakeArrow();
        }
        
    }
}
