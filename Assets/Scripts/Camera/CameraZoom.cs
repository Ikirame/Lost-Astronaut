using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraZoom : MonoBehaviour
{

    public float zoom = 2.0f;
    public float speedIn = 50.0f;
    public float speedOut = 50.0f;

    public float bloomThreashold = 5.0f;

    private bool zoomIn = false;

    private float initFov;
    private float currFov;
    private float minFov;
    private float addFov;
    private VignetteAndChromaticAberration v;
    private BloomOptimized b;
    private HealthManager player;

    public float vMax = 10.0f;
    public float bMax = 1.5f;

    void Start()
    {
        initFov = Camera.main.fieldOfView;
        minFov = initFov / zoom;
        v = GetComponent<VignetteAndChromaticAberration>() as VignetteAndChromaticAberration;
        b = GetComponent<BloomOptimized>();
        player = FindObjectOfType<HealthManager>();
    }

    void Update()
    {
        if (zoomIn)
            ZoomView();
        else
            ZoomOut();

        float currDistance = currFov - initFov;
        float totalDistance = minFov - initFov;
        float vMultiplier = currDistance / totalDistance;
        float vAmount = vMax * vMultiplier;

        vAmount = Mathf.Clamp(vAmount, 0, vMax);
        v.intensity = vAmount;
    
        if (player.oxygen < bloomThreashold)
        {
            float bMultiplier = (bloomThreashold - player.oxygen) / bloomThreashold;
            b.intensity = bMax * bMultiplier;
        }
    }
    void ZoomView()
    {
        currFov = Camera.main.fieldOfView;
        addFov = speedIn * Time.deltaTime;

        if (Mathf.Abs(currFov - minFov) < 0.5f)
            currFov = minFov;
        else if (currFov - addFov >= minFov)
            currFov -= addFov;
        Camera.main.fieldOfView = currFov;
    }

    void ZoomOut()
    {
        currFov = Camera.main.fieldOfView;
        addFov = speedOut * Time.deltaTime;

        if (Mathf.Abs(currFov - initFov) < 0.5f)
            currFov = initFov;
        else if (currFov + addFov <= initFov)
            currFov += addFov;
        Camera.main.fieldOfView = currFov;
    }

    public void DeathZoom()
    {
        zoomIn = true;
    }
}
