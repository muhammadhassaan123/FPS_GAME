using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;
    public float health = 100f;
    public bool is_Player,is_Boar,is_Cannibal;
    private bool is_Dead;
    private int debugVar;
    private EnemyAudio enemyAudio;
    private PlayerStats playerStats;
    //public Text scoring;
   
    

    public static int  Score =0;
    void Awake()
    {
        
       if(is_Boar || is_Cannibal){
           enemy_Anim =GetComponent<EnemyAnimator>();
           enemy_Controller = GetComponent<EnemyController>();
           navAgent= GetComponent<NavMeshAgent>();
           enemyAudio = GetComponentInChildren<EnemyAudio>();
           //get audio
       } 
       if(is_Player){
           playerStats = GetComponent<PlayerStats>();
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyDamage(float damage){
        if(is_Dead){
            return;
        }
        health -= damage;
        
        //print("Damaged:"+damage);
        if(is_Player){
        
            //Show stats
            playerStats.display_Health_Stats(health);    
        }
        if(is_Boar || is_Cannibal){
            if(enemy_Controller.Enemy_State == EnemyState.PATROL){
                enemy_Controller.chase_Distance = 50;
            }
        }
        if(health <= 0f){
            is_Dead= true;
            PlayerDead();
        }
        
        
    }//
    void PlayerDead(){
        if(is_Cannibal){
            enemy_Anim.Dead();
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger=false;
            //GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);
            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled= false;
            StartCoroutine(playDeadSound());
            Score+=5;
           
            // print("Cannibal Dead");
            
            //EnemyManager Spawn more Enemies
            
            EnemyManager.instance.EnmeyDied(true);
        }
        if(is_Boar){
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;
            enemy_Anim.Dead();
            Score+=2;
            
            
            StartCoroutine(playDeadSound());
            //EnemyManager Spawn more Enemies
            EnemyManager.instance.EnmeyDied(false);

        }
        if(is_Player){
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
            for(int i = 0;  i< enemies.Length;i++){
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled=false;
            GetComponent<WeaponManager>().GetCurrentWeapon().gameObject.SetActive(false);
            EnemyManager.instance.StopSpawning();
            if(PlayerPrefs.GetInt("BestScore")>Score){
             PlayerPrefs.SetInt("BestScore",Score);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible =true;
        }
        if(tag == Tags.PLAYER_TAG){
            Invoke("RestartGame",5f);
        }else{
            Invoke("TurnOffGameObject",5f);
        }
    }//
    void RestartGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

    }
    void TurnOffGameObject(){
        gameObject.SetActive(false);
    }
    IEnumerator playDeadSound(){
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_DeadSound();
    }
    
   
}
