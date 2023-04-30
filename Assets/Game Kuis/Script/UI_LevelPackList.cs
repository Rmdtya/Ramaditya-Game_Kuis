using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private UI_LevelKuisList _levelList = null;

    [SerializeField]
    private UI_OpsiLevelPack _tombolLevelPack = null;

    [SerializeField]
    private RectTransform _content = null;

    // Start is called before the first frame update
    void Start()
    {
        //LoadLevelPack();

        if (_inisialData.SaatKalah)
        {
            UI_OpsiLevelPack_EventSaatKlik(null, _inisialData.levelPack, false);
        }

        //Subsribe Event
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    private void OnDestroy()
    {
        //Unsubscribe Event
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }

    private void UI_OpsiLevelPack_EventSaatKlik(UI_OpsiLevelPack tombolLevelPack,LevelPackKuis levelPack, bool terkunci)
    {
        if (terkunci)
            return;

        /*_levelList.gameObject.SetActive(true);*/
        _levelList.UnloadLevelPack(levelPack);

        //Tutup Menu Level Pack
        // gameObject.SetActive(false);

        _inisialData.levelPack = levelPack;

        _animator.SetTrigger("KeLevels");
    }

    public void LoadLevelPack(LevelPackKuis[] levelPacks, PlayerProgress.MainData playerData)
    {
        foreach(var lp in levelPacks)
        {
            //Membuat salinan object dari prefab tombol level pack
            var t = Instantiate(_tombolLevelPack);

            t.SetLevelPack(lp);

            //Masukan Object tombol sebagai anak dari object content
            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;

            //cek apakah level pack terdafar di dictionary progress pemain
            if (!playerData.progresLevel.ContainsKey(lp.name))
            {
                //jika tidak terdaftar maka level pack terkunci
                t.KunciLevelPack();
            }
        }
    }
}
