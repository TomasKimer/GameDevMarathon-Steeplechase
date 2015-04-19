using UnityEngine;
using System.Collections;

public static class MonoBehaviourExtension {

	public static T GetChild<T>(this MonoBehaviour @this, string name) where T : MonoBehaviour {
		T[] children = @this.GetComponentsInChildren<T>(true);

		for (int i = 0; i < children.Length; ++i) {
			T child = children[i];

			if (child.name.Equals(name))
				return child;
		}

		return default(T);
	}
}
