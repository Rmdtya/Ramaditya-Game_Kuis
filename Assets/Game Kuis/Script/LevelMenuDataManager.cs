using UnityEngine;
using TMPro;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    void Start()
    {
        _tempatKoin.text = $"{_playerProgress.progresData.koin}";
    }

}
