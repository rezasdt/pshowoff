using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using UnityEngine;

public class AuthManager : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
        await SignInAnonymouslyAsync();
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
}
