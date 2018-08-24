using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {

    public Material[] AvailableMaterials;
    int materialId;
    Renderer rend;


    public void InitMatChange()
    {
        
        int lastMaterialId = PlayerPrefs.GetInt("LastMaterialId", 0);


        if(lastMaterialId > AvailableMaterials.Length){
		    PlayerPrefs.SetInt("LastMaterialId", 0);
            materialId = 0;
            ChangeSpecificMaterial(materialId);
        }

        if (materialId <= lastMaterialId)
        {
            materialId += 1;
            PlayerPrefs.SetInt("LastMaterialId", materialId);
            ChangeSpecificMaterial(materialId);
        }


        print("Material ID is: " + materialId);
    }

	void ChangeSpecificMaterial(int mat)
    {
        rend = GetComponent<Renderer>();
        rend.material = AvailableMaterials[mat];
    }


}
