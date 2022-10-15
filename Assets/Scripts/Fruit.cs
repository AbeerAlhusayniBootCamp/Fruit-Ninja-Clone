using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private GameObject whole;
    [SerializeField] private GameObject sliced;
    private Rigidbody fruitRB;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffects;
    private void Awake()
    {
        fruitRB = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffects = GetComponentInChildren<ParticleSystem>();
    }
    private void Slice(Vector3 direction, Vector3 position, float force)
    {
      FindObjectOfType<GameManager>().IncreaseScore();
        whole.SetActive(false);
        sliced.SetActive(true);
        fruitCollider.enabled = false;
        juiceParticleEffects.Play();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody slice in slices)
        {
            slice.velocity = fruitRB.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Blade bladeScript = other.GetComponent<Blade>();
            Slice(bladeScript.direction,bladeScript.transform.position,bladeScript.sliceForce);
        }
    }
}
