using UnityEngine;

public class BerkayGitHub : MonoBehaviour
{
    private string url = "https://github.com/berkaykiziltug";
   
    public void OpenProfile()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("URL is not set!");
        }
    }
}
