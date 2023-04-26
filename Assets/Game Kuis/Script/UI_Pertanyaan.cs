using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Pertanyaan : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatJudul = null;

    [SerializeField]
    private TextMeshProUGUI tempatText = null;

    [SerializeField]
    private Image tempatGambar = null;

    public float waktu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPertanyaan(string judul, string textpertanyaan, Sprite gambarHint)
    {
        _tempatJudul.text = judul;
        tempatText.text = textpertanyaan;
        tempatGambar.sprite = gambarHint;
    }
}
