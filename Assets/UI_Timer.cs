using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    [SerializeField]
    private UI_PesanLevel _tempatPesan = null;

    [SerializeField]
    private float _waktuJawab = 30f;

    [SerializeField]
    private Slider _timeBar = null;

    private float _sisaWaktu = 0f;
    private bool _waktuBerjalan = true;

    public bool WaktuBerjalan
    {
        get => _waktuBerjalan;
        set => _waktuBerjalan = value;
    }

    private void Start()
    {
        UlangiWaktu();
    }

    private void Update()
    {

        if (!_waktuBerjalan)
        return;

        _sisaWaktu -= Time.deltaTime;
        _timeBar.value = _sisaWaktu / _waktuJawab;

        if(_sisaWaktu <= 0f)
        {

            _tempatPesan.Pesan = "Waktu Sudah Habis!!!";
            _tempatPesan.gameObject.SetActive(true);
            //Debug.Log("Waktu Habis");
            _waktuBerjalan = false;
            return;
        }

        Debug.Log("Sisa Waktu");
    }
    public void UlangiWaktu()
    {
        _sisaWaktu = _waktuJawab;
    }
}
