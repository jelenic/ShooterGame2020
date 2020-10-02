using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhasesAI : MonoBehaviour
{
    public int currentPhase;
    public Phase[] phases;

    private BossAIArcade[] allAIs;

    private CombatVariables cv;
    private Stats stats;

    private void Awake()
    {
        cv = GetComponent<CombatVariables>();
        stats = GetComponent<Stats>();

        allAIs = GetComponents<BossAIArcade>();

        cv.onHpChangedCallback += checkHP;

        allAIs[currentPhase].enabled = true;
    }

    private void checkHP(int amount, int current)
    {
        float hpPercentage = (float)current / stats.hp;
        int nextPhase = currentPhase;

        for(int i = currentPhase; i < phases.Length; i++)
        {
            float hpActivation_i = phases[i].hpPercentageActivation;
            Debug.LogFormat("current hp: {0}, i:{1}, phase[i] hp: {2} next: {3}", hpPercentage, i, hpActivation_i, nextPhase);
            if (hpPercentage <= hpActivation_i) nextPhase = i+1;
            else break;
        }

        if (nextPhase != currentPhase)
        {
            allAIs[currentPhase].enabled = false;
            allAIs[nextPhase].enabled = true;
            currentPhase = nextPhase;

            foreach(StatBuff sb in phases[currentPhase-1].phaseBuffs)
            {
                cv.permanentStatBuff(sb, 2f);
            }

        }
    }
}

[System.Serializable]
public class Phase
{
    public float hpPercentageActivation;
    public StatBuff[] phaseBuffs;
}