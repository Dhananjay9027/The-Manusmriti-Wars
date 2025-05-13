using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private Rig Aimrig;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivty;
    [SerializeField] private float aimSensitivty;
    [SerializeField] private LayerMask aimColliderMask=new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform BulletPrefab;
    [SerializeField] private Transform SpawnPostion;
    [SerializeField] private GameObject MuzzleFlash;
    [SerializeField] private AudioClip Fire;
    [SerializeField] private GameObject CrossHair;
    //  [SerializeField] private Transform vfxHitGreen;
    //  [SerializeField] private Transform vfxHitRed;

    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdperson;

    private Animator animator;
    private float aimRigWeigth;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdperson = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        aimRigWeigth = 0f;
    }
   
    
    private void Update()
    {
       
        Vector3 MouseWorldPosition = Vector3.zero;
        Vector2 screenCentre = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            debugTransform.position = raycastHit.point;
            MouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (starterAssetsInputs.aim)
        {
            CrossHair.SetActive(true);
            aimVirtualCamera.gameObject.SetActive(true);
            thirdperson.SetSenstivity(aimSensitivty);

            Vector3 worldAimTarget = MouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection =(worldAimTarget-transform.position).normalized;
            transform.forward=Vector3.Lerp(transform.forward,aimDirection, Time.deltaTime *20f);
            thirdperson.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
            aimRigWeigth = 1f;
            Aimrig.weight = Mathf.Lerp(Aimrig.weight, aimRigWeigth, Time.deltaTime * 20f);
            if (starterAssetsInputs.Shoot )
            {
               // Debug.Log("shho");
                Vector3 aimDir = (MouseWorldPosition - SpawnPostion.position).normalized;
                Instantiate(MuzzleFlash.transform, SpawnPostion.position, Quaternion.LookRotation(aimDir, Vector3.up));
                Instantiate(BulletPrefab, SpawnPostion.position, Quaternion.LookRotation(aimDir, Vector3.up));
                AudioSource.PlayClipAtPoint(Fire, SpawnPostion.position);
                starterAssetsInputs.Shoot = false;
            }
        }
        else
        {
            CrossHair.SetActive(false);
            aimRigWeigth = 0f;
            Aimrig.weight = Mathf.Lerp(Aimrig.weight, aimRigWeigth, Time.deltaTime * 20f);
            aimVirtualCamera.gameObject.SetActive(false);
            thirdperson.SetSenstivity(normalSensitivty);
            thirdperson.SetRotateOnMove(true);
           animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }
        
        
    }

}
