using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectManager : MonoBehaviour
{
    public GameObject lightAttack;
    public GameObject heavyAttack;
    public GameObject healing;
    
    public GameObject particleSystemPrefab;
    
    void Start()
    {
        //PlayHeavyAtt();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayHealing()
    {
        // Instantiate the particle system game object
        GameObject particleSystemObject = Instantiate(healing, transform.position, Quaternion.identity);

        // Access the ParticleSystem component (assuming it's part of the prefab)
        ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();

        // Set up a coroutine to wait for the particle system to finish playing4
        StartCoroutine(DestroyAfterPlay(particleSystem));
    }
    
    public void PlayLightAtt()
    {
        // Instantiate the particle system game object
        GameObject particleSystemObject = Instantiate(lightAttack);

        // Access the ParticleSystem component (assuming it's part of the prefab)
        ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();

        // Set up a coroutine to wait for the particle system to finish playing4
        StartCoroutine(DestroyAfterPlay(particleSystem));
    }
    
    public void PlayHeavyAtt()
    {
        // Instantiate the particle system game object
        GameObject particleSystemObject = Instantiate(heavyAttack, transform.position, Quaternion.identity);

        // Access the ParticleSystem component (assuming it's part of the prefab)
        ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();

        // Set up a coroutine to wait for the particle system to finish playing4
        StartCoroutine(DestroyAfterPlay(particleSystem));
    }
    
    IEnumerator DestroyAfterPlay(ParticleSystem particleSystem)
    {
        // Wait until the particle system has finished playing
        yield return new WaitUntil(() => !particleSystem.isPlaying);

        // Destroy the particle system game object
        Destroy(particleSystem.gameObject);
    }
}