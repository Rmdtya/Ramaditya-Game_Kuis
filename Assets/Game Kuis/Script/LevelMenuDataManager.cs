using UnityEngine;
using TMPro;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private UI_LevelPackList _levelPackList = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    [SerializeField]
    private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    void Start()
    {

        if (!_playerProgress.MuatProgres())
        {
            _playerProgress.SimpanProgres();
        }

        _levelPackList.LoadLevelPack(_levelPacks, _playerProgress.progresData);

        _tempatKoin.text = $"{_playerProgress.progresData.koin}";

        AudioManager.instance.PlayBGM(0);
    }

}
