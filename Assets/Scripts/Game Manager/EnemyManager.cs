using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private GameObject boar_Prefab, cannibal_Prefab;
    public Transform[] cannibalSpawnPoints,boarSpawnPoints;
    [SerializeField] private int cannibal_Count, boar_Count;
    private int intial_Cannibal_Count,initial_Boar_Count;

    public float wait_Before_Spawn_Enemies_Time=10f; 
    void Awake()
    {
        MakeInstance();
    }

    void Start(){
        initial_Boar_Count = boar_Count;
        intial_Cannibal_Count=cannibal_Count;
        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }
    void SpawnEnemies(){
        SpawnCannibal();
        SpawnBoar();
    }
    void SpawnCannibal(){
        int index=0;

        for(int i=0;i< cannibal_Count; i++){
            if(index >= cannibalSpawnPoints.Length){
                index=0;
            }
            
             Instantiate(cannibal_Prefab,cannibalSpawnPoints[index].position,Quaternion.identity);
             index++;
        }
        cannibal_Count = 0;
    }
    void SpawnBoar(){
        int index=0;

        for(int i=0;i< boar_Count; i++){
            if(index >= boarSpawnPoints.Length){
                index=0;
            }
            
             Instantiate(boar_Prefab,boarSpawnPoints[index].position,Quaternion.identity);
             index++;
        }
        boar_Count = 0;

    }
    // Update is called once per frame
    void MakeInstance()
    {
        if(instance == null)
        instance = this;   
    }
    public void EnmeyDied(bool cannibal){
        if(cannibal){
            cannibal_Count++;
            if(cannibal_Count > intial_Cannibal_Count){
                cannibal_Count = intial_Cannibal_Count;
            }
        }else{
            boar_Count++;
            if(boar_Count > initial_Boar_Count)
            {
                boar_Count = initial_Boar_Count;
            }
        }
    }//
    IEnumerator CheckToSpawnEnemies(){
        yield return new WaitForSeconds(wait_Before_Spawn_Enemies_Time);
        SpawnCannibal();
        SpawnBoar();
    }

    public void StopSpawning(){
        StopCoroutine("CheckToSpawnEnemies");
    }
}
