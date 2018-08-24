using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceWorld : MonoBehaviour {

    [Header("Main Goal")]
    public GameObject goal;
    public Transform[] goalTransforms;

    [Header("Heating Points")]
    public GameObject storyPoint;
    public GameObject heatingPoint;
    public Transform[] heatingPoints;

    int goalId;
    int storyPointId;

	void Start () {
        SetGoal();
        SetHeatingPoints();
    }


	void Update () {
        if(Input.GetKeyUp(KeyCode.G))
        {
            SetGoal();
            SetHeatingPoints();
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
            print("PlayerPrefs Deleted");
        }
	}

#region Handle Goals

    void SetGoal()
    {
        ClearPOIs();
        ClearGoals();


        int lastId = PlayerPrefs.GetInt("LastGoal",0);

        while (goalId == lastId)
        {
            goalId = GenerateRandomInt(0, goalTransforms.Length);
        }

        PlayerPrefs.SetInt("LastGoal",goalId);

        GameObject newGoal = Instantiate(goal, goalTransforms[goalId].transform.position,Quaternion.identity);
        newGoal.transform.parent = goalTransforms[goalId].transform;
        print("goal is created at " + goalId);
    }

    void ClearPOIs()
    {
        GameObject[] currentPOIs = GameObject.FindGameObjectsWithTag("POI");

        if(currentPOIs != null)
        {
            foreach (GameObject p in currentPOIs)
            {
                Destroy(p);
            } 

        }
    }

    void ClearGoals()
    {
        GameObject[] currentGoals = GameObject.FindGameObjectsWithTag("Goal");

        foreach (GameObject g in currentGoals)
        {
            Destroy(g);
        }

    }

#endregion


    void SetHeatingPoints()
    {
        ClearPOIs();

        int lastStoryPointId = PlayerPrefs.GetInt("LastStoryPoint", 0);

        while (storyPointId == lastStoryPointId)
        {
            storyPointId = GenerateRandomInt(0, heatingPoints.Length);
        }

        PlayerPrefs.SetInt("LastStoryPoint", storyPointId);
       // print("story Point is: " + storyPointId);


        // TODO: discard some transforms from array to get more variations

        // Initialize Heating Points
        for (int i = 0; i < heatingPoints.Length; i++)
        {
            if(i != storyPointId)
            {
                GameObject newHeatingPoint = Instantiate(heatingPoint, heatingPoints[i].transform.position, Quaternion.identity);
                newHeatingPoint.transform.parent = heatingPoints[i].transform;
            //    print("heating Point created at: " + i); 
            }

            if(i == storyPointId)
            {
                GameObject newStoryPoint = Instantiate(storyPoint, heatingPoints[i].transform.position, Quaternion.identity);
                newStoryPoint.transform.parent = heatingPoints[i].transform;
              //  print("StoryPoint created at: " + i);
            }
        }

        ChangeMaterial cMat;
        cMat = storyPoint.GetComponent<ChangeMaterial>();
        if(cMat != null)
        {
            cMat.InitMatChange();
          //  print("mat changed");
        }
        else{
            Debug.LogError("InitMatChange not available please enable ChangeMaterial on target");
        }
    
    }

    int GenerateRandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }
}
