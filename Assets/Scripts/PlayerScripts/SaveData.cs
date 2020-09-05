using System.Collections;
using System.Linq;
using System.Collections.Generic;
[System.Serializable]
public class SaveData
{
    public int[] equipement;
    public string[] levelNames;
    public int[] levelScores;

    public SaveData(int[] equipement)
    {
        this.equipement = equipement;
    }

    public SaveData(Dictionary<string, int> levels_scores)
    {
        this.levelNames = levels_scores.Keys.ToArray();
        this.levelScores = levels_scores.Values.ToArray();
    }

}