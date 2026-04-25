using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public PetType Type;
    public string PetName;
    public int CurrentLeafs;
    // TODO: Journal sessions
    public int LastDateCheckIn;
    public List<DecorationData> Decorations;
}

[System.Serializable]
public class DecorationData
{
    public string ID;
    public float X;
    public float Y;
    public bool Flipped;
}