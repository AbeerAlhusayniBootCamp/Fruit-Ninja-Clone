using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public Vector3 direction { get; private set; }
    public float minSliceVolacity = 0.01f;
    public float sliceForce = 5f;
    private TrailRenderer trail;
    private Collider bladeCollider;
    private bool slicing;
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        trail = GetComponentInChildren<TrailRenderer>();
    }
    private void OnEnable()
    {
        StopSlicing();
    }
    private void OnDisable()
    {
        StopSlicing();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }else if(Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 newPosiotion = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosiotion.z = 0f;
        transform.position = newPosiotion;
        slicing = true;
        bladeCollider.enabled = true;
        trail.enabled = true;
        trail.Clear();
    }
    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        trail.enabled = false;
    }
    private void ContinueSlicing()
    {
        Vector3 newPosiotion = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosiotion.z = 0f;
        direction = newPosiotion - transform.position;
        float volacity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = volacity > minSliceVolacity;
        transform.position = newPosiotion;
    }
}
