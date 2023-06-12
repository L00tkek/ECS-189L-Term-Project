using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class VignetteController : MonoBehaviour
{
    [SerializeField] PostProcessVolume vol;
    private Vignette vin;
    // Start is called before the first frame update
    void Start()
    {
        vol.profile.TryGetSettings(out vin);
        vin.intensity.value = 0.0f;
    }

    public void setIntensity(float intensity)
    {
        vin.intensity.value = intensity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
