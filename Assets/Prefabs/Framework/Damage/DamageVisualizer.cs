using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

//This DamageVisualizer class is a Unity MonoBehaviour script that visually indicates when an object
//has taken damage by briefly changing its emission color (making it “blink” in a different color).
//The effect gradually returns the object to its original color after taking damage.

public class DamageVisualizer : MonoBehaviour
{
    //This is a Renderer component of the object, which is responsible for drawing the object in the scene. The material on this renderer will be modified to create the damage effect.
    [SerializeField] Renderer mesh;
    //This is the color that the material will briefly switch to when damage occurs, indicating a “damage flash” effect
    [SerializeField] Color DamageEmissionColor;
    //This controls how quickly the emission color of the material fades back to its original color.
    [SerializeField] float blinkSpeed = 2f;
    //The name of the color property in the material shader that will be modified (defaulted to "_Addition"
    [SerializeField] string EmissionColorPropertyName = "_Addition";
    //A reference to the HealthComponent of the object, which handles the health-related aspects and triggers the TookDamage function when damage is taken
    [SerializeField] HealthComponent healthComponent;
    //This variable stores the original color of the emission, which the material will fade back to after the damage effect.
    Color originalEmissionColor;

    void Start()
    {
        //It creates a new material instance for mesh.material to avoid modifying the shared material.
        Material mat = mesh.material;
        mesh.material = new Material(mat);
        //The original emission color is saved so that it can be restored after the damage effect.
        originalEmissionColor = mesh.material.GetColor(EmissionColorPropertyName);
        //registers TookDamage as a listener to healthComponent.onTakeDamage
        healthComponent.onTakeDamage += TookDamage;
    }

    protected virtual void TookDamage(float health, float amount, float maxHealth, GameObject Instigator)
    {
        //Check current color: It checks the current emission color of the material.
        Color currentEmissionColor = mesh.material.GetColor(EmissionColorPropertyName);
        //Color change threshold: If the material color is close to the original color (within a small threshold),
        //it updates the emission color to DamageEmissionColor, which provides a visual flash indicating damage.

        //By getting the grayscale of the color difference, we’re checking how visually similar the two colors are.
        //If the grayscale difference is very small, the two colors are almost identical in brightness and overall appearance
        if (Math.Abs((currentEmissionColor - originalEmissionColor).grayscale) < 0.1f)
        {
            //if they look similar we put the damageEmissionColor
            mesh.material.SetColor(EmissionColorPropertyName, DamageEmissionColor);
        }
    }


    void Update()
    {
        //The color gradually interpolates between the current emission color and originalEmissionColor at a rate controlled by blinkSpeed
        Color currentEmissionColor = mesh.material.GetColor(EmissionColorPropertyName);
        Color newEmissionColor = Color.Lerp(currentEmissionColor, originalEmissionColor, Time.deltaTime * blinkSpeed);
        //we set on each frame this newEmissionColor
        mesh.material.SetColor(EmissionColorPropertyName, newEmissionColor);
    }
}
