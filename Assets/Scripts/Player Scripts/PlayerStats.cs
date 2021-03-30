using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Image health_Stats, stamina_Stats;

        // Start is called before the first frame update
   public void display_Health_Stats(float healthValue){
       healthValue/=100f;
       health_Stats.fillAmount = healthValue;
   }
    public void display_Stamina_Stats(float staminaValue){
       staminaValue/=100f;
       stamina_Stats.fillAmount = staminaValue;
   }
}
