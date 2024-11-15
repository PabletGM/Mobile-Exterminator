using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This DamageVisualizer class is a Unity MonoBehaviour script that visually indicates when an object
// has taken damage by briefly switching its material (making it “blink” with a new material).
// The effect gradually returns the object to its original material after taking damage.

public class DamageVisualizer : MonoBehaviour
{
    // Renderer component of the object responsible for drawing the object in the scene
    [SerializeField] Renderer mesh;
    // Material to switch to when damage occurs
    [SerializeField] Material damageMaterial;
    // Original material that the object starts with
    [SerializeField] Material originalMaterial;
    // Speed at which the material fades back to its original material
    [SerializeField] float blinkSpeed = 2f;
    // Reference to the HealthComponent that triggers TookDamage when damage is taken
    [SerializeField] HealthComponent healthComponent;

    // Boolean to track if the damage effect is active
    bool isTakingDamage = false;
    // Time taken to lerp back to original material
    float lerpTimer = 0f;

    void Start()
    {
        // Save the original material in case it is not set
        if (originalMaterial == null)
        {
            originalMaterial = mesh.material;
        }

        // Register TookDamage as a listener to healthComponent.onTakeDamage
        healthComponent.onTakeDamage += TookDamage;
    }

    // Called when damage is taken
    protected virtual void TookDamage(float health, float amount, float maxHealth, GameObject instigator)
    {
        // Start the lerping effect, immediately apply damage material
        mesh.material = damageMaterial;

        // Start the lerping effect
        isTakingDamage = true;
        lerpTimer = 0f;  // Reset the lerp timer
    }

    void Update()
    {
        // If damage is being taken, gradually return to the original material
        if (isTakingDamage)
        {
            // Increment the lerp timer over time
            lerpTimer += Time.deltaTime * blinkSpeed;

            // Lerp between damageMaterial and originalMaterial based on lerpTimer
            // A new material is applied based on the lerp
            if (lerpTimer < 1f)
            {
                // We use lerp with materials by gradually blending between their properties
                mesh.material.Lerp(damageMaterial, originalMaterial, lerpTimer);
            }
            else
            {
                // Once the lerp is complete, set to the original material
                mesh.material = originalMaterial;
                isTakingDamage = false;
            }
        }
    }
}