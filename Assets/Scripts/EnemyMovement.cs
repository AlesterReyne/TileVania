using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    BoxCollider2D myTelescope;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myTelescope = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        myRigidbody.linearVelocity = new Vector2(moveSpeed, myRigidbody.linearVelocity.y);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.linearVelocity.x)), 1f);
    }
}