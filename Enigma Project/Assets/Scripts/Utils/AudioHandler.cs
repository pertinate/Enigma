using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Pertinate{
	public enum SoundType{
		Master,
		Music,
		Voice,
		Ambient,
		Interface
	}
	public static class SoundTypeHandler{
		public static void SetVolume(this SoundType type, float volume){
			foreach(KeyValuePair<SoundType, GameObject> s in AudioHandler.Sources){
				if(s.Key == type){
					AudioSource c = AudioHandler.Sources[s.Key].GetComponent<AudioSource>();
					c.volume = volume;
				}
			}
		}
	}
	public class AudioHandler : MonoBehaviour {
		public static Dictionary<SoundType, GameObject> Sources = new Dictionary<SoundType, GameObject>();
		public static void CreateSource(){

		}
	}
}