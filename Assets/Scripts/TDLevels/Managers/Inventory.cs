using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    Dictionary<string, int> turretDict = new Dictionary<string, int>()
    {
        {"Ring", 2 },
        {"Soda", 2 },
        {"Cold", 2 }
    };
    
    public void AddTurret(string type, int amount)
    {
        turretDict[type] += amount;
    }

    public int GetAmount(string type)
    {
        return turretDict[type];
    }

    public bool Build(string type)
    {
        if(HasTurret(type))
        {
            turretDict[type]--;
            return true;
        }
        return false;
    }

    public bool HasTurret(string type)
    {
        if(turretDict[type] > 0)
        {
            return true;
        }
        return false;
    }

    public void OutputDict()
    {
        Debug.Log(turretDict.Count);
        foreach (KeyValuePair<string, int> kvp in turretDict)
            Debug.Log("Key = {0} + Value = {1}" + kvp.Key + kvp.Value);
    }
}




