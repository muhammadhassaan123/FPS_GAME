using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAndBowScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody myBody;
    public float speed=30f;
    public  float deactivate_Timer = 5f;
    public float damage =100f;
    void Awake(){
        myBody = GetComponent<Rigidbody>();

    }
    void Start()
    {
        Invoke("DeactivateGameObject",deactivate_Timer);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Launch(Camera mainCamera){
        myBody.velocity = mainCamera.transform.forward* speed;
        transform.LookAt(transform.position + myBody.velocity); 
    }
    void DeactivateGameObject(){
        if(gameObject.activeInHierarchy){
        gameObject.SetActive(false);
        }

    }
    void OnTriggerEnter(Collider target){
         if(target.tag == Tags.ENEMY_TAG){
             target.GetComponent<HealthScript>().ApplyDamage(damage);
         }
    }
}
