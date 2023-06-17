using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> id = new List <TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();
    
    public void OnBeforeSerialize() {
        id.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this) {
            id.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize() {
        this.Clear();

        if(id.Count != values.Count) {
            Debug.LogError("Tried to deserialize a SerializableDictionary, but the amount of id's (" + id.Count + ")is not the same as the amount of values (" + values.Count + ")");
        }
        for (int i = 0; i < id.Count; i++) {
            this.Add(id[i], values[i]);
        }
    }   
}
