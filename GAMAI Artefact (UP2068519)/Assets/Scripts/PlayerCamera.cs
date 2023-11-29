using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float rotationSpeed = 1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) { transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.DownArrow)) { transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime); }
    }
}
