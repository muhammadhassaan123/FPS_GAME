using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxRotator : MonoBehaviour
{
     public float skyBoxRotationSpeed=10;
    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation",skyBoxRotationSpeed*Time.time);
        
    }
}
