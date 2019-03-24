using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

using Overtime.FSM;

public class CreateState : Editor
{
	[MenuItem("Overtime/Finite State Machine/Create New States From Selection", false)]
	public static void CreateNewState()
	{
		Object[] scripts = Selection.objects;//.GetFiltered(typeof(MonoScript), SelectionMode.Assets);

		Debug.Log(string.Format("Trying to create {0} new ScriptableObjects...", scripts.Length));

		foreach(Object script in scripts)
		{
			MonoScript monoscript = script as MonoScript;

			if(monoscript != null)
			{
				if(monoscript.GetClass() != null && monoscript.GetClass().IsSubclassOf(typeof(ScriptableObject)) && !monoscript.GetClass().IsAbstract)
				{
					ScriptableObject asset = ScriptableObject.CreateInstance (monoscript.name);
					string path = string.Format ("Assets/{0}.asset", monoscript.name);
					AssetDatabase.CreateAsset (asset, path);
					EditorUtility.FocusProjectWindow ();
					Selection.activeObject = asset;

					Debug.Log("Created " + path);
				}
				else
				{
					Debug.LogError(string.Format("Script {0} does not inherit from ScriptableObject or is not Creatable (maybe abstract class?)", monoscript.name));
				}
			}
			else
			{
				Debug.LogError(string.Format("Object of type {0} is not a MonoScript", script.GetType()));
			}
		}
	}

	[MenuItem("Overtime/Finite State Machine/Create New States From Selection", true)]
	public static bool CreateNewStateValidate()
	{
		Object[] scripts = Selection.objects;//.GetFiltered(typeof(MonoScript), SelectionMode.Assets);

		foreach(Object script in scripts)
		{
			MonoScript monoscript = script as MonoScript;

			if(monoscript != null)
			{
				if(monoscript.GetClass() != null && monoscript.GetClass().IsSubclassOf(typeof(ScriptableObject))  && !monoscript.GetClass().IsAbstract)
				{
					return true;
				}
			}
		}

		return false;
	}
}