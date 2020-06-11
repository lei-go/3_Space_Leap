using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 120f;
    [SerializeField] float mainThrust = 120f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }
    private void Thrust()
    {
        float thrustSpeed = Time.deltaTime * mainThrust;

        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            //print("Thrusting");
            rigidbody.AddRelativeForce(Vector3.up * thrustSpeed);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true; //take manual control of the movement
        float rotationSpeed = Time.deltaTime * rcsThrust;

        if (Input.GetKey(KeyCode.D)) //can thrust while rotating
        {
            //print("Pushing Left Thruster");
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.A)) //can thrust while rotating
        {
            //print("Pushing Right Thruster");
            transform.Rotate(Vector3.forward * rotationSpeed);
        }

        rigidbody.freezeRotation = false; //let physics controls it
    }

    private void OnCollisionEnter(Collision collision) 
    {
        //print("collided");
        switch (collision.gameObject.tag)
        {
            case "Friendly": 
                print("Still OK");
                break;
            default:
                print("dead");
                break;
        }
    }
}
