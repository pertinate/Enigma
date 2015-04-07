using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Pertinate.Settings{
	public class Settings : MonoBehaviour {
		public static float masterVolume = 100f;
		public static float musicVolume = 100f;
		public static float voiceVolume = 100f;
		public static float effectVolume = 100f;
		public static float interfaceVolume = 100f;
		public static float ambientVolume = 100f;
		private void Awake(){

		}
		private void Start(){

		}
		private void OnApplicationQuit(){
			//SettingsContainer.IniWriteValue("", "", );
			SettingsContainer.IniWriteValue("Video", "Resolution Height", Screen.currentResolution.height.ToString());
			SettingsContainer.IniWriteValue("Video", "Resolution Width", Screen.currentResolution.width.ToString());
			SettingsContainer.IniWriteValue("Volume", "Master", masterVolume.ToString());
			SettingsContainer.IniWriteValue("Volume", "Music", musicVolume.ToString());
			SettingsContainer.IniWriteValue("Volume", "Voice", voiceVolume.ToString());
			SettingsContainer.IniWriteValue("Volume", "Effect", effectVolume.ToString());
			SettingsContainer.IniWriteValue("Volume", "Interface", interfaceVolume.ToString());
			SettingsContainer.IniWriteValue("Volume", "Ambient", ambientVolume.ToString());
		}
	}
	public enum Sections{
		Section01,
	}
	public enum Keys{
		Key01,
		Key02,
		Key03
	}
	public static class SettingsContainer{
		private static string path = Path.Combine(Application.dataPath, "Settings.ini");
		private static Dictionary<string, Dictionary<string, string>> IniDict = new Dictionary<string, Dictionary<string, string>>();
		private static bool Initialized = false;
		private static bool firstRead(){
			if(File.Exists(path)){
				using(StreamReader sr = new StreamReader(path)){
					string line;
					string theSection = "";
					string theKey = "";
					string theValue = "";
					while(!string.IsNullOrEmpty(line = sr.ReadLine())){
						line.Trim();
						if(line.StartsWith("[") && line.EndsWith("]")){
							theSection = line.Substring(1, line.Length - 2);
						} else {
							string[] ln = line.Split(new char[] { '=' });
							theKey = ln[0].Trim();
							theValue = ln[1].Trim();
						}
						if(theSection == "" || theKey == "" || theValue == "")
							continue;
						PopulateIni(theSection, theKey, theValue);
					}
				}
			}
			return true;
		}
		private static void PopulateIni(string _section, string _key, string _value){
			if(IniDict.Keys.Contains(_section)){
				if(IniDict[_section].Keys.Contains(_key))
					IniDict[_section][_key] = _value;
				else
					IniDict[_section].Add(_key, _value);
			} else {
				Dictionary<string, string> neuVal = new Dictionary<string, string>();
				neuVal.Add (_key.ToString(), _value);
				IniDict.Add(_section.ToString(), neuVal);
			}
		}
		public static void IniWriteValue(string _section, string _key, string _value){
			if(!Initialized)
				firstRead();
			File.Delete(path);
			PopulateIni(_section, _key, _value);
			WriteIni();
		}
		public static void IniWriteValue(Sections _section, Keys _key, string _value){
			IniWriteValue(_section.ToString(), _key.ToString(), _value);
		}
		private static void WriteIni(){
			using(StreamWriter sw = new StreamWriter(path)){
				foreach(KeyValuePair<string, Dictionary<string, string>> sezioni in IniDict){
					sw.WriteLine("[" + sezioni.Key.ToString() + "]");
					foreach(KeyValuePair<string, string> chiave in sezioni.Value){
						string value = chiave.Value.ToString();
						value = value.Replace(Environment.NewLine, " ");
						value = value.Replace("\r\n", " ");
						sw.WriteLine(chiave.Key.ToString() + " = " + value);
					}
				}
			}
		}
		public static string IniReadValue(Sections _Section, Keys _Key)
		{
			if (!Initialized)
				firstRead();
			return IniReadValue(_Section.ToString(), _Key.ToString());
		}
		public static string IniReadValue(string _Section, string _Key)
		{
			if (!Initialized)
				firstRead();
			if (IniDict.ContainsKey(_Section))
				if (IniDict[_Section].ContainsKey(_Key))
					return IniDict[_Section][_Key];
			return null;
		}
	}
}