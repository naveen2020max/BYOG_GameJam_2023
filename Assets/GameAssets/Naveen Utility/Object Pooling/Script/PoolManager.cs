using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naveen
{
    public class PoolManager : MonoBehaviour
    {
        [System.Serializable]
        public class pool
        {
            public string tag;
            public int count;
            public GameObject poolObject;
        }

        public List<pool> Pools;

        //Create a class with monobehaviour and use it instead of TestPoolObj

        //public ObjectPooler<TestPoolObj> objectPooler;

        void Start()
        {
            //objectPooler = new ObjectPooler<TestPoolObj>(Pools);
            //objectPooler.SpawnFromPool(Pools[0].tag, Vector3.zero, Quaternion.identity);
        }

        void Update()
        {

        }
    }


    public class ObjectPooler<T> where T : MonoBehaviour, IInfo
    {
        public Dictionary<string, Queue<T>> poolDictionary;

        public ObjectPooler(List<PoolManager.pool> pools)
        {
            poolDictionary = new Dictionary<string, Queue<T>>();
            foreach (var pool in pools)
            {
                Queue<T> Objectpool = new Queue<T>();
                for (int i = 0; i < pool.count; i++)
                {
                    T obj;

                    obj = Object.Instantiate(pool.poolObject).GetComponent<T>();
                    obj.gameObject.SetActive(false);
                    Objectpool.Enqueue(obj);
                }
                poolDictionary.Add(pool.tag, Objectpool);

            }
        }

        public T SpawnFromPool(string _tag, Vector3 _pos, Quaternion _rot, object info = null)
        {
            if (!poolDictionary.ContainsKey(_tag))
            {
                Debug.LogWarning("Pool does not contain the tag " + _tag);
                return null;
            }

            T objectToSpawn = poolDictionary[_tag].Dequeue();

            if (info != null)
            {
                objectToSpawn.FillInfo(info);
            }

            objectToSpawn.transform.position = _pos;
            objectToSpawn.transform.rotation = _rot;
            objectToSpawn.gameObject.SetActive(true);


            poolDictionary[_tag].Enqueue(objectToSpawn);
            return objectToSpawn;
        }




    }
    public interface IInfo
    {
        void FillInfo(object _info);
    }
}
