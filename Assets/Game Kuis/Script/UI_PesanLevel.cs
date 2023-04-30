using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_PesanLevel : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private GameObject _opsiMenang = null;

    [SerializeField]
    private GameObject _opsiKalah = null;

    [SerializeField]
    private TextMeshProUGUI _tempatPesan = null;

    public string Pesan
    {
        get => _tempatPesan.text;
        set => _tempatPesan.text = value;
    }

    private void Awake()
    {
        //untuk mematikan halaman pesan level
        if (gameObject.activeSelf)
            gameObject.SetActive(false);

        UI_Timer.EventWaktuHabis += UI_Timer_EventWaktuHabis;
        UI_PointJawaban.EventJawabSoal += UI_PointJawaban_EventJawabSoal;
    }
    private void OnDestroy()
    {
        UI_Timer.EventWaktuHabis -= UI_Timer_EventWaktuHabis;
        UI_PointJawaban.EventJawabSoal -= UI_PointJawaban_EventJawabSoal;
    }

    private void UI_PointJawaban_EventJawabSoal(string jawabanText, bool adalahBenar)
    {
        Pesan = $"Jawaban Anda {adalahBenar} (jawab : {jawabanText})";
        gameObject.SetActive(true);

        if (adalahBenar)
        {
            _opsiMenang.SetActive(true);
            _opsiKalah.SetActive(false);
        }
        else
        {
            _opsiMenang.SetActive(false);
            _opsiKalah.SetActive(true);
        }

        _animator.SetBool("Menang", adalahBenar);
    }

    private void UI_Timer_EventWaktuHabis(){
        Pesan = "Waktu Sudah Habis !!!";
        gameObject.SetActive(true);

        _opsiMenang.SetActive(false);
        _opsiKalah.SetActive(true);
    }


}