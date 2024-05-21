using Unity.Services.CloudCode;
using Unity.Services.Leaderboards;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Leaderboards.Exceptions;
using Newtonsoft.Json;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardManager : MonoBehaviour
{
    public async Task SavePlayerDataAndScore(string playerName, int playerScore)
    {
        var data = new Dictionary<string, object>
        {
            { "playerName", playerName },
            { "playerScore", playerScore }
        };

        try
        {
            await LeaderboardsService.Instance.AddPlayerScoreAsync("top_players", playerScore);
            Debug.Log("Player score posted to leaderboard");
        }
        catch (LeaderboardsException ex)
        {
            Debug.LogError($"Failed to post score to leaderboard: {ex}");
        }
    }

    //public async Task GetTopScores()
    //{
    //    try
    //    {
    //        var response = await CloudCodeService.Instance.CallEndpointAsync<List<ScoreEntry>>("GetTopScores", null);

    //        Debug.Log("Top scores retrieved");
    //        foreach (var score in response)
    //        {
    //            Debug.Log($"{score.PlayerName}: {score.PlayerScore}");
    //        }
    //    }
    //    catch (CloudCodeException ex)
    //    {
    //        Debug.LogError($"Failed to retrieve top scores: {ex}");
    //    }
    //}

    //public async Task GetTopScores()
    //{
    //    try
    //    {
    //        var leaderboardId = "top_players";
    //        var versionId = "your_version_id";

    //        var scoresResponse = await LeaderboardsService.Instance.GetVersionScoresAsync(leaderboardId, versionId);

    //        // Log the retrieved scores
    //        Debug.Log("Top scores retrieved");
    //        foreach (var score in scoresResponse.Entries)
    //        {
    //            Debug.Log($"{score.PlayerName}: {score.Score}");
    //        }
    //    }
    //    catch (LeaderboardsException ex)
    //    {
    //        Debug.LogError($"Failed to retrieve top scores: {ex.Message}");
    //    }
    //}
    public async Task GetPlayerRange()
    {
        // Returns a total of 11 entries (the given player plus 5 on either side)
        var rangeLimit = 5;
        var scoresResponse = await LeaderboardsService.Instance.GetPlayerRangeAsync(
            "top_players",
            new GetPlayerRangeOptions { RangeLimit = rangeLimit }
        );
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }
}

[System.Serializable]
public class ScoreEntry
{
    public string PlayerName;
    public int PlayerScore;
}
