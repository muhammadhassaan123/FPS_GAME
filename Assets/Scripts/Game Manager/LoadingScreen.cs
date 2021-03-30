using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    public Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadAsyncOperation(){
        //Creat Asynce Operation
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneIndex); 
        while(gameLevel.progress < 1){
            progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
         
    }
}
