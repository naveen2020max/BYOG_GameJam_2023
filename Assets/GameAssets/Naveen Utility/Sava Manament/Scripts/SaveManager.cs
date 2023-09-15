using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naveen
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;

        //create a class for saveType use it instead of TestSaveType
        //public DataInfo<TestSaveType> CountData;



        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogError("Two SaveManager Instance exists");
                Destroy(this);
            }
        }

        private void Start()
        {
            //CountData = new DataInfo<TestSaveType>("CountData");
            //CountData.LoadData();
            //Debug.Log("Initialized");
        }
    }
}
