/////////////////////////////////////////////////////////////////////////////////////////////// All Of This Code Is from Moodle //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                                                                               ////////////////////////////////         
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Perception : MonoBehaviour
{
    public Dictionary<GameObject, MemoryRecord> MemoryMap = new Dictionary<GameObject, MemoryRecord>();

    public GameObject[] sensedObjects;
    public MemoryRecord[] sensedRecord;

    public void ClearFoV()
    {
        foreach(KeyValuePair<GameObject, MemoryRecord> Memory in MemoryMap)
        {
            Memory.Value.WithinFoV = false;
        }
    }

    public void AddMemory(GameObject Target)
    {
        MemoryRecord record = new MemoryRecord(DateTime.Now, Target.transform.position, true);

        if(MemoryMap.ContainsKey(Target))
        {
            MemoryMap[Target] = record;
        }
        else
        {
            MemoryMap.Add(Target, record);
        }

    }

    void Update()
    {
        sensedObjects = new GameObject[MemoryMap.Keys.Count];
        sensedRecord = new MemoryRecord[MemoryMap.Values.Count];
        MemoryMap.Keys.CopyTo(sensedObjects, 0);
        MemoryMap.Values.CopyTo(sensedRecord, 0);
    }

    
}

[Serializable]
public class MemoryRecord
{
    [SerializeField]
    public DateTime TimeLastSensed;

    [SerializeField]
    public Vector3 LastSensedPosition;

    [SerializeField]
    public bool WithinFoV;

    [SerializeField]
    public string TimeLastSensedStr;

    public MemoryRecord()
    {
        TimeLastSensed = DateTime.MinValue;
        LastSensedPosition = Vector3.zero;
        WithinFoV = false;
    }

    public MemoryRecord(DateTime Time, Vector3 Pos, bool FoV)
    {
        TimeLastSensed = Time;
        LastSensedPosition = Pos;
        WithinFoV = FoV;
        TimeLastSensedStr = Time.ToLongTimeString();
    }
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////