using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace GameWork.Unity.Assets.Editor
{
	public static class GenerateLinkXml
	{
		private const string LinkFileName = "link.xml";

		private static string RelativeLinkXmlFile
		{
			get { return Paths.RelativeGameWorkFolder + "/" + LinkFileName; }
		}
		
		private static string AbsoluteLinkXmlFile
		{
			get { return Paths.AbsoluteGameWorkFolder + "/" + LinkFileName; }
		}

		public static void Generate()
		{
			var assemblyNames = GetAssemblyNames(Paths.AbsoluteGameWorkFolder);
			var linkerXmlDoc = GenerateLinkerXml(assemblyNames);
			linkerXmlDoc.Save(AbsoluteLinkXmlFile);

			AssetDatabase.ImportAsset(RelativeLinkXmlFile);

			Debug.Log("Generated link.xml saved to: " + AbsoluteLinkXmlFile);
		}

		private static XmlDocument GenerateLinkerXml(IEnumerable<string> assemblyNames)
		{
			var linkerDoc = new XmlDocument();
			var rootElement = linkerDoc.CreateElement("linker");
			linkerDoc.AppendChild(rootElement);

			foreach (var assemblyName in assemblyNames)
			{
				var assemblyElement = linkerDoc.CreateElement("assembly");
				assemblyElement.SetAttribute("fullname", assemblyName);
				assemblyElement.SetAttribute("preserve", "all");
				rootElement.AppendChild(assemblyElement);
			}

			return linkerDoc;
		}

		private static List<string> GetAssemblyNames(string folder)
		{
			var assemblyNames = new List<string>();

			foreach (var assemblyPath in Directory.GetFiles(folder, "*.dll", SearchOption.AllDirectories))
			{
				var assemblyName = AssemblyName.GetAssemblyName(assemblyPath);
				assemblyNames.Add(assemblyName.Name);
			}

			return assemblyNames;
		}
	}
}