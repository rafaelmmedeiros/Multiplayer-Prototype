using Mirror;
using UnityEngine;

public class GameNetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]
    private string displayName = "Missing Name";

    [SyncVar]
    [SerializeField]
    private Color displayColor = Color.black;

    [Server]
    public void SetDisplayName(string name)
    {
        displayName = name;
    }

    [Server]
    public void SetDisplayColor(Color color)
    {
        displayColor = color;
    }
}
