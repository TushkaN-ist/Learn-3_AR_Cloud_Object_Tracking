using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptablePrefabElements : ScriptableObject
{
	public string specificName="NoName";
	public PrefabsObjects[] prefabItems;

	[System.Serializable]
	public struct PrefabsObjects
	{
		public string trackableName;
		public GameObject prefab;
	}
}
