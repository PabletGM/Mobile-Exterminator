using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIComponent : MonoBehaviour
{
    //health bar to spawn
    [SerializeField] HealthBar healthBarToSpawn;
    //position of the HealthBar refered to Enemy
    [SerializeField] Transform healthBarAttachPoint;

    private void Start()
    {
        //we find the InGameUI, there is only one
        InGameUI inGameUI = FindObjectOfType<InGameUI>();
        //create the healthBar on the InGameUI transform
        HealthBar newHealthBar = Instantiate(healthBarToSpawn, inGameUI.transform);
        //we put the position of the health bar
        newHealthBar.Init(healthBarAttachPoint);
    }
}
