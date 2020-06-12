using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 movementVector = new Vector3(6f,0,0);
    [Range(-1,1)][SerializeField] float movementFactor;
    [SerializeField] float period;
    private Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = 0f;
        //dont directly compare float num to 0, since float is "floating"
        //instead, to make code more robust, compare to Epsilon, the smallest
        //unit in float
        if (period > Mathf.Epsilon) {cycles = Time.time / period;}
        movementFactor = Mathf.Sin(cycles * 2 * Mathf.PI);
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
