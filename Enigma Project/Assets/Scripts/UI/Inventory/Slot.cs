using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Pertinate{
	public class Slot : MonoBehaviour, IPointerClickHandler {
		private Stack<Item> items = new Stack<Item>();

		public Stack<Item> Items {
			get {
				return items;
			}
			set {
				items = value;
			}
		}

		public Text stackText;
		public Sprite emptySprite;
		public bool isEmpty{
			get{return items.Count == 0;}
		}
		public bool IsAvailable{
			get{return CurrentItem.maxStackSize > items.Count;}
		}
		public Item CurrentItem{
			get{return items.Peek();}
		}
		public void Awake(){
			emptySprite = GetComponent<Image>().sprite;
		}
		public void Start(){
			items = new Stack<Item>();
			for(int i = 0; i < Random.Range(0, 50); i++){
				AddItem(ItemDatabase.instance.itemList[Random.Range(0, 1)]);
			}
		}
		private void Update(){
			if(items.Count == 1)
				stackText.text = string.Empty;
		}
		public void AddItem(Item item){
			items.Push(item);
			if(items.Count >= 0){
				stackText.text = items.Count.ToString();
			}
			ChangeSprite(item.itemIcon);
		}
		public void AddItems(Stack<Item> items){
			this.items = new Stack<Item>(items);
			ChangeSprite(CurrentItem.itemIcon);
		}
		public void ChangeSprite(Sprite Item){
			transform.GetChild(0).GetComponent<Image>().enabled = true;
			transform.GetChild(0).GetComponent<Image>().sprite = Item;
		}
		public void UseItem(){
			if(!isEmpty){
				items.Pop().Use();
				stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
				if(isEmpty){
					transform.GetChild(0).GetComponent<Image>().enabled = false;
					Inventory.EmptySlot++;
				}
			}
		}
		public void ClearSlot(){
			items.Clear();
			transform.GetChild(0).GetComponent<Image>().enabled = false;
		}
		public void OnPointerClick (PointerEventData eventData){
			if(eventData.button == PointerEventData.InputButton.Right){
				UseItem();
			}
		}
	}
}