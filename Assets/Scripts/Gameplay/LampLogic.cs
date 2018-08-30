using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLogic : MonoBehaviour {

    public Transform handPosition;
    public float offset;

	void Start () {
        gameObject.transform.SetParent(handPosition);
        gameObject.transform.localPosition = new Vector3(0,offset,0);

	}
}
