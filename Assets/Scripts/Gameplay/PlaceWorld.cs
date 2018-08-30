using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceWorld : MonoBehaviour
{

    [Header("Main Goal")]
    public GameObject goal;
    public Transform[] goalTransforms;

    [Header("Heating Points")]
    public GameObject storyPoint;
    public GameObject heatingPoint;
    public List<GameObject> heatingPoints = new List<GameObject>();
    public int disabledHeatPoints = 2;

    int goalId;
    int storyPointId;

    public delegate void OnSetup();
    public static event OnSetup OnSetupInitializedEvent;

    void Start()
    {
        InitRandomization();
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PlayerPrefs.DeleteAll();
            print("PlayerPrefs Deleted");
        }
    }

    void InitRandomization()
    {
        ClearPOIs();
        ClearGoals();

        SetGoal();
        SetHeatingPoints();

        // sub = ChangeMaterial
        if(OnSetupInitializedEvent != null){
            OnSetupInitializedEvent();
        }
    }

    #region Setup

    void SetGoal()
    {
        int lastId = PlayerPrefs.GetInt("LastGoal", 0);

        while (goalId == lastId)
        {
            goalId = GenerateRandomInt(0, goalTransforms.Length);
        }

        PlayerPrefs.SetInt("LastGoal", goalId);

        GameObject newGoal = Instantiate(goal, goalTransforms[goalId].transform.position, Quaternion.identity);
        newGoal.transform.parent = goalTransforms[goalId].transform;
    }

    void SetHeatingPoints()
    {
        int lastStoryPointId = PlayerPrefs.GetInt("LastStoryPoint", 0);

        while (storyPointId == lastStoryPointId)
        {
            storyPointId = GenerateRandomInt(0, heatingPoints.Count);
        }

        PlayerPrefs.SetInt("LastStoryPoint", storyPointId);

        RandomDeleteHeatPoints();

        for (int i = 0; i < heatingPoints.Count; i++)
        {
            if (i != storyPointId)
            {
                GameObject newHeatingPoint = Instantiate(heatingPoint, heatingPoints[i].transform.position, Quaternion.identity);
                newHeatingPoint.transform.parent = heatingPoints[i].transform;
            }

            if (i == storyPointId)
            {
                GameObject newStoryPoint = Instantiate(storyPoint, heatingPoints[i].transform.position, Quaternion.identity);
                newStoryPoint.transform.parent = heatingPoints[i].transform;
            }
        }
    }

    #endregion

    #region TidyUp
    void ClearPOIs()
    {
        GameObject[] currentPOIs = GameObject.FindGameObjectsWithTag("POI");

        if (currentPOIs != null)
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

    #region Randomization

    void RandomDeleteHeatPoints()
    {
        for (int del = 0; del < disabledHeatPoints; del++)
        {
            heatingPoints.RemoveAt(Random.Range(0, heatingPoints.Count));
        }
    }

    int GenerateRandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }

    #endregion
}
