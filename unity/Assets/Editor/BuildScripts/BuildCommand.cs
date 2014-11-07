using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

public class BuildCommand : MonoBehaviour 
{

	[MenuItem("CustomMenu/Builds/PRO/Android")]
	public static void BuildGame ()
	{
		// Get filename.
//		string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
//		string[] levels = new string[] {"Assets/Scene1.unity", "Assets/Scene2.unity"};

//		BuildPipeline.BuildPlayer
		// Build player.


		string[] levels = {"Assets/Module/Scenes/Launcher.unity"};
		BuildPipeline.BuildPlayer(levels, "Sample.apk", BuildTarget.Android, BuildOptions.None);

		
		// Copy a file from the project folder to the build folder, alongside the built game.
//		FileUtil.CopyFileOrDirectory("Assets/WebPlayerTemplates/Readme.txt", path + "Readme.txt");
		
		// Run the game (Process class from System.Diagnostics).
//		Process proc = new Process();
//		proc.StartInfo.FileName = path + "BuiltGame.exe";
//		proc.Start();
	}
	
	[MenuItem("CustomMenu/Builds/Set Version")]
	public static void SetVersionNumber ()
	{
		string buildVersion = string.Empty;
		string[] listArg = System.Environment.GetCommandLineArgs();

		for (int i = 0; i < listArg.Length; i++) 
		{
			if(listArg[i] == "-BuildVersion")
			{
				if((i+1) < listArg.Length)
				{
					buildVersion = listArg[(i+1)];
				}
			}
		}

		if(buildVersion == string.Empty)
		{
			SelectVersionWindows.ShowWindow();
		}
		else
		{
			ForceChangeVersionNumber(buildVersion);
		}

	}

	public static void ForceChangeVersionNumber(string a_VersionNumber)
	{
		StreamReader reader = new StreamReader(ResourcesPathUtil.GLOBAL_CONST_PATH);
		string filedContent = reader.ReadToEnd();
		reader.Close();
		reader = null;

		string regex = "(?<BeforeVersion>(.)*VERSION_NUMBER(\\s)*(=)(\\s)*(\\\")+)(?<Version>([a-zA-Z0-9.]*))(?<AfterVersion>(\\\";)+(.)*)";
		string newValue = Regex.Replace(filedContent, regex, "${BeforeVersion}"+a_VersionNumber+"${AfterVersion}");

		StreamWriter writer = new StreamWriter(ResourcesPathUtil.GLOBAL_CONST_PATH);
		
		writer.Write(newValue);
		writer.Flush();
		writer.Close();

		AssetDatabase.Refresh();
	}
}
