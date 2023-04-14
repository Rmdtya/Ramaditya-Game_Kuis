using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_PesanLevel : MonoBehaviour
{
    // Start is called before the first frame update

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
    }
}