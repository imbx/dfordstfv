using UnityEngine;

[CreateAssetMenu(fileName = "ArchivePage", menuName = "BoxScripts/ArchivePage", order = 0)]
public class ArchivePage : ScriptableObject {

    public string Language = "ES";
    public string Identifier = "ARCHIVE_01";
    public Sprite Front;
    public Sprite Back;
    public ArchiveButton Button;
    public bool isActive = false;
}