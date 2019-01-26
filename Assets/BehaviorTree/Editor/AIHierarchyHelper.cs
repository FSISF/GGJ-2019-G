using UnityEngine;
using UnityEditor;
using System;
using JFun.Gameplay.BehaviourTree;

namespace JFun.Gameplay.AG.Battle.CharAISystem
{
	[InitializeOnLoad]
	public class AIHierarchyHelper
	{
		public static bool EnableCustomHierarchy = true;

		private static Color _colorSuccess = new Color(0.35f, 0.75f, 0.196f);
		private static Color _colorRunning = new Color(255 / 255f, 126 / 255f, 0f);
		private static Color _colorFailure = new Color(204f / 255f, 0f, 0f);
		private static Color _colorNone = Color.black;

		static AIHierarchyHelper()
		{
			EditorApplication.hierarchyWindowItemOnGUI += HierarchWindowOnGui;
		}

		private static GUIStyle LabelStyle(Color color)
		{
			var style = new GUIStyle(((GUIStyle)"Label"))
			{
				padding =
				{
					left = EditorStyles.label.padding.left-2,
					top = EditorStyles.label.padding.top+1
				},
				normal =
				{
					textColor = color
				}
			};
			return style;
		}

		static void HierarchWindowOnGui(int instanceId, Rect selectionRect)
		{
			if (!EnableCustomHierarchy)
				return;
			try
			{
				var obj = EditorUtility.InstanceIDToObject(instanceId);
				var go = (GameObject)obj;

				GUIStyle style = null;

				AINodeMono aiNodeMono = go.GetComponent<AINodeMono>();
				if (aiNodeMono != null)
				{
					if (aiNodeMono.ResultState == AINodeResult.None)
					{
						return;
					}
					else if (aiNodeMono.ResultState == AINodeResult.Running)
					{
						style = LabelStyle(_colorRunning);
					}
					else if (aiNodeMono.ResultState == AINodeResult.Success)
					{
						style = LabelStyle(_colorSuccess);
					}
					else if (aiNodeMono.ResultState == AINodeResult.Failure)
					{
						style = LabelStyle(_colorFailure);
					}
					else
					{
						style = LabelStyle(_colorNone);
					}
				}

				if (style != null && go.activeInHierarchy)
				{
					GUI.Label(selectionRect, go.name, style);
				}
			}
			catch (Exception)
			{
			}
		}
	}
}