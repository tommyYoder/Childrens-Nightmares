using UnityEngine;
using System.Collections;
using UnityEditor;


namespace CompleteProject
{
    public class MakeWeaponObject
    {

        [MenuItem("Assets/Create/Weapon Object")]
        public static void Create()                                                //Allos you to create the weapon object in the inspector.
        {
            WeaponObject asset = ScriptableObject.CreateInstance<WeaponObject>();
            AssetDatabase.CreateAsset(asset, "Assets/NewWeaponObject.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}