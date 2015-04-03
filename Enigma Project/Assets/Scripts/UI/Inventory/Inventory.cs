using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Pertinate{
	public class Inventory : MonoBehaviour {
		public List<GameObject> allSlots;
		private static int emptySlot;
		private Slot from, to;

		public static int EmptySlot {
			get {return emptySlot;}
			set{emptySlot = value;}
		}
		public bool AddItem(Item item){
			if(item.maxStackSize == 1){
				PlaceEmpty(item);
				return true;
			} else {
				foreach(GameObject slot in allSlots){
					Slot tmp = slot.GetComponent<Slot>();
					if(tmp.isEmpty){
						if(tmp.CurrentItem.itemType == item.itemType && tmp.IsAvailable){
							tmp.AddItem(item);
							return true;
						}
					}
				}
				if(emptySlot > 0){
					PlaceEmpty(item);
				}
			}
			return false;
		}
		public void Start(){
			foreach(Slot s in GameObject.FindObjectsOfType<Slot>()){
				allSlots.Add(s.gameObject);
			}
			emptySlot = 25;
		}
		public void MoveItem(GameObject clicked){
			if(from == null){
				if(!clicked.GetComponent<Slot>().isEmpty){
					from = clicked.GetComponent<Slot>();
					from.GetComponent<Image>().color = Color.gray;
				}
			} else if(to == null){
				to = clicked.GetComponent<Slot>();
			}
			if(to != null && from != null){
				Stack<Item> tmpTo = new Stack<Item>(to.Items);
				to.AddItems(from.Items);
				if(tmpTo.Count == 0){

				}
			}
		}
		private bool PlaceEmpty(Item item){
			if(emptySlot > 0){
				foreach(GameObject slot in allSlots){
					Slot tmp = slot.GetComponent<Slot>();
					if(tmp.isEmpty){
						tmp.AddItem(item);
						emptySlot--;
						return true;
					}
				}
			}
			return false;
		}
	}
}