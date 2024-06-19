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
    public class LeaderboardPlayer
    {
        public int rank;
        public string playerName;
        public int score;

        public LeaderboardPlayer(int rank, string playerName, int score)
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

    [SerializeField] private string leaderboardId = "top_players";
    [SerializeField] private GameObject leaderboardButton;
    [SerializeField] private GameObject leaderboardView;
    [SerializeField] private RectTransform container;
    [SerializeField] private LeaderboardEntryUI entry1Prefab;
    [SerializeField] private LeaderboardEntryUI entry2Prefab;
    [SerializeField] private LeaderboardEntryUI entry3Prefab;
    [SerializeField] private LeaderboardEntryUI entryOtherPrefab;
    [SerializeField] private Int64Variable moneyVariable;
    private SortedDictionary<int, LeaderboardPlayer> _leaderboardPlayers = new();
    private System.Action<LeaderboardPlayer> _createEntry;
    private string _cachedPlayerName;
    
    private void Awake()
    {
        _createEntry = CreateEntry1;
    }

    async void Start()
    {
        await UnityServices.InitializeAsync();
        await SignInAnonymouslyAsync();
        await AddScore(leaderboardId, moneyVariable.Value);
        await GetPlayerScore(leaderboardId);
        await GetScores(leaderboardId);
        await GetPlayerRange(leaderboardId);
        CreateEntries();
    }

    private async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            //Debug.Log("Signed in anonymously");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogError($"Sign in failed: {ex}");
        }
    }

    private async Task AddScore(string leaderboardId, long score)
    {
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
        var playerToUpdate = _leaderboardPlayers.Values.FirstOrDefault(p => p.playerName == _cachedPlayerName);
        if (playerToUpdate != null)
        {
            playerToUpdate.playerName = "You";
        }

        foreach (var entry in _leaderboardPlayers)
        {
            _createEntry(entry.Value);
        }
        leaderboardView.gameObject.SetActive(true);
        leaderboardButton.gameObject.SetActive(true);
    }

    private void ProcessScores(object scoresResponse)
    {
        JObject scoresJson = JObject.Parse(JsonConvert.SerializeObject(scoresResponse));
        _leaderboardPlayers.Clear();

        foreach (var result in scoresJson["results"])
        {
            int rank = result["rank"].ToObject<int>();
            string playerName = result["playerName"].ToString();
            int score = result["score"].ToObject<int>();

            _leaderboardPlayers[rank] = new LeaderboardPlayer(rank, playerName, score);
        }

        // foreach (var entry in _leaderboardPlayers)
        // {
        //     Debug.Log(entry.Value);
        // }
    }
    
    
    private void CreateEntry1(LeaderboardPlayer pPlayer)
    {
        var entry = Instantiate(entry1Prefab, container);
        entry.Init(pPlayer.rank, pPlayer.playerName, pPlayer.score);
        _createEntry = CreateEntry2;
    }

    private void CreateEntry2(LeaderboardPlayer pPlayer)
    {
        var entry = Instantiate(entry2Prefab, container);
        entry.Init(pPlayer.rank, pPlayer.playerName, pPlayer.score);
        _createEntry = CreateEntry3;
    }

    private void CreateEntry3(LeaderboardPlayer pPlayer)
    {
        var entry = Instantiate(entry3Prefab, container);
        entry.Init(pPlayer.rank, pPlayer.playerName, pPlayer.score);
        _createEntry = CreateEntryOther;
    }

    private void CreateEntryOther(LeaderboardPlayer pPlayer)
    {
        var entry = Instantiate(entryOtherPrefab, container);
        entry.Init(pPlayer.rank, pPlayer.playerName, pPlayer.score);
    }

}
