using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour {

    public Shader drawShader;

    public GameObject terrain;

    public Transform[] foot;

    [Range(0, 2)] public float brushSize;
    [Range(0, 1)] public float brushStrength;

    int layerMask;

    RaycastHit groundHit;

    Material snowMaterial, drawMaterial;

    RenderTexture splatMap;


	
	void Start () {

        layerMask = LayerMask.GetMask("Ground");

        drawMaterial = new Material(drawShader);
        drawMaterial.SetVector("_Color", Color.red);

        snowMaterial = terrain.GetComponent<MeshRenderer>().material;

        splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        snowMaterial.SetTexture("_Splat", splatMap);
	}

	void Update () {
        for (int i = 0; i < foot.Length; i++)
        {
            if (Physics.Raycast(foot[i].position, -Vector3.up, out groundHit, 1f, layerMask))
                {
                drawMaterial.SetVector("_Coordinate", new Vector4(groundHit.textureCoord.x, groundHit.textureCoord.y, 0, 0));
                    drawMaterial.SetFloat("_Strength", brushStrength);
                    drawMaterial.SetFloat("_Size", brushSize);


                    RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height,
                         0, RenderTextureFormat.ARGBFloat);

                    Graphics.Blit(splatMap, temp);
                    Graphics.Blit(temp, splatMap, drawMaterial);
                    RenderTexture.ReleaseTemporary(temp);
              }
        }
	}
}
