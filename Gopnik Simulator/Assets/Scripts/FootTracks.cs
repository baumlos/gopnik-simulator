using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootTracks : MonoBehaviour
{
    public Shader drawShader;
    private RenderTexture splatMap;
    private Material snowMaterial, drawMaterial;
    public GameObject terrain;
    RaycastHit groundHit;
    LayerMask layerMask;
    [Range(1, 500)]
    public float brushSize;
    [Range(0, 1)]
    public float brushStrength;
    [Range(0, 0.1f)]
    public float stepLength;
    public Vector2 stepDirection;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
        drawMaterial = new Material(drawShader);
        drawMaterial.SetVector("_Color", Color.red);

        snowMaterial = terrain.GetComponent<MeshRenderer>().material;
        splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        snowMaterial.SetTexture("_Splat", splatMap);
    }

    // Update is called once per frame
    void Update()
    {
        DoTheFootprint();
    }
    public void DoTheFootprint()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 5f, layerMask))
        {
            stepDirection = stepDirection.normalized;
            for(int i = -2; i <= 2; i++)
            {
                float tempFloatX = groundHit.textureCoord.x + (i * stepLength) * stepDirection.x;
                float tempFloatY = groundHit.textureCoord.y + (i * stepLength) * stepDirection.y;
                //Debug.Log("RaycastHit: "+ tempFloat);
                Vector4 tempVector = new Vector4(tempFloatX, tempFloatY, 0, 0);

                drawMaterial.SetVector("_Coordinate", tempVector);
                drawMaterial.SetFloat("_Strength", brushStrength);
                drawMaterial.SetFloat("_Size", brushSize);
                RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(splatMap, temp);
                Graphics.Blit(temp, splatMap, drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }
}
