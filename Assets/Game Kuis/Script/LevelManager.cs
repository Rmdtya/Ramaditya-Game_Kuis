using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private LevelPackKuis _soalSoal = null;

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;

    [SerializeField]
    private UI_PointJawaban[] _pilihanJawaban = new UI_PointJawaban[0];

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _namaScenePilihMenu = string.Empty;

    [SerializeField]
    private PemanggilSuara _pemanggilSuara = null;

    [SerializeField]
    private AudioClip _suaraMenang = null;

    [SerializeField]
    private AudioClip _suaraKalah = null;

    private int _indexSoal = -1;

    private void Awake()
    {
        _inisialData.SaatKalah = false;
    }
    private void Start()
    {

        _soalSoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;
        
        NextLevel();

        AudioManager.instance.PlayBGM(1);
        UI_PointJawaban.EventJawabSoal += UI_PointJawaban_EventJawabSoal;
    }

    private void OnDestroy()
    {
        UI_PointJawaban.EventJawabSoal -= UI_PointJawaban_EventJawabSoal;
    }

    private void OnApplicationQuit()
    {
        _inisialData.SaatKalah = false;
    }
    private void UI_PointJawaban_EventJawabSoal(string jawaban, bool adalahBenar)
    {

        _pemanggilSuara.PanggilSuara(adalahBenar ? _suaraMenang: _suaraKalah);

        if (!adalahBenar) return;

        string namaLevelPack = _inisialData.levelPack.name;
        int levelTerakhir = _playerProgress.progresData.progresLevel[namaLevelPack];

        if (_indexSoal + 2 > levelTerakhir)
        {
            _playerProgress.progresData.koin += 20;

            _playerProgress.progresData.progresLevel[namaLevelPack] = _indexSoal + 2;
            _playerProgress.SimpanProgres();
        }
    }

    public void NextLevel()
    {
        //index soal selanjutnya
        _indexSoal++;

        //Jika melampaui soal terakhit, ulangi dari awal.
        if(_indexSoal >= _soalSoal.BanyakLevel)
        {
            //_indexSoal = 0;
            _gameSceneManager.BukaScene(_namaScenePilihMenu);
            _playerProgress.SimpanProgres();
            return;
        }

        //Get data pertanyaan
        LevelSoalKuis soal = _soalSoal.AmbilLevelKe(_indexSoal);

        //set informasi soal
        _pertanyaan.SetPertanyaan($"Soal {_indexSoal + 1}" ,soal.pertanyaan, soal.petunjukJawaban);

        for(int i=0; i < _pilihanJawaban.Length; i++)
        {
            UI_PointJawaban poin = _pilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanText, opsi.adalahBenar);
        }

    }
}
