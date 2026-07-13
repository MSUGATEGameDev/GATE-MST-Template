#if UNITY_EDITOR
namespace LBG.EditorUtils.Editor
{
	using UnityEditor;
	using UnityEditor.Overlays;

	[Overlay(typeof(SceneView), "Snapping Tools")]
	internal sealed class SnappingToolbar : ToolbarOverlay
	{
		public SnappingToolbar() : base(SnapOnCreateToolbarToggle.id) { }
	}
}
#endif