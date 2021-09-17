using Mirror;
using UnityEngine;

public class GameNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        GameNetworkPlayer player = conn.identity.GetComponent<GameNetworkPlayer>();

        player.SetDisplayName($"Player: {numPlayers}");

        player.SetDisplayColor(GenerateRandomColor());
    }

    private static Color GenerateRandomColor()
    {
        return new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));
    }
}
