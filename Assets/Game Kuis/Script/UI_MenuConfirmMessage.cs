using UnityEngine;
using TMPro;

public class UI_MenuConfirmMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private GameObject _pesanCukupKoin = null;

    [SerializeField]
    private GameObject _pesanTakCukupKoin = null;

    private UI_OpsiLevelPack _tombolLevelPack = null;

    private LevelPackKuis _levelPack = null;

    void Start()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);

            //subscribe event
            UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
        }
    }

    private void OnDestroy()
    {
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }

    private void UI_OpsiLevelPack_EventSaatKlik(UI_OpsiLevelPack tombolLevelPack,LevelPackKuis levelPack, bool terkunci)
    {
        if (!terkunci)
            return;

        gameObject.SetActive(true);

        //cek kecukupan koin untuk membeli level pack
        if(_playerProgress.progresData.koin < levelPack.Harga)
        {
            _pesanCukupKoin.SetActive(false);
            _pesanTakCukupKoin.SetActive(true);
            return;
        }

        _pesanTakCukupKoin.SetActive(false);
        _pesanCukupKoin.SetActive(true);

        _tombolLevelPack = tombolLevelPack;
        _levelPack = levelPack;
    }

    public void BukaLevel()
    {
        _playerProgress.progresData.koin -= _levelPack.Harga;
        _playerProgress.progresData.progresLevel[_levelPack.name] = 1;

        _tempatKoin.text = $"{_playerProgress.progresData.koin}";

        _playerProgress.SimpanProgres();
        _tombolLevelPack.BukaLevelPack();
    }
}
