using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowNoise : MonoBehaviour {

    public Shader snowFallShader;
    [Range(0.001f, 0.1f)]public float flakeAmount;
    [Range(0f,1f)]public float flakeOpacity;


    Material snowFallMaterial;
    MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        snowFallMaterial = new Material(snowFallShader);
	}
	
	// Update is called once per frame
	void Update () {
        snowFallMaterial.SetFloat("_FlakeAmount", flakeAmount);
        snowFallMaterial.SetFloat("_FlakeOpacity", flakeOpacity);
        RenderTexture snow = (RenderTexture)meshRenderer.material.GetTexture("_Splat");
        RenderTexture temp = RenderTexture.GetTemporary(snow.width, snow.height, 0, RenderTextureFormat.ARGBFloat);
        // Creates a drawcall ie. for Bloom or Depth 
        Graphics.Blit(snow, temp, snowFallMaterial);
        Graphics.Blit(temp, snow);
        meshRenderer.material.SetTexture("_Splat", snow);
        RenderTexture.ReleaseTemporary(temp);
	}
}
