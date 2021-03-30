using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character_controller;
    private Vector3 move_Direction;
    public float speed =5f;
    private float gravity=20f;
    public float jump_Force=10f;
    private float vertical_velocity;


    void Awake(){
        character_controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void MoveThePlayer()
    {
        move_Direction=new Vector3(Input.GetAxis(Axis.HORIZONTAL),0f,Input.GetAxis(Axis.VERTICAL));
        move_Direction= transform.TransformDirection(move_Direction);
        move_Direction*=speed*Time.deltaTime;
        ApplyGravity();
        character_controller.Move(move_Direction); 

    }
    void ApplyGravity(){
        vertical_velocity -= gravity*Time.deltaTime;
        if(character_controller.isGrounded){
            
            PlayerJump();
        }
        move_Direction.y = vertical_velocity * Time.deltaTime;
    }
    void PlayerJump(){
        if(Input.GetKeyDown(KeyCode.Space)){

            vertical_velocity = jump_Force;
        }
    }
    // Update is called once per frame
    void Update()
    {
         MoveThePlayer();
    }
}
