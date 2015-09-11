using UnityEngine;
using System.Collections;

namespace Pertinate.Generation{
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	public class Grid : MonoBehaviour {
		public int xSize, ySize, zSize;
		private Vector3[] vertices;
		private Mesh mesh;
		public bool _Continue;

		private void Awake(){
			Generate();
		}
		public void Generate(){
			GetComponent<MeshFilter>().mesh = mesh = new Mesh();
			mesh.name = "Procedural Mesh";
			vertices = new Vector3[(xSize + 1) * (ySize + 1) * (zSize + 1)];
			for(int i = 0, y  = 0; y <= ySize; y++){
				for(int x = 0; x <= xSize; x++){
					for(int z = 0; z <= zSize; z++, i++){
						vertices[i] = new Vector3(x, y, z);
					}
				}
			}
			mesh.vertices = vertices;
			int[] triangles = new int[xSize * ySize *6];
			for(int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++){
				for(int x = 0; x < xSize; x++, vi++){
					for(int z = 0; z < zSize; z++, ti += 6, vi++){
						triangles[ti] = vi;
						triangles[ti + 3] = triangles[ti + 2] = vi + 1;
						triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
						triangles[ti + 5] = vi + xSize + 2;
					}
				}
			}
			mesh.triangles = triangles;
			mesh.RecalculateNormals();
		}
		private void OnDrawGizmos(){
			if(vertices == null){
				return;
			}
			Gizmos.color = Color.black;
			for(int i = 0; i < vertices.Length; i++){
				Gizmos.DrawSphere(vertices[i], 0.1f);
			}
		}
	}
}