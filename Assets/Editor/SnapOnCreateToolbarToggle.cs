#if UNITY_EDITOR
namespace LBG.EditorUtils.Editor
{
	using UnityEngine;
	using UnityEditor;
	using UnityEditor.Toolbars;
	using UnityEngine.UIElements;

	[EditorToolbarElement(SnapOnCreateToolbarToggle.id, typeof(SceneView))]
	internal sealed class SnapOnCreateToolbarToggle : EditorToolbarToggle
	{
		public const string id = "Snapping/SnapOnCreate";

		public SnapOnCreateToolbarToggle() : base()
		{
			text = "Snap On Create";
			tooltip = "Toggles whether game objects are always automatically snapped to the grid on creation.";
			icon = (Texture2D)EditorGUIUtility.Load("d_SceneViewSnap");
			bool snapOnCreate = SnapSettings.instance.SnapOnCreate;
			this.SetValueWithoutNotify(snapOnCreate);
			this.RegisterValueChangedCallback<bool>(HandleToggleEvent);
		}

		private void HandleToggleEvent(ChangeEvent<bool> e)
		{
			SnapSettings.instance.SnapOnCreate = e.newValue;
		}
	}
}
#endif