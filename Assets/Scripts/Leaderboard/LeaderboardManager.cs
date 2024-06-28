using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Unity.Services.Core;
using Newtonsoft.Json.Linq;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private string leaderboardId = "top_players";
    [SerializeField] private GameObject leaderboardView;
    [SerializeField] private RectTransform container;
    [SerializeField] private LeaderboardEntryUI entry1Prefab;
    [SerializeField] private LeaderboardEntryUI entry2Prefab;
    [SerializeField] private LeaderboardEntryUI entry3Prefab;
    [SerializeField] private LeaderboardEntryUI entryOtherPrefab;
    [SerializeField] private Int64Variable moneyVariable;
    [SerializeField] private BoolVariable isPlaying;
    private SortedDictionary<int, PlayerEntry> _leaderboardPlayers = new();
    private System.Action<PlayerEntry> _createEntry;
    private string _cachedPlayerName = string.Empty;

    private async void OnEnable()
    {
        _cachedPlayerName = string.Empty;
        _createEntry = CreateEntry1;
        _leaderboardPlayers.Clear();
        await UnityServices.InitializeAsync();
        await SignInAnonymouslyAsync();
        try
        {
            await AddScore(leaderboardId, moneyVariable.Value);
            await GetPlayerScore(leaderboardId);
        }
        catch { }
        await GetScores(leaderboardId);
        try
        {
            await GetPlayerRange(leaderboardId);
        }
        catch { }
        CreateEntries();
        leaderboardView.SetActive(true);
    }

    private async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            //Debug.Log("Signed in anonymously");
        }
        catch { }
        // catch (AuthenticationException ex)
        // {
        //     Debug.LogError($"Sign in failed: {ex}");
        // }
    }

    private async Task AddScore(string leaderboardId, long score)
    {
        if (!isPlaying.Value) return;
        var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, (double)score);
        //Debug.Log(JsonConvert.SerializeObject(playerEntry));
    }

    private async Task GetPlayerScore(string leaderboardId)
    {
        var scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
        //Debug.Log(JsonConvert.SerializeObject(scoreResponse));

        // Cache the player name
        JObject responseJson = JObject.Parse(JsonConvert.SerializeObject(scoreResponse));
        _cachedPlayerName = responseJson["playerName"].ToString();
    }

    private async Task GetScores(string leaderboardId)
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId);
        ProcessScores(scoresResponse);
    }

    private async Task GetPlayerRange(string leaderboardId)
    {
        var rangeLimit = 5;
        var scoresResponse = await LeaderboardsService.Instance.GetPlayerRangeAsync(
            leaderboardId,
            new GetPlayerRangeOptions { RangeLimit = rangeLimit }
        );
        ProcessScores(scoresResponse);
    }

    private void CreateEntries()
    {
        if (_cachedPlayerName != string.Empty)
        {
            var playerToUpdate = _leaderboardPlayers.Values.FirstOrDefault(p => p.playerName == _cachedPlayerName);
            if (playerToUpdate != null)
            {
                playerToUpdate.playerName = "You";
            }
        }

        foreach (var entry in _leaderboardPlayers)
        {
            _createEntry(entry.Value);
        }
    }

    private void ProcessScores(object scoresResponse)
    {
        JObject scoresJson = JObject.Parse(JsonConvert.SerializeObject(scoresResponse));

        foreach (var result in scoresJson["results"])
        {
            int rank = result["rank"].ToObject<int>();
            string playerName = result["playerName"].ToString();
            int score = result["score"].ToObject<int>();

            _leaderboardPlayers[rank] = new PlayerEntry(rank, playerName, score);
        }

        // foreach (var entry in _leaderboardPlayers)
        // {
        //     Debug.Log(entry.Value);
        // }
    }
    
    
    private void CreateEntry1(PlayerEntry pPlayerEntry)
    {
        var entry = Instantiate(entry1Prefab, container);
        entry.Init(pPlayerEntry.rank, pPlayerEntry.playerName, pPlayerEntry.score);
        _createEntry = CreateEntry2;
    }

    private void CreateEntry2(PlayerEntry pPlayerEntry)
    {
        var entry = Instantiate(entry2Prefab, container);
        entry.Init(pPlayerEntry.rank, pPlayerEntry.playerName, pPlayerEntry.score);
        _createEntry = CreateEntry3;
    }

    private void CreateEntry3(PlayerEntry pPlayerEntry)
    {
        var entry = Instantiate(entry3Prefab, container);
        entry.Init(pPlayerEntry.rank, pPlayerEntry.playerName, pPlayerEntry.score);
        _createEntry = CreateEntryOther;
    }

    private void CreateEntryOther(PlayerEntry pPlayerEntry)
    {
        var entry = Instantiate(entryOtherPrefab, container);
        entry.Init(pPlayerEntry.rank, pPlayerEntry.playerName, pPlayerEntry.score);
    }

    private void OnDisable()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
    
    class PlayerEntry
    {
        public int rank;
        public string playerName;
        public int score;

        public PlayerEntry(int rank, string playerName, int score)
        {
            this.rank = rank;
            this.playerName = playerName;
            this.score = score;
        }

        public override string ToString()
        {
            return $"Rank: {rank}, Name: {playerName}, Score: {score}";
        }
    }
}
