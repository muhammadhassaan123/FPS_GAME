using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private WeaponManager weaponManager;
    public float fireRate= 15f;
    private float nextTimeToFire;
    public float damage=20f;
    private Animator zoomCameraAnim;
    private bool zoomed;
    private bool isAiming;
    private Camera mainCam;
    private GameObject crossHair;

    [SerializeField] private GameObject arrow_Prefab, spear_Prefab;
    [SerializeField] private Transform arrow_Bow_StartPosition;
    
    void Awake(){
        weaponManager = GetComponent<WeaponManager>();
        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crossHair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCam =Camera.main;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print("Working");
        WeaponShoot();
        ZoomInAndOut();
    }
    void WeaponShoot(){
        //For Assault Rifle
        if(weaponManager.GetCurrentWeapon().fireType == WeaponFireType.MULTIPLE){
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire){
                nextTimeToFire = Time.time + 1f/fireRate;
                weaponManager.GetCurrentWeapon().ShootAnimation();
                BulletFired();
           }

        }else{
            if(Input.GetMouseButtonDown(0)){
                if(weaponManager.GetCurrentWeapon().tag == Tags.AXE_TAG){
                    weaponManager.GetCurrentWeapon().ShootAnimation();
                }
                if(weaponManager.GetCurrentWeapon().bulletType == WeaponBulletType.BULLET){
                    weaponManager.GetCurrentWeapon().ShootAnimation();
                    //print("Calling BulletFired()");
                    BulletFired();
                    //print("BulletFired() Called");

                }else{
                    if(isAiming){
                        weaponManager.GetCurrentWeapon().ShootAnimation();
                        if(weaponManager.GetCurrentWeapon().bulletType == WeaponBulletType.ARROW){
                            //throw Arrow
                            ThroughArrowOrSpear(true);
                        }else if(weaponManager.GetCurrentWeapon().bulletType == WeaponBulletType.SPEAR){
                            //throw spear
                            ThroughArrowOrSpear(false);
                        }
                    }
                   
                }
            }

        }
    }//Update
    void BulletFired(){

        RaycastHit hit;
        //print("Enterd in BulletFired()");
        if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward,out hit)){
            print("Colliding Object is"+hit.transform.tag);
                if( hit.transform.tag==Tags.ENEMY_TAG ){
                    hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
                    
                }

        }

    }

    void ZoomInAndOut(){
        if(weaponManager.GetCurrentWeapon().weapon_Aim == WeaponAim.AIM){
            if(Input.GetMouseButtonDown(1)){
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crossHair.SetActive(false);
            }
            if(Input.GetMouseButtonUp(1)){
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crossHair.SetActive(true);
            }
        }
        if(weaponManager.GetCurrentWeapon().weapon_Aim == WeaponAim.SELF_AIM){
            if(Input.GetMouseButtonDown(1)){
            weaponManager.GetCurrentWeapon().Aim(true);
            isAiming = true;
            }
            if(Input.GetMouseButtonUp(1)){
            weaponManager.GetCurrentWeapon().Aim(false);
            isAiming = false;
            }
        }
    }
    void ThroughArrowOrSpear(bool throwArrow){
        if(throwArrow){
            GameObject arrow = Instantiate(arrow_Prefab);
            arrow.transform.position = arrow_Bow_StartPosition.position;
            arrow.GetComponent<ArrowAndBowScript>().Launch(mainCam);
        }
        {
            GameObject spear = Instantiate(spear_Prefab);
            spear.transform.position = arrow_Bow_StartPosition.position;
            spear.GetComponent<ArrowAndBowScript>().Launch(mainCam);
        }


    }
}
