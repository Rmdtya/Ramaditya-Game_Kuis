using UnityEngine;
using TMPro;

public class UI_PointJawaban : MonoBehaviour
{
    [SerializeField]
    private UI_PesanLevel _tempatPesan = null;

    [SerializeField]
    private TextMeshProUGUI _textJawaban = null;

    [SerializeField]
    private bool adalahBenar = false;

    public void PilihJawaban()
    {
        _tempatPesan.Pesan = $"{_textJawaban.text} adalah {adalahBenar}";
    }

    public void SetJawaban(string textJawaban, bool kebenaran)
    {
        _textJawaban.text = textJawaban;
        adalahBenar = kebenaran;
    }
}
