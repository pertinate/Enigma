using UnityEngine;
using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Pertinate.Utils{
	public class XmlSerialization {
	}
	public class XmlSave{
		public static void SaveKeyCodeDictionary(Dictionary<string, KeyCode> dict, string filename){
			AssemblyName assName = new AssemblyName("CustomType");
			AssemblyBuilder assBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assName, AssemblyBuilderAccess.RunAndSave);
			ModuleBuilder modBuilder = assBuilder.DefineDynamicModule(assName.Name);
			TypeBuilder typeBuilder = modBuilder.DefineType("KeyCodes", TypeAttributes.Public);
			foreach(string key in dict.Keys){
				typeBuilder.DefineField(key, dict[key].GetType(), FieldAttributes.Public);
			}
			Type newType = typeBuilder.CreateType();
			object newInstance = Activator.CreateInstance(newType);
			foreach(string key in dict.Keys){
				newInstance.GetType().GetField(key).SetValue(newInstance, dict[key]);
			}

			XmlSerializer xs = new XmlSerializer(newType);
			TextWriter tw = new StreamWriter(filename);
			xs.Serialize(tw, newInstance);
			tw.Flush();
			tw.Close();
		}
		public static Dictionary<string, KeyCode> KeyCodeDeserialize(string filename){
			Dictionary<string, KeyCode> output = new Dictionary<string, KeyCode>();
			XmlDocument xd = new XmlDocument();
			xd.Load(XmlReader.Create(filename));
			foreach(XmlNode node in xd.DocumentElement){
				output.Add(node.Name, (KeyCode)Enum.Parse(typeof(KeyCode), node.InnerText));
			}
			return output;
		}
	}
}