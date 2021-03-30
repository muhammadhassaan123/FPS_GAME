    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonFunctions : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject sceneTransforerLoader;
        
    public void PlayGame(){
       sceneTransforerLoader.SetActive(true);
   }
    public void QuiteGame(){
        Application.Quit();
    }
    public void GoToMenu(){
        SceneManager.LoadScene(1);
    }
}
