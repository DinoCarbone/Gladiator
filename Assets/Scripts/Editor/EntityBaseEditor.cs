using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Data.ScriptableObjects.States;

namespace Editor
{
    /// <summary>
    /// Определяет нужные поля для состояний и провайдеров сущности,
    /// вместо абстрактрых списов game objects
    /// </summary>
    [CustomEditor(typeof(Core.Behaviors.Entities.EntityBase))]
    public class EntityBaseEditor : UnityEditor.Editor
    {
        private bool entityDatasFoldout = true;
        private bool providersSOFoldout = true;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspectorExcept("entityDatas", "providersSO");

            DrawEntityDatas();
            DrawProvidersSO();

            serializedObject.ApplyModifiedProperties();
        }

        void DrawDefaultInspectorExcept(params string[] exclude)
        {
            var prop = serializedObject.GetIterator();
            prop.NextVisible(true);
            while (prop.NextVisible(false))
            {
                if (exclude.Contains(prop.name)) continue;
                EditorGUILayout.PropertyField(prop, true);
            }
        }

        void DrawEntityDatas()
        {
            var listProp = serializedObject.FindProperty("entityDatas");
            if (listProp == null) return;

            GUILayout.Space(8);
            EditorGUILayout.BeginHorizontal();
            entityDatasFoldout = EditorGUILayout.Foldout(entityDatasFoldout, "Entity Data Parts", true);
            EditorGUILayout.EndHorizontal();

            if (entityDatasFoldout)
            {
                for (int i = 0; i < listProp.arraySize; i++)
                {
                    var element = listProp.GetArrayElementAtIndex(i);
                    var soProp = element.FindPropertyRelative("behaviorSO");
                    var contextsProp = element.FindPropertyRelative("contexts");

                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField($"Element {i}", EditorStyles.miniBoldLabel);
                    if (GUILayout.Button("-", GUILayout.Width(24)))
                    {
                        listProp.DeleteArrayElementAtIndex(i);
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                        break;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.PropertyField(soProp);

                    var so = soProp.objectReferenceValue as BaseBehaviorSO;
                    var reqs = so?.GetContextRequirements();
                    if (reqs != null && reqs.Length > 0)
                    {
                        if (contextsProp != null) EnsureListSize(contextsProp, reqs.Length);
                        for (int r = 0; r < reqs.Length; r++)
                        {
                            var req = reqs[r];
                            var elem = contextsProp.GetArrayElementAtIndex(r);
                            Type requiredType = !string.IsNullOrEmpty(req.typeName) ? Type.GetType(req.typeName) : typeof(GameObject);
                            if (requiredType == null) requiredType = typeof(GameObject);
                            EditorGUI.BeginChangeCheck();

                            if (typeof(Component).IsAssignableFrom(requiredType) && requiredType != typeof(GameObject))
                            {
                                Component currentComp = null;
                                var storedGo = elem.objectReferenceValue as GameObject;
                                if (storedGo != null) currentComp = storedGo.GetComponent(requiredType) as Component;

                                var label = !string.IsNullOrEmpty(req.displayName) ? req.displayName : (req.typeName ?? requiredType.Name);
                                var newComp = EditorGUILayout.ObjectField(label, currentComp, requiredType, true) as Component;
                                if (EditorGUI.EndChangeCheck())
                                {
                                    elem.objectReferenceValue = newComp != null ? newComp.gameObject : null;
                                }
                            }
                            else
                            {
                                var label = !string.IsNullOrEmpty(req.displayName) ? req.displayName : (req.typeName ?? requiredType.Name);
                                UnityEngine.Object newObj = EditorGUILayout.ObjectField(label, elem.objectReferenceValue, requiredType, true);
                                if (EditorGUI.EndChangeCheck()) elem.objectReferenceValue = newObj;
                            }
                        }
                    }
                    else
                    {
                        EditorGUILayout.LabelField("No contexts required", EditorStyles.miniLabel);
                    }

                    EditorGUILayout.EndVertical();
                }

                // add button at bottom of entityDatas
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("+", GUILayout.Width(24)))
                {
                    listProp.InsertArrayElementAtIndex(listProp.arraySize);
                }
                EditorGUILayout.EndHorizontal();
            }

            // spacing between lists
            GUILayout.Space(8);
        }

        void DrawProvidersSO()
        {
            var listProp = serializedObject.FindProperty("providersSO");
            if (listProp == null) return;

            EditorGUILayout.BeginHorizontal();
            providersSOFoldout = EditorGUILayout.Foldout(providersSOFoldout, "Provider Data Parts", true);
            EditorGUILayout.EndHorizontal();

            if (providersSOFoldout)
            {
                for (int i = 0; i < listProp.arraySize; i++)
                {
                    var element = listProp.GetArrayElementAtIndex(i);
                    var soProp = element.FindPropertyRelative("providerSO");
                    var contextsProp = element.FindPropertyRelative("contexts");

                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField($"Element {i}", EditorStyles.miniBoldLabel);
                    if (GUILayout.Button("-", GUILayout.Width(24)))
                    {
                        listProp.DeleteArrayElementAtIndex(i);
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                        break;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.PropertyField(soProp);

                    var so = soProp.objectReferenceValue as Data.ScriptableObjects.Providers.BaseProviderSO;
                    var reqs = so?.GetContextRequirements();
                    if (reqs != null && reqs.Length > 0)
                    {
                        if (contextsProp != null) EnsureListSize(contextsProp, reqs.Length);
                        for (int r = 0; r < reqs.Length; r++)
                        {
                            var req = reqs[r];
                            var elem = contextsProp.GetArrayElementAtIndex(r);
                            Type requiredType = !string.IsNullOrEmpty(req.typeName) ? Type.GetType(req.typeName) : typeof(GameObject);
                            if (requiredType == null) requiredType = typeof(GameObject);
                            EditorGUI.BeginChangeCheck();

                            if (typeof(Component).IsAssignableFrom(requiredType) && requiredType != typeof(GameObject))
                            {
                                Component currentComp = null;
                                var storedGo = elem.objectReferenceValue as GameObject;
                                if (storedGo != null) currentComp = storedGo.GetComponent(requiredType) as Component;

                                var label = !string.IsNullOrEmpty(req.displayName) ? req.displayName : (req.typeName ?? requiredType.Name);
                                var newComp = EditorGUILayout.ObjectField(label, currentComp, requiredType, true) as Component;
                                if (EditorGUI.EndChangeCheck())
                                {
                                    elem.objectReferenceValue = newComp != null ? newComp.gameObject : null;
                                }
                            }
                            else
                            {
                                var label = !string.IsNullOrEmpty(req.displayName) ? req.displayName : (req.typeName ?? requiredType.Name);
                                UnityEngine.Object newObj = EditorGUILayout.ObjectField(label, elem.objectReferenceValue, requiredType, true);
                                if (EditorGUI.EndChangeCheck()) elem.objectReferenceValue = newObj;
                            }
                        }
                    }
                    else
                    {
                        EditorGUILayout.LabelField("No contexts required", EditorStyles.miniLabel);
                    }

                    EditorGUILayout.EndVertical();
                }

                // add button at bottom of providersSO
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("+", GUILayout.Width(24)))
                {
                    listProp.InsertArrayElementAtIndex(listProp.arraySize);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private static void EnsureListSize(SerializedProperty listProp, int size)
        {
            if (listProp == null) return;
            if (!listProp.isArray) return;

            while (listProp.arraySize < size) listProp.InsertArrayElementAtIndex(listProp.arraySize);
            while (listProp.arraySize > size) listProp.DeleteArrayElementAtIndex(listProp.arraySize - 1);
        }
    }
}