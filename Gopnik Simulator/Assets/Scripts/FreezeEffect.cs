using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : MonoBehaviour
{
    public Material mat;
    public float freeze_factor = 1f;
    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Debug.Log(GlobalVariables.vodka_level / GlobalVariables.max_vodka_level);
        mat.SetFloat("_Cutoff", (1-GlobalVariables.vodka_level / GlobalVariables.max_vodka_level)*freeze_factor);
        Graphics.Blit(source, destination, mat);
    }

}
