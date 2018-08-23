using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceWorld : MonoBehaviour {

    public GameObject goal;
    public Transform[] goalTransforms;

    int goalId;


	// Use this for initialization
	void Start () {
        SetGoal();
    }

    // Update is called once per frame
	void Update () {
        if(Input.GetKeyUp(KeyCode.G))
        {
            SetGoal();
        }
	}

    void SetGoal()
    {
        ClearGoal();

        int lastId = PlayerPrefs.GetInt("LastGoal",0);

        while (goalId == lastId)
        {
		
            goalId = GenerateRandomInt(0, goalTransforms.Length);

        }

        PlayerPrefs.SetInt("LastGoal",goalId);

        GameObject newGoal = Instantiate(goal, goalTransforms[goalId].transform.position,Quaternion.identity);
        newGoal.transform.parent = goalTransforms[goalId].transform;
        print("Set new Goal Position " + goalId);

    }

    void ClearGoal()
    {
        GameObject[] currentGoal = GameObject.FindGameObjectsWithTag("Goal");



        if(currentGoal != null)
        {
            foreach (GameObject g in currentGoal)
            {
                Destroy(g);
                print("All Goals turned off");
            } 
        }



    }

    int GenerateRandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }
}
