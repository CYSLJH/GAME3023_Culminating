using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
        //transform.position += new Vector3(inputX * moveSpeed * Time.deltaTime, inputY * moveSpeed * Time.deltaTime, 0);
    }

    private void FixedUpdate()
    {
        
    }
}
