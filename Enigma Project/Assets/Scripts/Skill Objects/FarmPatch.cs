using UnityEngine;
using System.Collections;

namespace Pertinate.Skills{
	public enum PatchType{
		Herb,
		Tree,
		FruitTree,
		Flower
	}
	public enum CropType{
		OakTree,
		MapleTree
	}
	public class FarmPatch : MonoBehaviour {
		private PatchType type;
		private CropType ctype;
		private bool isGrowing;
		private bool isEmpty;
		private int currentTick;
		private int totalTick;
		private float tickDistance;

		private void Awake(){
			type = PatchType.Tree;
			ctype = CropType.MapleTree;
		}
		private void Update(){
		}
		private IEnumerator GrowCrop(PatchType patch, CropType crop){
			while(isGrowing){
				switch(patch){
				case PatchType.Tree:
					switch(crop){
					case CropType.OakTree:
						break;
					case CropType.MapleTree:
						if(currentTick >= totalTick){
							isGrowing = false;
							yield return null;
						} else {
							this.isEmpty = false;
							currentTick++;
							yield return new WaitForSeconds(tickDistance);
						}
						break;
					}
					break;
				default:break;
				}
			}
		}
	}
}