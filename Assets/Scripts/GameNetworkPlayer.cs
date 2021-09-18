using Mirror;
using TMPro;
using UnityEngine;

public class GameNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] private Renderer displayColorRenderer = null;

    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField] private string displayName = "Missing Name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField] private Color displayColor = Color.black;

    #region Server

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

    [Command]
    private void CmdSetDisplayName(string name)
    {
        if (name.Length < 2 ||
            name.Length > 20) return;

        RpcLogNewName(name);
        SetDisplayName(name);
    }

    #endregion

    #region Client

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = displayName;
    }

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    [ContextMenu("Set Player Name")]
    private void SetPlayerName()
    {
        CmdSetDisplayName("NEW NAME!");
    }

    [ClientRpc]
    private void RpcLogNewName(string name)
    {
        Debug.Log(name);
    }

    #endregion
}
