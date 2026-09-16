using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Sumi
{
    [ScriptedImporter(1, "sui")]
    public class SuiImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            string text = System.IO.File.ReadAllText(ctx.assetPath);

            TextAsset asset = new TextAsset(text);

            ctx.AddObjectToAsset("SuiScript", asset);
            ctx.SetMainObject(asset);
        }
    }
}
