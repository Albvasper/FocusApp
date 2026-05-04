using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public PetType Type;
    public string PetName;
    public int CurrentLeafs;
    public int LastDateCheckIn;
    public List<DecorationData> Decorations;
    public List<string> Sessions;
}

[System.Serializable]
public class DecorationData
{
    public string ID;
    public float X;
    public float Y;
    public bool Flipped;
    public int SortingLayer;
}