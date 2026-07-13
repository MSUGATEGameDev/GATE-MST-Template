#if UNITY_EDITOR
namespace LBG.EditorUtils.Editor
{
	using UnityEngine;
	using UnityEditor;

	[InitializeOnLoad]
	internal static class SnappingTracker
	{
		static SnappingTracker()
		{
			ObjectChangeEvents.changesPublished += HandleCreatedGameObjects;
		}

		private static void HandleCreatedGameObjects(ref ObjectChangeEventStream stream)
		{
			int length = stream.length;
			for (int i = 0; i < length; i++)
			{
				var eventType = stream.GetEventType(i);
				if (eventType == ObjectChangeKind.CreateGameObjectHierarchy)
				{
					stream.GetCreateGameObjectHierarchyEvent(i, out var createdGameObjectsEvent);
					int instanceID = createdGameObjectsEvent.instanceId;

					if (AssetDatabase.Contains(instanceID) == true) // don't want to snap assets
					{
						return;
					}

					var createdGameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
					var snaps = new Transform[] { createdGameObject.transform };
					Handles.SnapToGrid(snaps);
				}
			}
		}
	}
}
#endif