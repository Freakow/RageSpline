// Alternative version, with redundant code removed
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class TransformInspector : Editor {

    public void OnSceneGUI() { }

    public override void OnInspectorGUI() {

        Transform t = (Transform)target;

        // Replicate the standard transform inspector gui
        //EditorGUIUtility.LookLikeControls();
		EditorGUIUtility.labelWidth = 0f;
		EditorGUIUtility.fieldWidth = 0f;
        EditorGUI.indentLevel = 0;
        Vector3 position = EditorGUILayout.Vector3Field("Position", t.localPosition);
        Vector3 eulerAngles = EditorGUILayout.Vector3Field("Rotation", t.localEulerAngles);
        Vector3 scale = EditorGUILayout.Vector3Field("Scale", t.localScale);

        if (GUI.changed) {
            //Undo.RegisterUndo(t, "Transform Change");
			Undo.RecordObject (t, "Transform Change");

            t.localPosition = FixIfNaN(position);
            t.localEulerAngles = FixIfNaN(eulerAngles);
            t.localScale = FixIfNaN(scale);
        }
    }

    private Vector3 FixIfNaN(Vector3 v) {
        if (float.IsNaN(v.x)) v.x = 0;
        if (float.IsNaN(v.y)) v.y = 0;
        if (float.IsNaN(v.z)) v.z = 0;
        return v;
    }

}
