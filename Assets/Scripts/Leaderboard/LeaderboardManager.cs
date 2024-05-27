using UnityEngine;
using Newtonsoft.Json;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Unity.Services.Core;
using Newtonsoft.Json.Linq;
using System.Text;


public class LeaderboardManager : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
        await SignInAnonymouslyAsync();
        await AddScore("top_players", 33);
        await GetPlayerScore("top_players");
        await GetScores("top_players");
    }
    private async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Signed in anonymously");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogError($"Sign in failed: {ex}");
        }
    }
    public async Task AddScore(string leaderboardId, int score)
    {
        var playerEntry = await LeaderboardsService.Instance
            .AddPlayerScoreAsync(leaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(playerEntry));
    }

    public async Task GetPlayerScore(string leaderboardId)
    {
        var scoreResponse = await LeaderboardsService.Instance
            .GetPlayerScoreAsync(leaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    //public async Task GetScores(string leaderboardId)
    //{
    //    var scoresResponse = await LeaderboardsService.Instance
    //        .GetScoresAsync(leaderboardId);
    //    Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    //}
    public async Task GetScores(string leaderboardId)
    {
        var scoresResponse = await LeaderboardsService.Instance
            .GetScoresAsync(leaderboardId);

        JObject scoresJson = JObject.Parse(JsonConvert.SerializeObject(scoresResponse));

        StringBuilder sb = new StringBuilder();

        foreach (var result in scoresJson["results"])
        {
            string playerName = result["playerName"].ToString();
            int rank = result["rank"].ToObject<int>();
            float score = result["score"].ToObject<float>();

            sb.AppendLine($"{rank}\t{playerName}\t{score}");
        }

        Debug.Log(sb.ToString());
    }
}
