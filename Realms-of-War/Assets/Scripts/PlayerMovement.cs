using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float baseHorisontalSpeed = 20f;

    float horizontalSpeed = 0f;
    bool isJumping = false;

    void Update()
    {
        // Player Movement
        horizontalSpeed = Input.GetAxisRaw("Horizontal") * baseHorisontalSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalSpeed * Time.fixedDeltaTime, isJumping);
        isJumping = false;
    }
}
