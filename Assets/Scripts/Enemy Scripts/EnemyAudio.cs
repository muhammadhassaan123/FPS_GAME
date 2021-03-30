using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip screamClip,deadClip;
    [SerializeField] private AudioClip[] attackClip;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void Play_ScreamSound(){
        audioSource.clip= screamClip;
        audioSource.Play();
    }
    public void Play_DeadSound(){
        audioSource.clip= deadClip;
        audioSource.Play();
    }
    public void Play_AttackSound(){
        audioSource.clip = attackClip[Random.Range(0,attackClip.Length)];
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
