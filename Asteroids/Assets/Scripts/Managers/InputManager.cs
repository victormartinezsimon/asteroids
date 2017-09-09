using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);

            if(hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<Click>().Action();
                GameManager.Instance().AsteroidDestroyed();
            }
        }
    }
}
