using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

namespace EditorTools
{
    [InitializeOnLoad]
    public class ScriptNamespaceEditor : AssetModificationProcessor
    {
        private static string currentFolderPath;

        static ScriptNamespaceEditor()
        {
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        private static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
        {
            // Получаем путь к текущей выделенной папке
            if (Selection.activeObject != null)
            {
                string path = AssetDatabase.GetAssetPath(Selection.activeObject);
                if (Directory.Exists(path))
                {
                    currentFolderPath = path;
                }
            }
        }

        private static void OnWillCreateAsset(string assetPath)
        {
            if (!assetPath.EndsWith(".cs.meta"))
                return;

            string actualAssetPath = assetPath.Replace(".meta", "");
            string fileContent = File.ReadAllText(actualAssetPath);

            // Получаем namespace из пути папки
            string namespaceName = GetNamespaceFromPath(actualAssetPath);

            if (!string.IsNullOrEmpty(namespaceName) && !fileContent.Contains("namespace"))
            {
                // Добавляем namespace в скрипт
                string newContent = AddNamespaceToScript(fileContent, namespaceName);
                File.WriteAllText(actualAssetPath, newContent);
                AssetDatabase.Refresh();
            }
        }

        private static string GetNamespaceFromPath(string path)
        {
            string relativePath = path.Replace("Assets/", "").Replace(".cs", "");
            string[] folders = relativePath.Split('/');

            string namespaceName = "";
            for (int i = 0; i < folders.Length - 1; i++) // -1 чтобы исключить файл
            {
                if (!string.IsNullOrEmpty(folders[i]))
                {
                    // Пропускаем папку "Scripts"
                    if (folders[i].Equals("Scripts", System.StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (!string.IsNullOrEmpty(namespaceName))
                        namespaceName += ".";
                    namespaceName += FormatNamespacePart(folders[i]);
                }
            }

            return namespaceName;
        }

        private static string FormatNamespacePart(string folderName)
        {
            // Убираем специальные символы и делаем CamelCase
            string formatted = Regex.Replace(folderName, @"[^a-zA-Z0-9_]", "");
            if (formatted.Length > 0)
            {
                formatted = char.ToUpper(formatted[0]) + formatted.Substring(1);
            }
            return formatted;
        }

        private static string AddNamespaceToScript(string content, string namespaceName)
        {
            // Находим индекс начала класса
            int classIndex = content.IndexOf("public class");
            if (classIndex == -1)
                return content;

            // Находим последний using (конец импортов)
            int lastUsingIndex = content.LastIndexOf("using");
            int endOfImportsIndex = 0;

            if (lastUsingIndex != -1)
            {
                // Находим конец последнего using (после ;)
                endOfImportsIndex = content.IndexOf(';', lastUsingIndex) + 1;
                if (endOfImportsIndex == 0) endOfImportsIndex = lastUsingIndex;
            }

            // Разделяем содержимое на части
            string importsPart = content.Substring(0, endOfImportsIndex).Trim();
            string classPart = content.Substring(endOfImportsIndex).Trim();

            // Форматируем с правильными отступами
            string formattedContent = $@"{importsPart}

namespace {namespaceName}
{{
{AddIndentation(classPart, 4)}
}}";

            return formattedContent;
        }

        // Вспомогательный метод для добавления отступов
        private static string AddIndentation(string text, int indentLevel)
        {
            string indent = new string(' ', indentLevel);
            string[] lines = text.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                if (!string.IsNullOrEmpty(lines[i].Trim()))
                {
                    lines[i] = indent + lines[i];
                }
            }

            return string.Join("\n", lines);
        }
    }
}