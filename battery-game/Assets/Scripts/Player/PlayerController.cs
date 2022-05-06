using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform movePoint;
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask whatStopsMovement;
    // [SerializeField] Animator animator;
    float x, y, circleColliderRadius = .2f;
    Vector3 newPosition;

    void Start()
    {
        transform.position = spawnPoint.position;
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                x = Input.GetAxisRaw("Horizontal");
                y = 0f;
                newPosition = movePoint.position + new Vector3(x, y, 0f);
                if (!Physics2D.OverlapCircle(newPosition, circleColliderRadius, whatStopsMovement))
                {
                    movePoint.position = newPosition;
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                x = 0f;
                y = Input.GetAxisRaw("Vertical");
                newPosition = movePoint.position + new Vector3(x, y, 0f);
                if (!Physics2D.OverlapCircle(newPosition, circleColliderRadius, whatStopsMovement))
                {
                    movePoint.position = newPosition;
                }
            }

            // animator.SetBool("isMoving", false);
        }
        else
        {
            // animator.SetBool("isMoving", true);
        }
    }
}
