using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[System.Serializable]
//==================================================================
/**
 * @brief Dictionary for JsonUtility
 */
//==================================================================
public class Dict<Tkey, TValue> : Dictionary<Tkey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<Tkey> keys = new List<Tkey>();
    [SerializeField] private List<TValue> vals = new List<TValue>();

    public void OnBeforeSerialize()
    {
        keys.Clear();
        vals.Clear();

        var e = GetEnumerator();

        while (e.MoveNext())
        {
            keys.Add(e.Current.Key);
            vals.Add(e.Current.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        this.Clear();

        int cnt = (keys.Count <= vals.Count) ? keys.Count : vals.Count;
        for (int i = 0; i < cnt; ++i)
            this[keys[i]] = vals[i];
    }
}

[Serializable]
public class ItemDict : ISerializationCallbackReceiver
{
    public int this[Item i] { get { return Items[i]; } }
    [NonSerialized] public Dictionary<Item, int> Items;
    [SerializeField] private List<Item> _keys;
    [SerializeField] private List<int> _values;

    public ItemDict(ICollection<string> names)
    {
        Items = new Dictionary<Item, int> { };
        foreach (var name in names)
        {
            Items.Add(new Item(name), Random.Range(0, 100));
        }
    }

    public void OnBeforeSerialize()
    {
        _keys = new List<Item> { };
        foreach (Item key in Items.Keys)
        {
            _keys.Add(key);
        }
        _values = new List<int> { };
        foreach (int value in Items.Values)
        {
            _values.Add(value);
        }
    }

    public void OnAfterDeserialize()
    {
        Items = new Dictionary<Item, int> { };
        for (var i = 0; i < _keys.Count; i++)
        {
            Items.Add(_keys[i], _values[i]);
        }
    }

    public override string ToString()
    {
        var items = new List<KeyValuePair<Item, int>> { };
        foreach (var item in Items)
        {
            items.Add(item);
        }
        return $"\"Items\":[ {string.Join(",", items.ConvertAll(item => $"{item.Key}:{item.Value}"))}]";
    }
}

[Serializable]
public class Item : ISerializationCallbackReceiver
{

    [SerializeField] public string Name;
    [NonSerialized] public Guid Id;
    [SerializeField] private string _id;

    public Item(string name)
    {
        Id = Guid.NewGuid();
        this.Name = name ?? Id.ToString();
    }

    public void OnBeforeSerialize()
    {
        _id = Id.ToString();
    }

    public void OnAfterDeserialize()
    {
        Id = Guid.Empty;
        Guid.TryParse(_id, out Id);
    }

    public override string ToString()
    {
        return $"{{\"name\":\"{Name}\", \"id\":\"{Id}\"}}";
    }

}
