using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardinalDirection
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}


public class CharacterAnimController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    CardinalDirection direction = CardinalDirection.SOUTH;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        Vector2 velocity = new Vector2(inputX, inputY);


        //Vector2 velocity = rb.velocity;
        bool isWalking = velocity.sqrMagnitude > float.Epsilon;
        bool isMovingHorizontally = Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y);

        if(isMovingHorizontally)
        {
            if (velocity.x < 0)
            {
                direction = CardinalDirection.WEST;
            }
            else 
            {
                direction = CardinalDirection.EAST;
            }
        }
        else
        {
            if (velocity.y > 0)
            {
                direction = CardinalDirection.NORTH;
            }
            else 
            {
                direction = CardinalDirection.SOUTH;
            }
        }

        animator.SetBool("isWalking", isWalking);
        animator.SetInteger("walkDirection", (int)direction);
    }
}
