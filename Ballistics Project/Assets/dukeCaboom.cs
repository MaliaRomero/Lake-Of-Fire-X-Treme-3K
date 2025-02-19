using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dukeCaboom : MonoBehaviour
{
    public float myTimeScale = 1.0f;
    public GameObject target;
    public Slider forceSlider;
    private float force;
    Rigidbody rb;
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        Time.timeScale = myTimeScale;
        rb = GetComponent<Rigidbody>();

        if (forceSlider != null)
        {
            force = forceSlider.value;
            forceSlider.onValueChanged.AddListener(UpdateForce);
        }
    }

    void Update()
    {
        Debug.Log("Caboom");
        
        // Update force continuously
        if (forceSlider != null)
        {
            force = forceSlider.value;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            calculateFiringSolution fs = new calculateFiringSolution();
            Nullable<Vector3> aimVector = fs.Calculate(transform.position, target.transform.position, force, Physics.gravity);
            if (aimVector.HasValue)
            {
                rb.AddForce(aimVector.Value.normalized * force, ForceMode.VelocityChange);
            }
            else
            {
                // Fire in the general direction of the target anyway (fallback)
                Vector3 direction = (target.transform.position - transform.position).normalized;
                rb.AddForce(direction * force, ForceMode.VelocityChange);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reset");
            rb.isKinematic = true;
            transform.position = startPosition;
            rb.isKinematic = false;
        }
    }

/*
void Update()
{
    Debug.Log("Caboom");

    if (forceSlider != null)
    {
        force = forceSlider.value;
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
        calculateFiringSolution fs = new calculateFiringSolution();
        Vector3? aimVector = fs.Calculate(transform.position, target.transform.position, force, Physics.gravity);

        if (aimVector.HasValue)
        {
            rb.AddForce(aimVector.Value.normalized * force, ForceMode.VelocityChange);
        }
        else
        {
            // Fire in the general direction of the target anyway (fallback)
            Vector3 direction = (target.transform.position - transform.position).normalized;
            rb.AddForce(direction * force, ForceMode.VelocityChange);
            Debug.Log("No valid firing solution. Firing in general direction.");
        }
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
        Debug.Log("Reset");
        rb.isKinematic = true;
        transform.position = startPosition;
        rb.isKinematic = false;
    }
}
*/
    void UpdateForce(float newForce)
    {
        force = newForce;
    }
}