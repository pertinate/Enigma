using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Pertinate.Utils{
	public class KeyManager {
		public static Dictionary<string, KeyCode> keyCodes = new Dictionary<string, KeyCode>();
		
		public static void Init(){
			if(File.Exists(Application.dataPath + "/keys.xml"))
				keyCodes = Load(Application.dataPath + "/keys.xml");
			else
				SetDefault();
		}
		public static void SetDefault(){
			keyCodes.Add("dForward", KeyCode.W);
			keyCodes.Add("dBack", KeyCode.S);
			keyCodes.Add("dLeft", KeyCode.A);
			keyCodes.Add("dRight", KeyCode.D);
			keyCodes.Add("jump", KeyCode.Space);
			keyCodes.Add("crouch", KeyCode.LeftControl);
			keyCodes.Add("closeInterfaces", KeyCode.LeftAlt);
		}
		public static void Save(){
			Save(keyCodes, Application.dataPath + "/keys.xml");
		}
		public static Dictionary<string, KeyCode> Load(string filename){
			Dictionary<string, KeyCode> output = new Dictionary<string, KeyCode>();
			XmlDocument xd = new XmlDocument();
			xd.Load(XmlReader.Create(filename));
			foreach(XmlNode node in xd.DocumentElement){
				output.Add(node.Name, (KeyCode)Enum.Parse(typeof(KeyCode), node.InnerText));
			}
			return output;
		}
		public static KeyCode Get(string name) {
			KeyCode k;
			keyCodes.TryGetValue(name, out k);
			return k;
		}
		public static void Set(string name, KeyCode keyCode) {
			keyCodes[name] = keyCode;
		}
		public static void Save(Dictionary<string, KeyCode> dict, string filename){
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
	}
}