using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;

    [SerializeField] private SteamVR_Action_Boolean fireAction;

    private SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.RightHand;

    private Interactable interactable;
    private AudioSource gunAudio;
    private Hand curHand;

    private void Start()
    {
        gunAudio = GetComponent<AudioSource>();
        interactable = GetComponent<Interactable>();
        interactable.onAttachedToHand += InteractableOnonAttachedToHand;
        interactable.onDetachedFromHand += InteractableOnonDetachedFromHand;
    }

    private void InteractableOnonDetachedFromHand(Hand hand)
    {
        if (curHand == hand)
        {
            curHand = null;
        }
    }

    private void InteractableOnonAttachedToHand(Hand hand)
    {
        if (curHand != null || curHand != hand)
        {
            curHand = hand;
        }
    }

    private void Update()
    {
        if (curHand != null && fireAction.GetStateDown(curHand.handType)) 
        {
            Shoot();
        }
    }

    // Метод для выполнения выстрела
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;
        
        if (gunAudio != null)
        {
            gunAudio.Play();
        }
        
        Destroy(bullet, 2.0f);
    }
}