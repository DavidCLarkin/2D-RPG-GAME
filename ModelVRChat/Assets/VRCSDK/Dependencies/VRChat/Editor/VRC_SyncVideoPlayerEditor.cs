using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(VRCSDK2.VRC_SyncVideoPlayer.VideoEntry))]
public class CustomVideoEntryDrawer : PropertyDrawer
{    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty source = property.FindPropertyRelative("Source");
        SerializedProperty ratio = property.FindPropertyRelative("AspectRatio");
        SerializedProperty speed = property.FindPropertyRelative("PlaybackSpeed");
        SerializedProperty clip = property.FindPropertyRelative("VideoClip");
        SerializedProperty url = property.FindPropertyRelative("URL");

        return EditorGUI.GetPropertyHeight(source, new GUIContent("Source"), true) + EditorGUIUtility.standardVerticalSpacing
            + EditorGUI.GetPropertyHeight(ratio, new GUIContent("Aspect Ratio"), true) + EditorGUIUtility.standardVerticalSpacing
            + EditorGUI.GetPropertyHeight(speed, new GUIContent("Playback Speed"), true) + EditorGUIUtility.standardVerticalSpacing
            + Mathf.Max(EditorGUI.GetPropertyHeight(clip, new GUIContent("VideoClip"), true), EditorGUI.GetPropertyHeight(url, new GUIContent("URL"), true)) + EditorGUIUtility.standardVerticalSpacing;
    }

    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        SerializedProperty source = property.FindPropertyRelative("Source");
        SerializedProperty ratio = property.FindPropertyRelative("AspectRatio");
        SerializedProperty speed = property.FindPropertyRelative("PlaybackSpeed");
        SerializedProperty clip = property.FindPropertyRelative("VideoClip");
        SerializedProperty url = property.FindPropertyRelative("URL");

        EditorGUI.BeginProperty(rect, label, property);
        float x = rect.x;
        float y = rect.y;
        float w = rect.width;
        float h = EditorGUI.GetPropertyHeight(source, new GUIContent("Source"), true) + EditorGUIUtility.standardVerticalSpacing;
        VRCSDK2.VRC_EditorTools.FilteredEnumPopup<UnityEngine.Video.VideoSource>(new Rect(x, y, w, h), source, (e) => e == UnityEngine.Video.VideoSource.Url);
        y += h;

        if (source.enumValueIndex == (int)UnityEngine.Video.VideoSource.Url)
        {
            h = EditorGUI.GetPropertyHeight(url, new GUIContent("URL"), true) + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(new Rect(x, y, w, h), url);
            y += h;
        }
        else
        {
            h = EditorGUI.GetPropertyHeight(clip, new GUIContent("VideoClip"), true) + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(new Rect(x, y, w, h), clip);
            y += h;
        }

        h = EditorGUI.GetPropertyHeight(ratio, new GUIContent("AspectRatio"), true) + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField(new Rect(x, y, w, h), ratio);
        y += h;

        h = EditorGUI.GetPropertyHeight(ratio, new GUIContent("Playback Speed"), true) + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField(new Rect(x, y, w, h), speed);
        if (speed.floatValue == 0f)
            speed.floatValue = 1f;
        y += h;

        EditorGUI.EndProperty();
    }
}

[CustomEditor(typeof(VRCSDK2.VRC_SyncVideoPlayer))]
public class SyncVideoPlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SerializedProperty videos = serializedObject.FindProperty("Videos");

        videos.arraySize = EditorGUILayout.DelayedIntField("Playlist Count", videos.arraySize);

        if (videos.arraySize <= 0)
            return;

        for (int idx = 0; idx < videos.arraySize; ++idx)
        {
            EditorGUILayout.PropertyField(videos.GetArrayElementAtIndex(idx));
            EditorGUILayout.Space();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
