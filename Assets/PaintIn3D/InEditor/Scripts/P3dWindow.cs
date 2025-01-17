﻿#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace PaintIn3D
{
	/// <summary>This class handles in-editor painting for the main Paint in 3D window.</summary>
	public partial class P3dWindow : P3dEditorWindow
	{
		private int tab;

		private GUIContent[] tabs = new GUIContent[] { new GUIContent("Objects"), new GUIContent("Textures"), new GUIContent("Painting"), new GUIContent("Extras") };

		private Tool oldTool;

		private bool oldToolSet;

		private bool painting;

		private static int logCount;

		[MenuItem("Window/Paint in 3D")]
		public static void OpenWindow()
		{
			GetWindow<P3dWindow>("Paint in 3D", true);
		}

		protected override void OnEnable()
		{
			base.OnEnable();

			// Load
			P3dWindowData.Load();

			Application.logMessageReceived += OnLog;
		}

		protected override void OnInspector()
		{
			tab = GUILayout.Toolbar(tab, tabs);

			EditorGUILayout.Separator();

			switch (tab)
			{
				case 0: DrawTab0(); break;
				case 1: DrawTab1(); break;
				case 2: DrawTab2(); break;
				case 3: DrawTab3(); break;
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			PaintEnd();
			Revert();

			// Save
			P3dWindowData.Save();

			Application.logMessageReceived -= OnLog;
		}

		protected override void OnScene(SceneView sceneView, Camera camera, Vector2 mousePosition)
		{
			if (tab == 2) // Painting
			{
				if (mousePosition.x >= 0.0f && mousePosition.x < sceneView.position.width && mousePosition.y >= 0.0f && mousePosition.y < sceneView.position.height)
				{
					if (Event.current.modifiers == EventModifiers.None)
					{
						var force = false;

						if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
						{
							PaintStart(mousePosition, ref force);
						}

						if (Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseMove || Event.current.type == EventType.MouseDrag)
						{
							if (painting == false)
							{
								Revert();
							}

							Paint(camera, mousePosition, force);
						}
					}
				}
				else
				{
					PaintEnd();
				}

				if (Event.current.type == EventType.MouseUp)
				{
					PaintEnd();
				}

				if (oldToolSet == false)
				{
					oldTool    = Tools.current;
					oldToolSet = true;

					Tools.current = Tool.None;
				}

				if (Tools.current != Tool.None)
				{
					oldTool = Tools.current;
				}

				HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

				Tools.current = Tool.None;

				sceneView.Repaint();
			}
			else
			{
				Revert();
				
				PaintEnd();

				if (oldToolSet == true)
				{
					Tools.current = oldTool;

					oldToolSet = false;
				}
			}
		}

		private void PaintStart(Vector2 mousePosition, ref bool force)
		{
			if (painting == false)
			{
				painting           = true;
				force              = true;

				foreach (var brush in P3dWindowData.Instance.CurrentBrushesSafe)
				{
					brush.LastMousePosition = mousePosition;
				}
			}
		}

		private void Paint(Camera camera, Vector2 mousePosition, bool force)
		{
			foreach (var brush in P3dWindowData.Instance.CurrentBrushesSafe)
			{
				if (painting == true && brush.DragStep > 0.01f)
				{
					var delta = mousePosition - brush.LastMousePosition;
					var steps = Mathf.FloorToInt(delta.magnitude / brush.DragStep);

					if (steps > 0)
					{
						var step = P3dHelper.Reciprocal(steps);

						for (var i = 0; i < steps; i++)
						{
							Paint(camera, Vector2.Lerp(brush.LastMousePosition, mousePosition, i * step));
						}

						brush.LastMousePosition = mousePosition;
					}
				}
				else
				{
					Paint(camera, mousePosition);

					brush.LastMousePosition = mousePosition;
				}
			}
		}

		private void Paint(Camera camera, Vector2 mousePosition)
		{
			var repeaters = new List<P3dTransform>();

			foreach (var brush in P3dWindowData.Instance.CurrentBrushesSafe)
			{
				repeaters.AddRange(brush.GetComponents<P3dTransform>());
			}

			foreach (var brush in P3dWindowData.Instance.CurrentBrushesSafe)
			{
				var ray = GetRay(camera, mousePosition);
				var hit = default(RaycastHit);

				if (Raycast(ray, ref hit) == true)
				{
					var hitPoints = brush.GetComponentsInChildren<IHitPoint>();
					var commands  = new List<P3dCommand>();

					foreach (var brushHandler in hitPoints)
					{
						var rotation = Quaternion.LookRotation(-hit.normal, camera.transform.up);

						brushHandler.HandleHitPoint(commands, repeaters, false, hit.collider, hit.point, rotation, 1.0f);
					}

					foreach (var command in commands)
					{
						foreach (var paintable in paintables)
						{
							paintable.Paint(command, paintables);
						}
					}
				}
			}
		}

		private Ray GetRay(Camera camera, Vector2 screen)
		{
			var pointR = HandleUtility.GUIPointToWorldRay(screen);
			var pointA = pointR.origin + pointR.direction * camera.nearClipPlane;
			var pointB = pointR.origin + pointR.direction * camera.farClipPlane;

			return new Ray(pointA, pointB - pointA);
		}

		private void Revert()
		{
			for (var i = paintables.Count - 1; i >= 0; i--)
			{
				paintables[i].Revert();
			}
		}

		private void PaintEnd()
		{
			if (painting == true)
			{
				painting = false;

				for (var i = paintables.Count - 1; i >= 0; i--)
				{
					paintables[i].Apply();
				}

				Repaint();
			}
		}

		private bool Raycast(Ray ray, ref RaycastHit hit)
		{
			hit.distance = float.PositiveInfinity;

			for (var i = paintables.Count - 1; i >= 0; i--)
			{
				var newHit = default(RaycastHit);

				if (paintables[i].Raycast(ray, ref newHit) == true)
				{
					if (newHit.distance < hit.distance)
					{
						hit = newHit;
					}
				}
			}

			return hit.distance < float.PositiveInfinity;
		}

		private void ValidatePaintables()
		{
			for (var i = paintables.Count - 1; i >= 0; i--)
			{
				var paintable = paintables[i];

				if (paintable.Root == null)
				{
					paintables.RemoveAt(i);
				}
			}
		}

		private void OnLog(string condition, string stackTrace, LogType type)
		{
			logCount++;
		}

		public bool CanReadWrite(Texture2D texture2D)
		{
			if (texture2D != null)
			{
				var oldLogCount = logCount;

				texture2D.SetPixels32(texture2D.GetPixels32());

				return logCount == oldLogCount;
			}

			return false;
		}
	}
}
#endif