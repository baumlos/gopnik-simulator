using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkenEffect : MonoBehaviour
{
    public Material mat;
    public float drunken_factor = 0.03f;
    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        float m = (GlobalVariables.vodka_level - GlobalVariables.max_vodka_level / 2) / (GlobalVariables.max_vodka_level * 0.5f) * drunken_factor;

        mat.SetFloat("_Magnitude", m);
        Graphics.Blit(source, destination, mat);
    }
}
