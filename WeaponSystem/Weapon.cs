using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    protected FiniteStateMachine _fsm;
    
    public Transform firePoint;
    public LayerMask targetMask;
    public UnityEvent OnShot;
    
    protected abstract void InitFSM();

    protected void Start()
    {
        InitFSM();
    }

    protected void Update()
    {
        _fsm.Update();
    }

    public void ShootRay(Vector3 from, Vector3 dir, LayerMask mask)
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(from, dir);
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, mask))
        {
            //todo: invoke event
        }
        
        OnShot.Invoke();
    }

    public void ShootProjectile(Vector3 from, Vector3 dir, Transform projectilePrefab)
    {
        Transform projectile = Instantiate(projectilePrefab, from, Quaternion.LookRotation(dir));
        
        OnShot.Invoke();
    }
}