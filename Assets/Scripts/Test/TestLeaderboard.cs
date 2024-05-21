using System.Threading.Tasks;
using UnityEngine;

public class TestLeaderboard : MonoBehaviour
{
    public LeaderboardManager leaderboardManager;

    async void Start()
    {
        // Example: Saving player data and score
        await SavePlayerDataAndScore("PlayerName", 100);

        // Wait for 3 seconds
        await Task.Delay(3000);

        // Example: Retrieving and printing top scores
        await GetAndPrintTopScores();
    }

    async Task SavePlayerDataAndScore(string playerName, int playerScore)
    {
        try
        {
            await leaderboardManager.SavePlayerDataAndScore(playerName, playerScore);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to save player data and score: {ex}");
        }
    }

    async Task GetAndPrintTopScores()
    {
        //try
        //{
        //    await leaderboardManager.GetTopScores();
        //}
        //catch (System.Exception ex)
        //{
        //    Debug.LogError($"Failed to retrieve and print top scores: {ex}");
        //}
        try
        {
            await leaderboardManager.GetPlayerRange();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to retrieve and print top scores: {ex}");
        }
    }
}
