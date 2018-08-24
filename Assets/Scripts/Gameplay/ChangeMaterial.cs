using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {

    public Material[] AvailableMaterials;
    Renderer rend;

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
        print("last mat id " + lastMaterialId);

   

        if(lastMaterialId > AvailableMaterials.Length -1)
        {
            PlayerPrefs.SetInt("LastMaterialId",0);
    
        }
        else{
            int newID = lastMaterialId += 1;

        PlayerPrefs.SetInt("LastMaterialId", newID); 

        }
         
        ChangeSpecificMaterial(lastMaterialId);


    }

	void ChangeSpecificMaterial(int mat)
    {
        print(rend);
        if(mat <= AvailableMaterials.Length -1){
            rend.material = AvailableMaterials[mat];
        }
     
    }


}
