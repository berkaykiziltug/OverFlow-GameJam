using UnityEngine;

public class KerimLinkedIn : MonoBehaviour
{
    private string url = "https://www.linkedin.com/in/azizkerimkocak/";
   
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
