using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatVariables : MonoBehaviour
{
    private Stats stats;

    public int hp;

    public Image hpBar;

    private Dictionary<string, float> resistances;

    public GameObject floatingNumberText;

    Transform transform;

    private LevelManager levelManager;

    public bool involunrable;


    public void createFloatingNumberText(Vector2 position, Color color, string text = "oops")
    {
        if (involunrable)
        {
            Debug.Log("involunrable");
            return;
        }
        else
        {
            if (color == null) color = Color.white;
            GameObject floatingNumber = Instantiate(floatingNumberText, position, Quaternion.identity);
            TextMesh tm = floatingNumber.GetComponent<TextMesh>();
            tm.text = text;
            tm.color = color;
        }

    }


    public int DecreaseHP(int amount, string dmgType = "default")
    {
        if (involunrable)
        {
            Debug.Log("involunrable");
            return hp;
        }
        else
        {
            int receivedDmg = (int)Math.Round(amount * (1f - resistances[dmgType]));
            Debug.LogFormat("{0} received {1} dmg of type {2}, original amount: {3}", stats.name, receivedDmg, dmgType, amount);
            hp = Math.Max(0, hp - receivedDmg);
            createFloatingNumberText(transform.position, Color.red, receivedDmg.ToString());
            if (hp == 0) Destroy(gameObject);
            hpBar.enabled = true;
            hpBar.fillAmount = (float)hp / stats.hp;
            return hp;
        }
    }

    public int InreaseHP(int amount)
    {
        hp = Math.Min(stats.hp, hp + amount);
        //Debug.LogFormat("object {0} hp increased by {1}, current hp: {2}", gameObject.tag, amount, hp);
        hpBar.fillAmount = (float)hp / stats.hp;
        return hp;
    }
    void Start()
    {
        floatingNumberText = Resources.Load("FloatingNumberText") as GameObject;
        transform = GetComponent<Transform>();
        stats = GetComponent<Stats>();
        hp = stats.hp;
        //Debug.LogFormat("total hp: {0}", stats.hp);
        resistances = new Dictionary<string, float>();
        resistances.Add("projectile", stats.projectileResistance);
        resistances.Add("beam", stats.beamResistance);
        resistances.Add("physical", stats.physicalResistance);
        resistances.Add("default", 0f);
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        involunrable = false;
    }

    private void OnDestroy()
    {
        levelManager.increaseScore(stats.scoreValue);
    }


}
