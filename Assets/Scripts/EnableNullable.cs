using System;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;

public class EnableNullable : AssetPostprocessor
{
    private static readonly string[] DisableNullableAssemblyNameArray = {
        "BattleScene.DataAccesses",
        "BattleScene.Debug",
        "BattleScene.Views"
    };

    /// <summary>
    /// UnityでNull許容参照型を利用可能にするために.csprojに追記を行うクラス。
    /// </summary>
    private static string OnGeneratedCSProject(string path, string content)
    {
        var document = XDocument.Parse(content);
        var root = document.Root ?? throw new InvalidOperationException();
        var xNamespace = root.GetDefaultNamespace();

        var assemblyName = root.Descendants(xNamespace + "AssemblyName").Single().Value;
        if (DisableNullableAssemblyNameArray.Contains(assemblyName)) return content;

        var propertyGroup = root.Descendants(xNamespace + "PropertyGroup").First();
        propertyGroup.Add(new XElement(xNamespace + "Nullable", "enable"));

        var noWarn = root.Descendants(xNamespace + "NoWarn").Single();
        noWarn.Value += ",8632";
        return document.ToString();
    }
}