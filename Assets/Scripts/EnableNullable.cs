using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.Experimental;
using UnityEditorInternal;

public class EnableNullable : AssetPostprocessor
{
    private static readonly string[] DisableNullableAssemblyNameList = {
        "DataAccess.BattleScene",
        "Debug.BattleScene",
        "Framework.BattleScene"
    };
        
    /// <summary>
    /// UnityでNull許容参照型を利用可能にするために.csprojに追記を行うクラス。以下の記事を参考にした。<br/>
    /// <see href="https://blog.kyubuns.dev/entry/2020/12/16/172335">Unity2020.2でnull許容参照型を使う</see>
    /// </summary>
    private static string OnGeneratedCSProject(string path, string content)
    {
        var assemblyName = Path.GetFileNameWithoutExtension(path);
        var assemblyDefinitionFilePath = CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName(assemblyName);
        if (string.IsNullOrWhiteSpace(assemblyDefinitionFilePath)) return content;

        var assemblyDefinition = EditorResources.Load<AssemblyDefinitionAsset>(assemblyDefinitionFilePath);
        if (DisableNullableAssemblyNameList
                .Any(x => assemblyDefinition.text.Contains($"\"name\": \"{x}\""))) return content;

        var document = XDocument.Parse(content);
        if (document.Root == null) throw new Exception($"document.Root == null");

        XNamespace xNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        // ReSharper disable once PossibleNullReferenceException
        var propertyGroup = document
            .Element(xNamespace + "Project")
            .Elements(xNamespace + "PropertyGroup")
            .First();
        propertyGroup.Add(new XElement(xNamespace + "Nullable", "enable"));
        return document.ToString();
    }
}