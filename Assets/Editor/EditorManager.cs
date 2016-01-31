using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class EditorManager : Editor
{
	[MenuItem("Vortexel/Scene/Homeport")]	public static void OpenHomeport()	{ OpenScene("Homeport");}	
	[MenuItem("Vortexel/Scene/Battle 1")]	public static void OpenBattle1()	{ OpenScene("Battle_01");}
	[MenuItem("Vortexel/Scene/Battle 2")]	public static void OpenBattle2()	{ OpenScene("Battle_02");}
	[MenuItem("Vortexel/Scene/Battle 3")]	public static void OpenBattle3()	{ OpenScene("Battle_03");}
	
	public static void OpenScene(string sceneName)
	{
		EditorApplication.isPaused = false;
		EditorApplication.isPlaying = false;

		if (SceneManager.GetActiveScene().isDirty)
		{
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		}

		EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity");
	}

	[MenuItem("Vortexel/Prefab/Selection")]
	public static void PrefabSelection()
	{
		const string PrefabLocation = "Assets/Prefabs/";
		foreach(var o in Selection.objects)
		{
			var s = PrefabLocation + o.name + ".prefab";
			PrefabUtility.CreatePrefab(s,o as GameObject);
		}
	}

	[MenuItem("Vortexel/Editor/Render/OpenGL")]
	public static void OpenOGL()
	{
		var activeProject = Application.dataPath.Replace("Assets","");
		EditorApplication.OpenProject(activeProject, "-force-opengl", "-projectPath " + activeProject);
	}

	[MenuItem("Vortexel/Editor/Render/DirectX 9")]
	public static void OpenDX9()
	{
		var activeProject = Application.dataPath.Replace("Assets","");
		EditorApplication.OpenProject(activeProject, "-force-d3d9", "-projectPath " + activeProject);
	}
	
	[MenuItem("Vortexel/Editor/Render/DirectX 11")]
	public static void OpenDX11()
	{
		var activeProject = Application.dataPath.Replace("Assets","");
		EditorApplication.OpenProject(activeProject, "-force-d3d11", "-projectPath " + activeProject);
	}

	[MenuItem("Vortexel/Editor/Clear/Prefs")]
	public static void ClearPrefs()
	{
		PlayerPrefs.DeleteAll();
	}

	[MenuItem("Vortexel/Save/Location")]
	public static void OpenSaveLocation()
	{
		var startInfo = new ProcessStartInfo();
		startInfo.UseShellExecute = true;
		startInfo.FileName = Application.persistentDataPath;
		startInfo.Verb = "open";

		Process.Start(startInfo);
	}
	
	[MenuItem ("Vortexel/Select All of same tag")]
	static void SelectSameTag() 
	{
		GameObject go = Selection.activeGameObject;
		GameObject[] gos = GameObject.FindGameObjectsWithTag(go.transform.tag);
		Selection.objects = gos;
	}

    [MenuItem("Vortexel/Materials/Check")]
    static void CheckSelections()
    {
        GameObject[] objs = Selection.gameObjects;
        foreach (var o in objs)
        {
            Debug.Log(o.name);
        }
    }

    [MenuItem("Vortexel/Materials/Copy From First")]
    static void CopyFirst()
    {
        var objs = Selection.gameObjects;

        if (objs.Length != 2)
        {
            Debug.LogError("You need to select two objects to matMerge");
            return;
        }

        var renderers1 = objs[0].GetComponentsInChildren<Renderer>(true);
        var keyRenderers1 = renderers1.ToDictionary(r => r.gameObject.name);

        var renderers2 = objs[1].GetComponentsInChildren<Renderer>(true);
        var keyRenderers2 = renderers2.ToDictionary(r => r.gameObject.name);

        for (int i = 0; i < keyRenderers1.Count; ++i)
        {
            var rend1 = keyRenderers1.ElementAt(i);

            if (keyRenderers2.ContainsKey(rend1.Key) == false)
                continue;
            
            var rend2 = keyRenderers2[rend1.Key];
            if (rend2 != null)
            {
                // TODO Add some form of count, to check if the submeshs are the same.
                rend2.sharedMaterials = rend1.Value.sharedMaterials;
            }
        }
    }

    // TODO remove dupe function and replace with dynamic function for both this and above
    [MenuItem("Vortexel/Materials/Copy From Last")]
    static void CopyLast()
    {
        var objs = Selection.gameObjects;

        if (objs.Length != 2)
        {
            Debug.LogError("You need to select two objects to matMerge");
            return;
        }

        var renderers1 = objs[0].GetComponentsInChildren<Renderer>(true);
        var keyRenderers1 = renderers1.ToDictionary(r => r.gameObject.name);

        var renderers2 = objs[1].GetComponentsInChildren<Renderer>(true);
        var keyRenderers2 = renderers2.ToDictionary(r => r.gameObject.name);

        for (var i = 0; i < keyRenderers2.Count; ++i)
        {
            var rend1 = keyRenderers2.ElementAt(i);

            if (keyRenderers1.ContainsKey(rend1.Key) == false)
                continue;

            var rend2 = keyRenderers1[rend1.Key];
            if (rend2 != null)
            {
                // TODO Add some form of count, to check if the submeshs are the same.
                rend2.sharedMaterials = rend1.Value.sharedMaterials;
            }
        }
    }

    [MenuItem("Vortexel/Renderer/Wireframe/Hide")]
    static void HideWireframe()
    {
        var objs = Selection.gameObjects;
        foreach (var obj in objs)
        {
            var rend = obj.GetComponent<Renderer>();
            if (rend != null)
            {
                EditorUtility.SetSelectedWireframeHidden(rend, true);
            }
        }
    }

    [MenuItem("Vortexel/Renderer/Wireframe/Show")]
    static void ShowWireframe()
    {
        var objs = Selection.gameObjects;
        foreach (var obj in objs)
        {
            var rend = obj.GetComponent<Renderer>();
            if (rend != null)
            {
                EditorUtility.SetSelectedWireframeHidden(rend, false);
            }
        }
    }
	
	[MenuItem("Vortexel/Build/Build NAS")]
	public static void BuildNas()
	{
		Build(false);
	}

    [MenuItem("Vortexel/Build/Build NAS(Open Folder)")]
    public static void BuildNasOpen()
    {
        Build(true);
    }

    [MenuItem("Vortexel/Build/Open Folder")]
    public static void OpenBuildFolder()
    {
        var path = "Z:/Builds/" + PlayerSettings.productName + "/Latest/";
        var proc = new Process();
        proc.StartInfo.FileName = path;
        proc.Start();
    }
	
	static string Build(bool openFolder = false)
	{
        var path = "Z:/Builds/" + PlayerSettings.productName + "/Latest/";
	    var product = path + PlayerSettings.productName + ".exe";
        var scenes = EditorBuildSettings.scenes;
		var levels = new string[scenes.Length];
		for(var i = 0; i < scenes.Length; ++i)
		{
			levels[i] = scenes[i].path;
		}
		
		// Build player.
        BuildPipeline.BuildPlayer(levels, product, BuildTarget.StandaloneWindows64, BuildOptions.None);
        Debug.Log("Build TO: " + product);

	    if (openFolder)
	    {
	        var proc = new Process();
	        proc.StartInfo.FileName = path;
	        proc.Start();
	    }

	    return path;
	}

	[MenuItem("Vortexel/Build/Run")]
	public static void RunX1()
	{
		var path = Build();

		// Run the game (Process class from System.Diagnostics).
		var proc = new Process();
		proc.StartInfo.FileName = path;
		
		proc.Start();
	}

	[MenuItem("Vortexel/Build/Run x2")]
	public static void RunX2()
	{
        var path = Build();

		// Run the game (Process class from System.Diagnostics).
		var proc = new Process();
		proc.StartInfo.FileName = path;

		proc.Start();
		proc.Start();
	}

	[MenuItem("Vortexel/Build/Run x3")]
	public static void RunX3()
	{
        var path = Build();
		
		// Run the game (Process class from System.Diagnostics).
		var proc = new Process();
		proc.StartInfo.FileName = path;
		
		proc.Start();
		proc.Start();
		proc.Start();
	}

	[MenuItem("Vortexel/Build/Run x4")]
	public static void RunX4()
	{
        var path = Build();
		
		// Run the game (Process class from System.Diagnostics).
		var proc = new Process();
		proc.StartInfo.FileName = path;
		
		proc.Start();
		proc.Start();
		proc.Start();
		proc.Start();
	}

    [MenuItem("Vortexel/Render/Cubemap")]
    public static void Rendercubemap()
    {
        var obj = Selection.activeGameObject;
        if (obj == null)
            return;

        Cubemap cubemap = new Cubemap(1024,TextureFormat.ARGB32, false);

        // create temporary camera for rendering
        GameObject go = new GameObject("CubemapCamera");
        go.AddComponent<Camera>();
        // place it on the object
        go.transform.position = obj.transform.position;
        go.transform.rotation = Quaternion.identity;
        // render into cubemap		
        if (go.GetComponent<Camera>().RenderToCubemap(cubemap))
        {
            AssetDatabase.CreateAsset(cubemap, "Assets/cubemap.cubemap");
        }
        else
        {
            Debug.LogError("Something went wrong...");
        }
        // destroy temporary camera
        DestroyImmediate(go);
    }
}
