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

    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("HIT");
        if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1f, layerMask))
        {
            Debug.Log("RaycastHit");
            drawMaterial.SetVector("_Coordinate", new Vector4(groundHit.textureCoord.x, groundHit.textureCoord.y, 0, 0));
            drawMaterial.SetFloat("_Strength", brushStrength);
            drawMaterial.SetFloat("_Size", brushSize);
            RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatMap, temp);
            Graphics.Blit(temp, splatMap, drawMaterial);
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}
