using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastChackPointKey = 0;

    public List<CheckpointBase> checkpoints;


    public bool HasCheckpoint()
    {
        return lastChackPointKey > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if(i > lastChackPointKey)
        {
            lastChackPointKey = i;
            SaveManager.Instance.SaveItems();
        }
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastChackPointKey);
        return checkpoint.transform.position;
    }
}
