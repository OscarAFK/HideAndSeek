using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoresUI : MonoBehaviour
{
    Dictionary<Player, PlayerScoreUI> scoresPlayers;
    public GameObject prefabPlayerScore;

    /*private void Update()
    {
        foreach(var p in scoresPlayers)
        {
            UpdateScorePlayer(p.Key);
        }
    }*/

    public void AddPlayerUI(Player player)
    {
        if(scoresPlayers==null) scoresPlayers = new Dictionary<Player, PlayerScoreUI>();
        GameObject newPlayerScoreGO = Instantiate(prefabPlayerScore, transform);
        var newPlayerScore = newPlayerScoreGO.GetComponent<PlayerScoreUI>();
        newPlayerScore.SetPseudoJoueur(player.netPseudo.Value.ToString());
        newPlayerScore.SetScoreJoueur(player.netScore.Value);
        scoresPlayers.Add(player, newPlayerScore);
    }

    public void UpdatePseudoPlayer(Player player)
    {
        scoresPlayers[player].SetPseudoJoueur(player.netPseudo.Value);
    }

    public void RemovePlayerUI(Player player)
    {
        var go = scoresPlayers[player].gameObject;
        scoresPlayers.Remove(player);
        Destroy(go);
    }

    public void UpdateScorePlayer(Player player, bool winScore)
    {
        scoresPlayers[player].SetScoreJoueur(player.netScore.Value);
        scoresPlayers[player].PlayScoreAnimation(winScore);
    }

    public void UpdateRoleUI(Player player)
    {
        if(player.GetComponent<PlayerFirstPersonMovement>()) scoresPlayers[player].SetToHunter();
        else scoresPlayers[player].SetToPrey();
    }
}
