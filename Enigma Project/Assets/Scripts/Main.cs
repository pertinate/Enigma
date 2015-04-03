using UnityEngine;
using Pertinate.Utils;

namespace Pertinate{
	public class Main : MonoBehaviour {
		#region MainClass instance
		private static Main _instance;
		public static Main instance{
			get{return _instance;}
		}
		#endregion
		#region Scenes
		public static bool SplashScreen{
			get{if(Application.loadedLevel == 0)
					return true;
				else
					return false;
			}
		}
		public static bool MainMenu{
			get{if(Application.loadedLevel == 1)
					return true;
				else
					return false;
			}
		}
		public static bool LoadingScreen{
			get{if(Application.loadedLevel > 4)
					return true;
				else
					return false;
			}
		}
		public static bool PlayScene{
			get{if(Application.loadedLevel == 2 || Application.loadedLevel == 3 || Application.loadedLevel == 4)//first starting area, normal city, tree city
					return true;
				else
					return false;
			}
		}
		#endregion
		#region Player
		public static GameObject player{
			get{return GameObject.FindGameObjectWithTag("Player");}
		}
		public static Transform playerTransform{
			get{return player.transform;}
		}
		public static Vector3 playerPosition{
			get{return playerTransform.position;}
		}
		#endregion
		#region Initialization and Finalization
		private void Awake(){
			Init();
		}
		private void OnApplicationQuit(){
			Save();
		}
		#endregion
		#region Organization
		private void Init(){
			_instance = this;
			KeyManager.Init();
		}
		private void Save(){
			KeyManager.Save();
		}
		#endregion
	}
}