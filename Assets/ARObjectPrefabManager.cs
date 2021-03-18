using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARObjectPrefabManager : MonoBehaviour
{
    public ARTrackedObjectManager arTrackableObjectManager;

	public ScriptablePrefabElements scriptableElements;

	Dictionary<string, GameObject> prepearItems = new Dictionary<string, GameObject>();
	Dictionary<ARTrackedObject, GameObject> trackedItems = new Dictionary<ARTrackedObject, GameObject>();


	private void OnValidate()
	{
        if (arTrackableObjectManager==null)
            arTrackableObjectManager = GetComponent<ARTrackedObjectManager>();
    }

	// Start is called before the first frame update
	void Start()
    {
        OnValidate();
		foreach(var item in scriptableElements.prefabItems)
		{
			prepearItems.Add(item.trackableName,item.prefab);
		}
		arTrackableObjectManager.trackedObjectsChanged += ArTrackableObjectManager_trackedObjectsChanged;
	}

	private void ArTrackableObjectManager_trackedObjectsChanged(ARTrackedObjectsChangedEventArgs obj)
	{
		if (obj.added.Count>0){
			foreach(ARTrackedObject item in obj.added)
			{
				trackedItems[item] = Instantiate(prepearItems[item.name], item.transform, false);
			}
		}

		if (obj.removed.Count > 0)
		{
			GameObject go;
			foreach (ARTrackedObject item in obj.removed)
			{
				if (trackedItems.TryGetValue(item, out go))
				{
					trackedItems.Remove(item);
					Destroy(go);
				}
			}
		}
	}

}
