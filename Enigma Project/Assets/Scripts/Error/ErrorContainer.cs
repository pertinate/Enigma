using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace Pertinate.Error{
    public class ErrorContainer : MonoBehaviour
    {
        public static GameObject errorBox;
        public void Awake()
        {
            errorBox = Resources.Load("Error Message") as GameObject;
           
        }
        public void Start()
        {
            if (errorBox != null)
            {
                throw new InterfaceException("test");
            }
        }
        public static void InstantiateError(string message)
        {
            GameObject error = Instantiate(errorBox, Vector3.zero, Quaternion.identity) as GameObject;
            error.transform.parent = GameObject.Find("C/O Interfaces").transform;
            GameObject.Find("ErrorText").GetComponent<Text>().text = message;
            Vector3 set = new Vector3(0, 78.2f, 0);
            error.transform.position = set;
        }
	}
    public class InterfaceException : Exception
    {
        public InterfaceException()
        {
        }
        public InterfaceException(string message)
        {
            ErrorContainer.InstantiateError(message);
        }
    }
}