using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(3, 8)] private float playerSpeed = 5f;

    private Animator animator;
    private CharacterController characterController;    

    private bool isRight;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        isRight = true;
    }

    private void Update()
    {
        MovementLogic();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Realize all movement mechanics:
    private void MovementLogic()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        PlayerMove(horizontal, vertical);

        CheckForChangeDirection();

        PlayerWalking(horizontal, vertical);
    }    

    // Function that move player according to buttons that he pressed
    private void PlayerMove(float hor, float ver)
    {
        float xDirection = hor * playerSpeed * Time.deltaTime;
        float yDirection = ver * playerSpeed * Time.deltaTime;

        Vector3 movementVector = new Vector3(xDirection, yDirection, 0);

        characterController.Move(movementVector);
    }

    private void CheckForChangeDirection()
    {
        bool bothDirectionCalled = BothDirectionCalled();

        if (!bothDirectionCalled)
        {
            ChangeDirectionAnim();
        }
    }

    // Function that change animation according to player direction(right/left):
    private void ChangeDirectionAnim()
    {
        if (Input.GetKey(KeyCode.A) && isRight)
        {
            FlipDirection();
        }
        else if (Input.GetKey(KeyCode.D) && !isRight)
        {
            FlipDirection();
        }     
    }

    // If player pressed both buttons(A and D) in the same time, then animation constantly changing direction.
    // In this function we check for this bug:
    private bool BothDirectionCalled()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            return true;
        }
        return false;
    }

    // Function that set walk animation if player moving:
    private void PlayerWalking(float hor, float ver)
    {
        if (hor != 0 || ver != 0)
            animator.SetBool("isWalk", true);
        else
            animator.SetBool("isWalk", false);
    }

    // Function that rotate player:
    private void FlipDirection()
    {
        isRight = !isRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;   // -1 because we should mirror player
        transform.localScale = theScale;
    }
}
