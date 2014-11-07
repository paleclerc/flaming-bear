using UnityEngine;
using System.Collections;
using UnityEditor;

public class SelectVersionWindows : EditorWindow 
{
	public static void  ShowWindow () 
	{
		EditorWindow.GetWindow(typeof(SelectVersionWindows), false, "Select The Version Number");
	}

	private string m_VersionNumber = string.Empty;
	private bool m_FirstSetup = false;
	void OnGUI ()
	{
		if(!m_FirstSetup)
		{
			m_VersionNumber = GlobalConst.VERSION_NUMBER;
			m_FirstSetup = true;
		}

		GUILayout.BeginVertical();
		{
			GUILayout.Space(30);
			EditorGUILayout.LabelField("Current Version = "+GlobalConst.VERSION_NUMBER);
			GUILayout.Space(10);
			m_VersionNumber = EditorGUILayout.TextField("Version to use", m_VersionNumber);
			GUILayout.Space(50);

			GUILayout.BeginHorizontal();
			{

				if(GUILayout.Button("Cancel"))
				{
					this.Close();
				}

				GUILayout.Space(50);

				if(GUILayout.Button("Confirm"))
				{
					BuildCommand.ForceChangeVersionNumber(m_VersionNumber);
					this.Close();
				}
			}
		}	
	}
}

