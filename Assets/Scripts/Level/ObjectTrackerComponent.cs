using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrackerComponent : MonoBehaviour
{
	public enum CAMERASTATE {TRACK_OBJECT, TRACK_OBJECT_MOUSE, STATIC};
	public GameObject trackObjectMain;
	public GameObject trackObjectSub;
	public CAMERASTATE state = CAMERASTATE.TRACK_OBJECT_MOUSE;

	// Update is called once per frame
	void Update()
	{
		if(state == CAMERASTATE.STATIC || !trackObjectMain) {

		} else if(state == CAMERASTATE.TRACK_OBJECT) {
			transform.position = new Vector3(trackObjectMain.transform.position.x, Mathf.Max(trackObjectMain.transform.position.y - 2.00f, 0.0f, 1.3f),transform.position.z);
		} else 
			transform.position = new Vector3((trackObjectMain.transform.position.x + Camera.main.ScreenToWorldPoint(Input.mousePosition).x) / 2,
												Mathf.Max((trackObjectMain.transform.position.y + Camera.main.ScreenToWorldPoint(Input.mousePosition).y) / 2, 0),
												transform.position.z);{
		}
	}
}
