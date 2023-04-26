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

    private int _indexSoal = -1;

    private void Start()
    {

        /*if (!_playerProgress.MuatProgres())
        {
            _playerProgress.SimpanProgres();
        }*/

        _soalSoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;
        
        NextLevel();

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
        if (adalahBenar)
        {
            _playerProgress.progresData.koin += 20;
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
