using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowNoise : MonoBehaviour
{
    public Shader snowfallShader;
    private Material snowfallMat;
    private MeshRenderer meshRenderer;
    [Range(0.001f,0.1f)]
    public float flakeAmount;
    [Range(0f,1f)]
    public float flakeOpacity;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        snowfallMat = new Material(snowfallShader);
    }

    // Update is called once per frame
    void Update()
    {
        snowfallMat.SetFloat("_FlakeAmount", flakeAmount);
        snowfallMat.SetFloat("_FlakeOpacity", flakeOpacity);

        RenderTexture snow = (RenderTexture) meshRenderer.material.GetTexture("_Splat");
        RenderTexture temp = RenderTexture.GetTemporary(snow.width, snow.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(snow, temp, snowfallMat);
        Graphics.Blit(temp, snow);
        meshRenderer.material.SetTexture("_Splat", snow);
        RenderTexture.ReleaseTemporary(temp);
    }
}
