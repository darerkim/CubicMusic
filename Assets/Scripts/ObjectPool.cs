using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public GameObject go_Prefab;
    public int count;
    public Transform tf_PoolParent;
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField] ObjectInfo[] objectInfo = null;

    public static ObjectPool instance;

    public Queue<GameObject> noteQueue = new Queue<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        noteQueue = InsertQueue(objectInfo[0]);
        //noteQueue = InsertQueue(objectInfo[1]);
        //noteQueue = InsertQueue(objectInfo[2]);
    }

    Queue<GameObject> InsertQueue(ObjectInfo p_objectInfo)
    {
        Queue<GameObject> t_queue = new Queue<GameObject>();
        for (int i = 0; i < p_objectInfo.count; i++)
        {
            GameObject t_clone = Instantiate(p_objectInfo.go_Prefab, transform.position, Quaternion.identity);
            t_clone.SetActive(false);
            if (p_objectInfo.tf_PoolParent != null)
                t_clone.transform.SetParent(p_objectInfo.tf_PoolParent);
            else
                t_clone.transform.SetParent(this.transform);

            t_queue.Enqueue(t_clone);
        }

        return t_queue;
    }
}
