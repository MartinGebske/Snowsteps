using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {

    public Material[] AvailableMaterials;
    Renderer rend;

	private void OnEnable()
	{
        PlaceWorld.OnSetupInitializedEvent += this.InitMatChange;
	}

	private void OnDisable()
	{
        PlaceWorld.OnSetupInitializedEvent -= this.InitMatChange;
	}

	private void Awake()
	{
        rend = GetComponent<Renderer>();
	}
	private void Update()
	{
        if (Input.GetKeyUp(KeyCode.I))
        {
            PlayerPrefs.SetInt("LastMaterialId",0);
            print("material Deleted");
        }
	}

	public void InitMatChange()
    {
        int lastMaterialId = PlayerPrefs.GetInt("LastMaterialId", 0);

        if(lastMaterialId == AvailableMaterials.Length -1)
        {
            PlayerPrefs.SetInt("LastMaterialId",-1);
            ChangeSpecificMaterial(0);
            PlayerPrefs.SetInt("LastMaterialId", 0);
            return;
        }
        else{
            int newID = lastMaterialId += 1;

            PlayerPrefs.SetInt("LastMaterialId", newID); 
        }
         
        ChangeSpecificMaterial(lastMaterialId);
    }

	void ChangeSpecificMaterial(int mat)
    {
        if(mat <= AvailableMaterials.Length -1){
            if(rend == null){
                rend = GetComponent<Renderer>();
            }
            rend.material = AvailableMaterials[mat];
        }
    }
}
