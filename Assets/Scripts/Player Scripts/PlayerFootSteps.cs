using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource footSteps_Sound;

    [SerializeField] private AudioClip[]  footSteps_Clip;
    [HideInInspector] public CharacterController character_Controller;

    public float volume_Min, volume_Max;

    private float accumulated_Distance;

    [HideInInspector] public float step_Distance;

    void Awake()
    {
        footSteps_Sound = GetComponent<AudioSource>();
        character_Controller = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootStepSound();
    }
    void CheckToPlayFootStepSound(){
        if(!character_Controller.isGrounded)
            return;
        if(character_Controller.velocity.sqrMagnitude > 0){
            accumulated_Distance += Time.deltaTime;
            if(accumulated_Distance > step_Distance){
                footSteps_Sound.volume = Random.Range(volume_Min,volume_Max); 
                footSteps_Sound.clip = footSteps_Clip[Random.Range(0,footSteps_Clip.Length)];
                footSteps_Sound.Play();

                accumulated_Distance= 0f;
            }
        }
        else{
            accumulated_Distance = 0f;
        }
    }





}
