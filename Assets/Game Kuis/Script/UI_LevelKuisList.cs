using UnityEngine;

public class UI_LevelKuisList : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private UI_OpsiLevelKuis _tombolLevel = null;

    [SerializeField]
    private RectTransform _content = null;

    [Space, SerializeField]
    private LevelPackKuis _levelPack = null;

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _gameplayScene = null;

    // Start is called before the first frame update

    private void Start()
    {
        UI_OpsiLevelKuis.EventSaatKlik += UI_OpsiLevelKuis_EventSaatKlik;
    }

    private void OnDestroy()
    {
        UI_OpsiLevelKuis.EventSaatKlik -= UI_OpsiLevelKuis_EventSaatKlik;
    }

    private void UI_OpsiLevelKuis_EventSaatKlik(int index)
    {
        _inisialData.levelIndex = index;
        _gameSceneManager.BukaScene(_gameplayScene);
    }

    public void UnloadLevelPack(LevelPackKuis levelPack)
    {
        HapusIsiKonten();

        var levelTerbukaTerakhir = _playerProgress.progresData.progresLevel[levelPack.name] - 1;

        _levelPack = levelPack;

        for (int i = 0; i < levelPack.BanyakLevel; i++)
        {
            //Membuat salinan object dari prefab tombol level pack
            var t = Instantiate(_tombolLevel);

            t.SetLevelKuis(levelPack.AmbilLevelKe(i), i);

            //Masukan Object tombol sebagai anak dari object content
            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;

            if (i > levelTerbukaTerakhir)
            {
                t.InteraksiTombol = false;
            }
        }
    }

    private void HapusIsiKonten()
    {
        var cc = _content.childCount;

        for(int i = 0; i < cc; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
    }
}
