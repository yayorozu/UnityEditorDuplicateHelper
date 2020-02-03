using UnityEngine;
using UnityEditor;

namespace UniLib
{
	public class EditorDuplicateHelper
	{
		[InitializeOnLoadMethod]
		private static void SetDelegate()
		{
			EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
		}

		private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect rect)
		{
			if (Selection.activeInstanceID != instanceID)
				return;

			var e = Event.current;

			if (e.type != EventType.ValidateCommand && e.type != EventType.ExecuteCommand)
				return;

			if ("Duplicate" != e.commandName)
				return;

			var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
			if (gameObject == null)
				return;

			var name = gameObject.name; 

			var scale = gameObject.transform.localScale;
			EditorApplication.delayCall += () =>
			{
				var activeGameObject = Selection.activeGameObject;
				if (activeGameObject == null)
					return;
			
				activeGameObject.transform.name = name;
				activeGameObject.transform.localScale = scale;
			};
		}
	}
}