#if UNITY_EDITOR
namespace LBG.EditorUtils.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	[FilePath("Library/LBGSnapSettings.asset", FilePathAttribute.Location.ProjectFolder)]
	public sealed class SnapSettings : ScriptableSingleton<SnapSettings>
	{
		#region Inspector Fields

		[SerializeField]
		private bool _snapOnCreate;

		#endregion

		#region Properties

		public bool SnapOnCreate
		{
			get => _snapOnCreate;
			set => _snapOnCreate = value;
		}

		#endregion

		private void OnDisable()
		{
			this.Save(true);
		}
	}
}
#endif