using UnityEngine;

[CreateAssetMenu(
        fileName = "inisial Data Gameplay",
        menuName = "Game Kuis/Inisial Data Gameplay")]
public class InisialDataGameplay : ScriptableObject
{
    public LevelPackKuis levelPack = null;
    public int levelIndex = 0;

    [SerializeField]
    private bool _saatKalah = false;

    public bool SaatKalah
    {
        get => _saatKalah;
        set => _saatKalah = value;
    }
}
