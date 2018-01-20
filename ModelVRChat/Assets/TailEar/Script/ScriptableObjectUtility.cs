#if UNITY_EDITOR

//LAST UPDATED: 20140327

using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility
{
	public static void CreateAsset<T> ( string path ) where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T> ();
		
		if (path == "") 
		{
			path = "Assets";
		} 
		else if (Path.GetExtension (path) != "") 
		{
			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}
		
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(T).ToString() + ".asset");
		
		AssetDatabase.CreateAsset( asset, assetPathAndName );
		
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
}

#endif