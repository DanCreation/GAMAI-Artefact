using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public float speed = 10;

    public Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) { transform.Translate(Vector3.back * speed * Time.deltaTime); }
        if(Input.GetKey(KeyCode.S)) { transform.Translate(Vector3.forward * speed * Time.deltaTime); }
        if(Input.GetKey(KeyCode.A)) { transform.Translate(Vector3.right * speed * Time.deltaTime); }
        if(Input.GetKey(KeyCode.D)) { transform.Translate(Vector3.left * speed * Time.deltaTime); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            player.transform.position = startPosition;
        }
    }
}
