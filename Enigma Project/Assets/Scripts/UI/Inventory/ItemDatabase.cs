using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pertinate{
	public class ItemDatabase : MonoBehaviour {
		public static ItemDatabase instance;
		public Item[] itemList;
		private void Awake(){
			instance = this;
		}
	}
}