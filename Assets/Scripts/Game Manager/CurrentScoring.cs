using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrentScoring : MonoBehaviour
{
    // Start is called before the first frame update
   
    public GameObject textField;
    
    // Update is called once per frame
    void Update()
    {
        textField.GetComponent<Text>().text= "Score:"+ HealthScript.Score ;
        
    }
}
