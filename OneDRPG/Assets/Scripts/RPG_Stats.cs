using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RPG_Stats : MonoBehaviour {

    public int hp, atk, def; //current health, defence and attack
    public bool defend, attack, dead, player;
    public int maxHP, maxATK, maxDEF;
    
    //stat feedback
    public Text hpTxt, defTxt, atkTxt;

    //damage feedback
    public Image damageImage;
    public Color flashColour;

    bool damaged;
    int flashSpeed = 2;

    public void UseATTACK() { atk--; if (atk <= 0) atk = 0; }//atk--?

    public void UseDEFEND() { def--; if (def <= 0) def = 0; }//def--?

    void Start()
    {
        hp = maxHP;
        atk = maxATK;
        def = maxDEF;
    }

    void Update()
    {
        hpTxt.text = "HP: " + hp;
        defTxt.text = "DEF: " + def;
        atkTxt.text = "ATK: " + atk;

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;

    }

    public void Ow(int pain) {
        if (defend)
        {
            Debug.Log("The Attack's Strength is: " + pain);
            pain = pain - def;
            Debug.Log("The Damage is reduced by: " + def+ ". The Resulting Damage is: "+pain);
            
        }
        damaged = true;
        hp = hp - pain;
        if (hp <= 0) {
            hp = 0; dead = true;
        }
    }

    //public void Yay(int heals) { hp = hp + heals; }

    public void Idle() { atk++; def++;
        if (atk > maxATK) { atk = maxATK; }
        if (def > maxDEF) { def = maxDEF; }
    }//restores one atk and one def, and you know what whatever I'll clean this up later

    
	
}
