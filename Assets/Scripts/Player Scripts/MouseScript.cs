using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseScript : MonoBehaviour
{
    [SerializeField] private Transform playerRoot, lookRoot;
    [SerializeField] private bool invert;

    [SerializeField] private bool can_unlock = true;

    [SerializeField] private float sensitivity = 5f;

    [SerializeField] private int smooth_steps =10;
    [SerializeField] private float smooth_height = 0.4f;

    [SerializeField] private float roll_Angle = 10f;

    [SerializeField] private float roll_speed= 3f;

     [SerializeField] private Vector2 default_Look_Limits = new Vector2(-70f,80f);
      private Vector2 look_Angles;
     private Vector2 current_Mouse_look;
     private Vector2 smooth_Move;
     private float current_Roll_Angles;
    
    private int last_look_frame;
 
    public static bool gameIsPaused=false;
    public GameObject PauseMenuUI;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //pM = new PauseMenu();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible =false;
    }

    // Update is called once per frame
    void Update()
    {   
        LockAndUnlockCursor();

        if(Cursor.lockState == CursorLockMode.Locked){
            LookAround();
        }
        
    }

    void LockAndUnlockCursor(){
        if(Input.GetKeyDown(KeyCode.Escape )){
            if(Cursor.lockState == CursorLockMode.Locked){
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible =true;
                Pause();
            }else{
                Cursor.lockState= CursorLockMode.Locked;
                Cursor.visible =false;
               
                Resume();
            }
        }
    }

    void LookAround(){
        current_Mouse_look = new Vector2(Input.GetAxis(Mouse_Axis.Mouse_Y), Input.GetAxis(Mouse_Axis.Mouse_X));
        look_Angles.x += current_Mouse_look.x * sensitivity * (invert ? 1f : -1f); 
        look_Angles.y += current_Mouse_look.y * sensitivity;

        look_Angles.x = Mathf.Clamp(look_Angles.x,default_Look_Limits.x,default_Look_Limits.y);

        // current_Roll_Angles =Mathf.Lerp(current_Roll_Angles, Input.GetAxisRaw(Mouse_Axis.Mouse_X)*roll_Angle,
        // Time.deltaTime * roll_speed);

        lookRoot.localRotation = Quaternion.Euler(look_Angles.x,0f, current_Roll_Angles);
        playerRoot.localRotation = Quaternion.Euler(0f,look_Angles.y,0f); 
    }
   public void  Resume(){
        
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    
    }
    public void Pause(){
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;        
    }
    public void LoadMenu(){
        SceneManager.LoadScene(1);
         if(PlayerPrefs.GetInt("BestScore")>HealthScript.Score){
             PlayerPrefs.SetInt("BestScore",HealthScript.Score);
            }
    }
}
