﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 120f;
    [SerializeField] float mainThrust = 120f;

    enum State{Alive, Dying, Transcending}
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // todo sound still playing while dead, need fix
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
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
        //god operation
        if (state != State.Alive) {return;}

        //print("collided");
        switch (collision.gameObject.tag)
        {
            case "Friendly": 
                print("Still OK");
                break;
            case "Finish":
                state = State.Transcending;
                print("finish");
                Invoke("LoadNextScene", 1f);
                break;
            default:
                state = State.Dying;
                print("dead");
                Invoke("LoadBeginScene", 1f);
                break;
        }
    }

    private void LoadBeginScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
