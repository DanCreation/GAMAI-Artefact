using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public float playerZPosition;
    public float playerXPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerZPosition = player.transform.position.z;
        playerXPosition = player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) { playerZPosition++; }
        if(Input.GetKey(KeyCode.S)) { playerZPosition--; }
        if(Input.GetKey(KeyCode.A)) { playerXPosition++; }
        if(Input.GetKey(KeyCode.D)) { playerXPosition--; }
    }
}
