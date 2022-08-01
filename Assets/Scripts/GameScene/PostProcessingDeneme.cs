using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PostProcessingDeneme : MonoBehaviour
{
    public PostProcessVolume postProcess;
    public DepthOfField depthOf;
    public Vignette vignette;
    public BulletScript bulletScript;
    [Header("Player")]
    public HealthPlayer healthPlayer;
    private void Awake()
    {
        healthPlayer = GameObject.Find("Player").GetComponent<HealthPlayer>();
    }
    private void Update()
    {
        Damage();
        bulletScript = GameObject.FindObjectOfType<BulletScript>();
    }
    void Start()
    {
        postProcess = GetComponent<PostProcessVolume>();
        postProcess.profile.TryGetSettings(out depthOf);
        postProcess.profile.TryGetSettings(out vignette);
    }
    public void StartGame()
    {
        depthOf.aperture.value = Mathf.Lerp(depthOf.aperture.value, 16, 1);
    }
    public void Damage()
    {
        if (bulletScript == null) return;

        if (bulletScript.vignetteWork)
        {
            StartCoroutine(nameof(VignetteWork));
        }
    }
    IEnumerator VignetteWork()
    {
        vignette.enabled.value = true;
        yield return new WaitForSeconds(.15f);
        vignette.enabled.value = false;
        bulletScript.vignetteWork = false;
    }
}
