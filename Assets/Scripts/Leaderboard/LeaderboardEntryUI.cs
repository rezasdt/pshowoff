using TMPro;
using UnityEngine;
using System.Text;

public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rank;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI score;
    
    private static StringBuilder _scoreBuilder = new();

    public void Init(int pRank, string pPlayerName, int pScore)
    {
        int offsetRank = pRank + 1;
        rank.text = offsetRank.ToString();
        playerName.text = pPlayerName;

        // Clear the StringBuilder before using it
        _scoreBuilder.Clear();
        _scoreBuilder.AppendFormat("{0:N0}$", pScore);
        score.text = _scoreBuilder.ToString();
    }
}