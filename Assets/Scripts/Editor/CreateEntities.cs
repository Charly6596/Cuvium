using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using System.IO;
using Cuvium.Core;

namespace Cuvium.EditorExtension
{
    public class CreateEntities : EditorWindow
    {

        public string ResourcesFolder = "Resources";
        public string EntityName;
        public string EntityPath;
        private int CurrentSelection = 0;
        private int CachedSelection = 0;
        private Type[] entitiesCache;
        private Type[] EntitiesCache
        {
            get
            {
                if(EntitiesNeedRefresh || entitiesCache is null)
                {
                    entitiesCache = LoadEntities();
                    EntitiesNeedRefresh = false;
                }
                return entitiesCache;
            }
            set
            {
                entitiesCache = value;
            }
        }
        private bool EntitiesNeedRefresh = true;
        private bool EditorNeedRefresh = true;
        private Editor editorCache;
        private Editor EditorCache
        {
            get
            {
                if(EditorNeedRefresh || editorCache is null)
                {
                    editorCache = CreateEditor();
                    EditorNeedRefresh = false;
                }
                return editorCache;
            }
            set
            {
                editorCache = value;
            }
        }

        [MenuItem("Cuvium/Entities")]
        public static void ShowWindow()
        {
            GetWindow<CreateEntities>("CreateEntities");
        }

        void OnEnable()
        {
            EntitiesCache = LoadEntities();
            EditorCache = CreateEditor();
            EntitiesNeedRefresh = false;
            EditorNeedRefresh = false;
        }

        void OnGUI()
        {
            if(CachedSelection != CurrentSelection)
            {
                RefreshEditor();
                CachedSelection = CurrentSelection;
            }

            CurrentSelection = EditorGUILayout.Popup("Entity", CurrentSelection, EntitiesCache.Select(c => c.Name).ToArray());
            ResourcesFolder = EditorGUILayout.TextField("Resources folder", ResourcesFolder);
            EntityName = EditorGUILayout.TextField("Entity Name", EntityName);
            EditorCache.OnInspectorGUI();
            var save = GUILayout.Button("Create entity");
            if(save)
            {
                SaveButton();
            }
        }

        private void SaveButton()
        {
            var path = String.Format("Assets/{0}/{1}", ResourcesFolder, EntityPath);
            Directory.CreateDirectory(path);
            var assetPath = AssetDatabase.GenerateUniqueAssetPath(path + "/" + EntityName + ".asset");
            AssetDatabase.CreateAsset(EditorCache.serializedObject.targetObject, assetPath);
            EditorNeedRefresh = true;
            EntitiesNeedRefresh = true;
        }

        private void RefreshEditor()
        {
            EditorNeedRefresh = true;
        }

        private Editor CreateEditor()
        {
            EntityName = EntitiesCache[CurrentSelection].Name;
            EntityPath = EntityName;
            var so = ScriptableObject.CreateInstance(EntityName);
            var editor = Editor.CreateEditor(so);
            return editor;
        }

        private Type[] LoadEntities()
        {
            return Assembly
                .GetAssembly(typeof(CuviumModel))
                .GetTypes()
                .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(CuviumModel)))
                .ToArray();
        }
    }
}

