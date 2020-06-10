using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            print("Thrusting");
            rigidbody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.A)) //can thrust while rotating
        {
            print("Pushing Left Thruster");
            transform.Rotate(-Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D)) //can thrust while rotating
        {
            print("Pushing Right Thruster");
            transform.Rotate(Vector3.forward);
        }

    }
}
