using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private LevelPackKuis _soalSoal = null;

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;

    [SerializeField]
    private UI_PointJawaban[] _pilihanJawaban = new UI_PointJawaban[0];

    private int _indexSoal = -1;

    private void Start()
    {

        if (!_playerProgress.MuatProgres())
        {
            _playerProgress.SimpanProgres();
        }
        
        NextLevel();
    }

    public void NextLevel()
    {
        //index soal selanjutnya
        _indexSoal++;

        //Jika melampaui soal terakhit, ulangi dari awal.
        if(_indexSoal >= _soalSoal.BanyakLevel)
        {
            _indexSoal = 0;
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
