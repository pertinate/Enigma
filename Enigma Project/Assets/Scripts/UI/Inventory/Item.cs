using UnityEngine;
using System;

namespace Pertinate{
	public enum ItemType{
		MainHand,
		OffHand,
		Chest,
		Helm,
		Amulet,
		Ring,
		Trinket,
		Cape,
		Gloves,
		Belt,
		General,
		Quest,
		Pants
	}
	public enum ItemRarity{
		None,
		Normal,
		Magical,
		Rare,
		Unique,
		Set
	}
	public static class ItemRarityExtensions{
		public static void CreateAttributes(this ItemRarity e){
			switch(e){
			case ItemRarity.Normal:
				Debug.Log("Creating normal item.");
				break;
			case ItemRarity.Magical:
				Debug.Log("Creating magical item.");
				break;
			case ItemRarity.Rare:
				break;
			case ItemRarity.Unique:
				break;
			case ItemRarity.Set:
				break;
			default:break;
			}
		}
	}
	public enum ItemAttributes{

	}
	[System.Serializable]
	public class Item {
		public string itemName;
		public int itemID;
		public Sprite itemIcon;
		public GameObject itemModel;
		public ItemType itemType;
		public ItemRarity itemRarity;
		public bool isStackable;
		public int maxStackSize;

		public Item(string name, int id, ItemType type, bool stack, Sprite icon, int stackSize = 1){
			itemName = name;
			itemID = id;
			itemType = type;
			isStackable = stack;
			itemIcon = icon;
			maxStackSize = stackSize;
			switch(itemRarity){
			case ItemRarity.Normal:
				ItemRarity.Normal.CreateAttributes();
				break;
			case ItemRarity.Magical:
				ItemRarity.Magical.CreateAttributes();
				break;
			}
		}
		public void Use(){
			switch(itemType){

			}
		}
	}
}