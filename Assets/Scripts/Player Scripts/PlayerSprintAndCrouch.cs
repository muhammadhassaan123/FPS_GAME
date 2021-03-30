using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public float sprint_Speed = 10f;
    public float move_Speed=5f;
    public float crouch_Speed=2f;

    private Transform look_Root; 
    private float stand_height=1.6f;
    private float crouch_Height=1f;

    private bool is_Crouching;

    private PlayerFootSteps player_FootSteps;
    private float sprint_Volume =1f;
    private float crouch_Volume = 0.1f;

    private float walk_VolumeMin = 0.2f, walk_VolumeMax = 0.6f;
    private float walk_Step_distance =0.4f;
    private float sprint_Step_distance =0.25f;
    private float crouch_Step_distance =0.5f;
    
    private PlayerStats playerStats;
    private float sprintValue =100f;
    public float sprintTreshold = 10f;
    private NavMeshAgent navMeshAgent;
    void Awake(){
        playerMovement = GetComponent<PlayerMovement>();
        look_Root = transform.GetChild(0);
        player_FootSteps = GetComponentInChildren<PlayerFootSteps>();
        playerStats =GetComponent<PlayerStats>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    
    void Sprint(){
        if(sprintValue >0){
            if(Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching){
            playerMovement.speed = sprint_Speed;
            player_FootSteps.step_Distance = sprint_Step_distance;
            player_FootSteps.volume_Min=sprint_Volume;
            player_FootSteps.volume_Max=sprint_Volume;

            
        }
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching){
            playerMovement.speed = move_Speed;
            player_FootSteps.step_Distance = walk_Step_distance;            
            player_FootSteps.volume_Min = walk_VolumeMin;
            player_FootSteps.volume_Max=walk_VolumeMax;
        }
        if(Input.GetKey(KeyCode.LeftShift) && !is_Crouching && navMeshAgent.velocity.sqrMagnitude >=0){
                sprintValue -= Time.deltaTime * sprintTreshold;
                if(sprintValue <= 0f){
                    sprintValue = 0f;
                    // reset the speed and sound;
                    playerMovement.speed = move_Speed;
                    player_FootSteps.step_Distance = walk_Step_distance;            
                    player_FootSteps.volume_Min = walk_VolumeMin;
                    player_FootSteps.volume_Max=walk_VolumeMax;
                }
                playerStats.display_Stamina_Stats(sprintValue); 
        }else {
            if(sprintValue != 100){
                sprintValue+=(sprintTreshold/2f)* Time.deltaTime;
                playerStats.display_Stamina_Stats(sprintValue);  
                if(sprintValue>100f){
                    sprintValue = 100;
                }             
            }
        }
    }
    void Crouch(){

        if(Input.GetKeyDown(KeyCode.C)){
            if(is_Crouching){
                look_Root.localPosition = new Vector3(0f,stand_height,0f);
                playerMovement.speed =move_Speed;
                is_Crouching = false;
                player_FootSteps.step_Distance = walk_Step_distance;            
                player_FootSteps.volume_Min = walk_VolumeMin;
                player_FootSteps.volume_Max=walk_VolumeMax;
            }else{
                look_Root.localPosition = new Vector3(0f,crouch_Height,0f);
                playerMovement.speed =crouch_Speed;
                is_Crouching=true;
                player_FootSteps.step_Distance = crouch_Step_distance;
                player_FootSteps.volume_Min = crouch_Volume;
                player_FootSteps.volume_Max=  crouch_Volume;


            }
        }
    }
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Start(){
        player_FootSteps.volume_Min = walk_VolumeMin;
        player_FootSteps.volume_Max=walk_VolumeMax;
        player_FootSteps.step_Distance = walk_Step_distance;
    }
    
   
    
    
}
