using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RuntimeVar : ScriptableObject, ISerializationCallbackReceiver
{
    public float InitialVal;

    [System.NonSerialized]
    private float _RuntimeVal;
    public float RuntimeVal 
    {
        get => _RuntimeVal;
        set
        {
            if (value < 0) { _RuntimeVal = 0; }
            else if(value > InitialVal) {_RuntimeVal = InitialVal;}
            else {_RuntimeVal = value;}
        }
    }

    public void OnAfterDeserialize() {
        RuntimeVal = InitialVal;
    }

public void OnBeforeSerialize() { }

}
