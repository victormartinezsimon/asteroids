using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    public GameObject _asteroid;
    public int _initialSize;

    private List<GameObject> m_unused;
    private List<GameObject> m_used;

    // Use this for initialization
    void Awake()
    {
        m_unused = new List<GameObject>();
        m_used = new List<GameObject>();

        //initialize the unused list with the size
        for (int i = 0; i < _initialSize; ++i)
        {
            GameObject go = Instantiate(_asteroid);
            go.SetActive(false);
            go.transform.SetParent(this.transform);
            m_unused.Add(go);
        }
    }

    public GameObject getObject()
    {
        //if there is no objects, we create a new one
        if(m_unused.Count == 0)
        {
            GameObject go = Instantiate(_asteroid);
            m_unused.Add(go);
            go.SetActive(false);
            go.transform.SetParent(this.transform);
        }

        GameObject toReturn = m_unused[0];
        m_unused.RemoveAt(0);
        m_used.Add(toReturn);
        toReturn.SetActive(true);
        return toReturn;
    }

    public void releaseObject(GameObject go)
    {
        int index = m_used.FindIndex(e =>  e == go);
        if(index >= 0)
        {
            //we check that the object is from this pool
            m_used.RemoveAt(index);
            m_unused.Add(go);
            go.SetActive(false);
        }
    }
}
