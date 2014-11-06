
using UnityEngine;
using UnityEditor;
using System.IO;

public static class MenuUtil
{
	public static void CreateAsset<T>() where T : ScriptableObject
	{
		CreateAsset<T>(null, null);
	}

	public static void CreateAsset<T>(string a_Path, string a_Name) where T : ScriptableObject
	{
		T obj = ScriptableObject.CreateInstance<T>();
		
		string path = a_Path;
		if (string.IsNullOrEmpty(path))
		{
			if (Selection.objects != null && Selection.objects.Length > 0)
			{
				path = AssetDatabase.GetAssetPath(Selection.objects[0]);
			}
			if (string.IsNullOrEmpty(path))
			{
				path = "Assets";
			}
			else if (!Directory.Exists(path))
			{
				path = Path.GetDirectoryName(path);
				if (!Directory.Exists(path))
				{
					path = "Assets";
				}
			}
		}
		
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		
		path = path + "/" + (string.IsNullOrEmpty(a_Name)?typeof(T).Name:a_Name);
		string finalPath = path + ".asset";
		int i = 1;
		while (AssetDatabase.LoadAssetAtPath(finalPath, typeof(T)) != null)
		{
			finalPath = string.Format("{0} {1}{2}", path, i++, ".asset");
		}
		AssetDatabase.CreateAsset(obj, finalPath);
		Selection.activeObject = obj;
	}
}
