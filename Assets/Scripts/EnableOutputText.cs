using UnityEngine;

public class EnableOutputText : MonoBehaviour
{
    [SerializeField] private GameObject outputTextGO;

    public void EnableOutputTextGO()
    {
        outputTextGO.SetActive(true);
    }

    public void DisableOutputTextGO()
    {
        outputTextGO.SetActive(false);
    }
}
