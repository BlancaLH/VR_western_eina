using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class arrowBehavior : MonoBehaviour
{

    GameObject cameraParent;
    Collider cameraCollider;

    public AudioClip goodAudio;
    public AudioClip badAudio;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        cameraParent = GameObject.FindGameObjectWithTag("Player");
        cameraCollider = cameraParent.GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }    
    
    void OnCollisionEnter(Collision obj)
    {

        // Play success or failure audio
        if (obj.gameObject.tag == "floor")
        {
            audioSource.clip = badAudio;
            audioSource.Play();
        }
        else if (obj.gameObject.tag == "target")
        {
            audioSource.clip = goodAudio;
            audioSource.Play();
        }


        // Arrow gets stuck when hitting the target
        if (obj.gameObject.tag == "target")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            transform.SetParent(obj.gameObject.transform);

            Collider col = GetComponent<Collider>();
            col.isTrigger = true;

            // Enable physics between the camera and the arrow
            Physics.IgnoreCollision(cameraCollider, col, false);
        }

        // Arrow gets stuck when hitting the floor
        if (obj.gameObject.tag == "floor")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;

            Collider col = GetComponent<Collider>();
            col.isTrigger = true;

            // Enable physics between the camera and the arrow
            Physics.IgnoreCollision(cameraCollider, col, false);
            
        }   
    }
}
