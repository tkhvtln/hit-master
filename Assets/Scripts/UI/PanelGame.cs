using UnityEngine;
using Zenject;

public class PanelGame : MonoBehaviour
{
    [Inject]
    private void Construct() 
    {

    }

    public void Show() 
    {
        gameObject.SetActive(true);
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
    }
}
