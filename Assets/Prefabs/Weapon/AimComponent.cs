using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    [SerializeField] Transform Muzzle;
    [SerializeField] float aimRange = 1000;
    [SerializeField] LayerMask aimMask;
    
    //get the target that the raycast is aiming
    public GameObject GetAimTarget(out Vector3 aimDir)
    {
        Vector3 aimStart = Muzzle.position;
        aimDir = GetAimDirection();
        if(Physics.Raycast(aimStart, aimDir, out RaycastHit hitInfo, aimRange, aimMask))
        {
            if(hitInfo.collider.gameObject!= null)
            {
                return hitInfo.collider.gameObject;
            }
            
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Muzzle.position, Muzzle.position + (GetAimDirection() * aimRange));
    }

    //get the Muzzle Direction but without the Y Axis and normalized
    Vector3 GetAimDirection()
    {
        Vector3 muzzleDir = Muzzle.forward;

        return new Vector3(muzzleDir.x,0f,muzzleDir.z).normalized;
    }
}
