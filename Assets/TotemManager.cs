using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemManager : MonoBehaviour
{
    private static TotemManager _instance;
    public static TotemManager Instance { get { return _instance; } }
    public static TotemDBWrapper totemdb;

    public List<TotemItem> items;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            var totemdb = gameObject.AddComponent<TotemDBWrapper>();
            items = totemdb.GetAllItems();
        }
    }
}