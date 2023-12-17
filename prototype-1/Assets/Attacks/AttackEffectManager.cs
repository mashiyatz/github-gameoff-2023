using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class AttackEffectManager : MonoBehaviour
{
    public GameObject lightAttack;
    public GameObject heavyAttack;
    public GameObject healing;
    //public GameObject globalVol;
    
    public Volume postProcessingVolume;
    public AnimationCurve volumeCurve;
    
    public Volume postProcessingVolume2;
    public AnimationCurve volumeCurve2;
    
    public Volume postProcessingVolume3;
    public AnimationCurve volumeCurve3;
    
    public float animationDuration = 2.5f;

    public GameObject particleSystemPrefab;
    public bool isFinishedPlaying = true;

    public void PlayDemonWeak()
    {
        if (postProcessingVolume == null)
        {
            Debug.LogError("Post-processing Volume not assigned!");
            return;
        }

        // Start the volume animation
        StartCoroutine(AnimateGlobalPostProcessingVolume());
        print("weak played");
    }
    
    public void PlayDemonStrong()
    {
        if (postProcessingVolume == null)
        {
            Debug.LogError("Post-processing Volume not assigned!");
            return;
        }

        // Start the volume animation
        StartCoroutine(AnimateGlobalPostProcessingVolume2());
        print("strong played");
    }
    
    public void PlayPlayerIllusion()
    {
        if (postProcessingVolume == null)
        {
            Debug.LogError("Post-processing Volume not assigned!");
            return;
        }

        // Start the volume animation
        //StartCoroutine(AnimateGlobalPostProcessingVolume3());
        //print("strong played");
    }
    
    public void PlayHealing()
    {
        isFinishedPlaying = false;

        // Instantiate the particle system game object
        GameObject particleSystemObject = Instantiate(healing);

        // Access the ParticleSystem component (assuming it's part of the prefab)
        ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();

        // Set up a coroutine to wait for the particle system to finish playing4
        StartCoroutine(DestroyAfterPlay(particleSystem));
    }

    public void PlayLightAtt()
    {
        isFinishedPlaying = false;

        // Instantiate the particle system game object
        GameObject particleSystemObject = Instantiate(lightAttack);

        // Access the ParticleSystem component (assuming it's part of the prefab)
        ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();

        // Set up a coroutine to wait for the particle system to finish playing4
        StartCoroutine(DestroyAfterPlay(particleSystem));
    }
    
    public void PlayHeavyAtt()
    {
        isFinishedPlaying = false;

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
        isFinishedPlaying = true;

        // Destroy the particle system game object
        Destroy(particleSystem.gameObject);
    }
    
    private System.Collections.IEnumerator AnimateGlobalPostProcessingVolume()
    {
        float timer = 0f;
        float startWeight = postProcessingVolume.weight;

        while (timer < animationDuration)
        {
            float normalizedTime = timer / animationDuration;
            float curveValue = volumeCurve.Evaluate(normalizedTime);

            // Set the global volume weight
            postProcessingVolume.weight = Mathf.Lerp(startWeight, curveValue, normalizedTime);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the volume weight is set to 0 at the end
        postProcessingVolume.weight = 0f;
    }
    
    private System.Collections.IEnumerator AnimateGlobalPostProcessingVolume2()
    {
        float timer = 0f;
        float startWeight = postProcessingVolume2.weight;

        while (timer < animationDuration)
        {
            float normalizedTime = timer / animationDuration;
            float curveValue = volumeCurve2.Evaluate(normalizedTime);

            // Set the global volume weight
            postProcessingVolume2.weight = Mathf.Lerp(startWeight, curveValue, normalizedTime);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the volume weight is set to 0 at the end
        postProcessingVolume2.weight = 0f;
    }
    
    private System.Collections.IEnumerator AnimateGlobalPostProcessingVolume3()
    {
        float timer = 0f;
        float startWeight = postProcessingVolume3.weight;

        while (timer < animationDuration)
        {
            float normalizedTime = timer / animationDuration;
            float curveValue = volumeCurve3.Evaluate(normalizedTime);

            // Set the global volume weight
            postProcessingVolume3.weight = Mathf.Lerp(startWeight, curveValue, normalizedTime);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the volume weight is set to 0 at the end
        postProcessingVolume3.weight = 0f;
    }
}
