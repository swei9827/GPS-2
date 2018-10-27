using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelection : MonoBehaviour
{

    public int ID { get; set; }
    public string StageName { get; set; }
    public bool Completed { get; set; }
    public int Coins { get; set; }
    public bool Locked { get; set; }

    public StageSelection (int id, string stageName, bool completed, int coins, bool locked)
    {
        this.ID = id;
        this.StageName = stageName;
        this.Completed = completed;
        this.Coins = coins;
        this.Locked = locked;
    }

    public void Complete()
    {
        this.Completed = true;
    }

    public void Complete(int coins)
    {
        this.Completed = true;
        this.Coins = coins;
    }

    public void Lock()
    {
        this.Locked = true;
    }

    public void Unlock()
    {
        this.Locked = false;
    }
}
