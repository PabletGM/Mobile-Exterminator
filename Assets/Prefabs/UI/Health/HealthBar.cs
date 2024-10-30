using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    //position of the HealthBar
    private Transform _attachPoint;

    public void Init(Transform attachPoint)
    {
        _attachPoint = attachPoint;
    }

    //change health slider value
    public void SetHealthSliderValue(float health, float amount, float maxHealth)
    {
        healthSlider.value = health/maxHealth;
    }

    private void Update()
    {
        //lets make sure we are always in the UISpace
        //we transform the HealthBar._attachPoint (in the enemy head) to UI coordinates
        Vector3 attachScreenPoint = Camera.main.WorldToScreenPoint(_attachPoint.position);
        //we put the healthBar on that UIPoint.
        transform.position = attachScreenPoint;
    }
}
