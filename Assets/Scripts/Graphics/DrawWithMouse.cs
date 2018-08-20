using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour {

    public Camera cam;
    public Shader drawShader;

    private RenderTexture splatMap;

    private Material snowMaterial, drawMaterial;

    private RaycastHit hit;


	// Use this for initialization
	void Start () {
        drawMaterial = new Material(drawShader);
        drawMaterial.SetVector("_Color", Color.red);

        snowMaterial = GetComponent<MeshRenderer>().material;
        splatMap = new RenderTexture(1024, 1024,0,RenderTextureFormat.ARGBFloat);
        snowMaterial.SetTexture("_Splat", splatMap);

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit)) {
                drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y,0,0));

                RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height,
                     0, RenderTextureFormat.ARGBFloat);

                Graphics.Blit(splatMap, temp);
                Graphics.Blit(temp, splatMap, drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
	}
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 256, 256), splatMap, ScaleMode.ScaleToFit, false, 1);
    }
}
