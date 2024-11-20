using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image amountImage;
    [SerializeField] TextMeshProUGUI amountText;

    private float actualLife = 0;

    //public property
    public float ActualLife
    {
        get { return actualLife; }
        set { actualLife = value; }
    }

    internal void UpdateHealth(float health, float amount, float maxHealth)
    {
        amountImage.fillAmount = health/maxHealth;
        amountText.SetText(health.ToString());

        //update property
        ActualLife = amountImage.fillAmount;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
