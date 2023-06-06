using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EndScreenEffect : MonoBehaviour
{
    public PostProcessVolume volume;
    public LensDistortion lensDistortion;
    public ChromaticAberration chromaticAberration;

    public float[] LDValues;
    public float LDLerpTime;
    public float[] CAValues;
    public float CALerpTime;

    void Start()
    {
        volume.profile.TryGetSettings(out lensDistortion);
        volume.profile.TryGetSettings(out chromaticAberration);
        lensDistortion.intensity.value = LDValues[0];
        chromaticAberration.intensity.value = CAValues[0];
    }

    void Update()
    {
        lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, LDValues[1], LDLerpTime);
        chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, CAValues[1], CALerpTime);
    }
}
