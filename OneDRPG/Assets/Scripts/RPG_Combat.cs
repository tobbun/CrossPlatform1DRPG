using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RPG_Combat : MonoBehaviour {

    public GameObject p1, p2;

    public Slider timerSlider;
    public Text timerTxt, roundTxt, p1ChoiceTxt, p2ChoiceTxt, resultTxt;
    public int combatTurn, timerTime;
    int  p1Choice, p2Choice;
    public float timer;
    public bool turnStarted; 


    /*
        Per turn, choose one:
- Attack
- Defend
- Idle

Attack
- Deals dmg = total ATK.
- @endOfTurn, 1 ATK spent.

Defend
- Negates dmg = total DEF.
- @endOfTurn, 1 DEF spent.

Idle

- Restores 1 ATK/DEF, up to player's max ATK/DEF
    */
/*
    void Start()//calls the StartCombatTurn method.
    {
        StartCombatTurn();
        turnStarted = true;
    }*/

    void Update()//calls the CountDown method.
    {
        if(turnStarted) { CountDown(); }
        
    }

    void CountDown()//ticks away at the timer, and calls the Resolve Turn when timer runs out.
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            timerSlider.value = timer;
            timerTxt.text = "Time left: "+timer.ToString("F2");
            //Debug.Log(turnStarted);
        } else if (timer <= 0) { StartEndPhaseOfTurn(); }
    }

    public RPG_Combat(GameObject redCorner, GameObject blueCorner) {
        p1 = redCorner;
        p2 = blueCorner;
    }

    void StartCombatTurn()//sets the start of the combat turn
    {
        //tick off next turn
        turnStarted = false;
        combatTurn++;
        roundTxt.text = "Turn: " + combatTurn;
        //start timer
        //StartCombatTimer();
        //resets choices like Defend etc
        p1Choice = 0; p1ChoiceTxt.text = "You're currently idling.";
        p1.GetComponent<RPG_Stats>().defend = false;
        p2.GetComponent<RPG_Stats>().defend = false;

        //queue in npc choice

        DecideForP2();

        switch (p2Choice)
        {
            case 1:
                p2ChoiceTxt.text = "The enemy chose Offence.";
                break;
            case 2:
                p2ChoiceTxt.text = "The enemy chose Defence.";
                break;

            case 0:
                p2ChoiceTxt.text = "The enemy idles.";
                break;

            default:
                p2ChoiceTxt.text = "The enemy don't know what to do.";
                break;
        }

    }

    void StartEndPhaseOfTurn() //calls CheckP1Choice and CheckP2Choice, then starts the next combat turn.
    {
        CheckP1Choice();//the player 1 actions are resolved

        CheckP2Choice();//the player 2 actions are resolved

        ResolveResources();//the resource use is resolved

        CheckIfSomeoneDied(); //calls this method to check if one of the combatants died, and then end the combat.
        
        StartCombatTurn();//starts the next combat turn

        


    }

    void ResolveResources()
    {
        

        switch (p1Choice)//checks the player's choice
        {
            case 0://if idle then idle
                //p1.GetComponent<RPG_Stats>().Idle();
                break;

            case 1://defend
                //p1.GetComponent<RPG_Stats>().defend = true;
                p1.GetComponent<RPG_Stats>().UseDEFEND();
                break;

            case 2://if attack then attack
                //p2.GetComponent<RPG_Stats>().Ow(p1.GetComponent<RPG_Stats>().atk);//calls the target's damage method, using the player's attack as parameter
                p1.GetComponent<RPG_Stats>().UseATTACK();
                break;

            default:
                Debug.Log("What, P1");
                break;
        }
        switch (p2Choice)
        {
            case 0:
                //p2.GetComponent<RPG_Stats>().Idle();
                break;

            case 1:
                //p1.GetComponent<RPG_Stats>().Ow(p2.GetComponent<RPG_Stats>().atk);
                p2.GetComponent<RPG_Stats>().UseATTACK();
                break;

            case 2:
                //p2.GetComponent<RPG_Stats>().defend = true;
                p2.GetComponent<RPG_Stats>().UseDEFEND();
                break;

            default:
                Debug.Log("P2 What");
                break;
        }

    }

    void CheckIfSomeoneDied()
    {
        if (p1.GetComponent<RPG_Stats>().dead && !p2.GetComponent<RPG_Stats>().dead) { YouLose(); }
        if (!p1.GetComponent<RPG_Stats>().dead && p2.GetComponent<RPG_Stats>().dead) { YouWin(); }
        if (p1.GetComponent<RPG_Stats>().dead && p2.GetComponent<RPG_Stats>().dead) { ItsADraw(); }
    }

    void CheckP1Choice()
    {
        switch (p1Choice)//checks the player's choice
        {
            case 0://if idle then idle
                p1.GetComponent<RPG_Stats>().Idle();
                break;
            
            case 1://defend
                p1.GetComponent<RPG_Stats>().defend = true;
                //p1.GetComponent<RPG_Stats>().UseDEFEND();
                break;

            case 2://if attack then attack
                p2.GetComponent<RPG_Stats>().Ow(p1.GetComponent<RPG_Stats>().atk);//calls the target's damage method, using the player's attack as parameter
                //p1.GetComponent<RPG_Stats>().UseATTACK();
                break;

            default:
                Debug.Log("What, P1");
                break;
        }
    }//checks the P1choice and calls the appropriate methods

    void CheckP2Choice()//checks the P2choice and calls the appropriate methods
    {
        switch (p2Choice)
        {
            case 0:
                p2.GetComponent<RPG_Stats>().Idle();
                break;
            
            case 1:
                p1.GetComponent<RPG_Stats>().Ow(p2.GetComponent<RPG_Stats>().atk);
               // p2.GetComponent<RPG_Stats>().UseATTACK();
                break;

            case 2:
                p2.GetComponent<RPG_Stats>().defend = true;
                //p2.GetComponent<RPG_Stats>().UseDEFEND();
                break;

            default:
                Debug.Log("P2 What");
                break;
        }
    }

    void DecideForP2()//generates an integer between 0 and 2 and sets p2Choice to the result.
    {
        p2Choice = Random.Range(0,2);
    }

    public void Option1()//gets called by button that covers one half of screen
    {
        //sets player choice to 1
        p1Choice = 1;
        p1.GetComponent<RPG_Stats>().defend = true;//the character is now defending
        p1ChoiceTxt.text = "You've chosen to Defend.";
        //Resolve();
        //calls p1 atk stat, calls p2.Ow method with p1 atk stat.

        StartCombatTimer();
    }

    public void Option2()//gets called by button that covers the other half of screen
    {
        //sets player choice to 2
        p1Choice = 2;
        p1ChoiceTxt.text = "You've chosen to Attack.";
        //Resolve();
        //calls p1 def stat

        StartCombatTimer();
    }

    void StartCombatTimer()//says that the turn has started and sets the timer to 7 seconds. 
    {               
        timer = timerTime;
        turnStarted = true;
    }
    void ItsADraw()
    {
        resultTxt.text = "Both of you died. It's a dead draw.";
        EndMatch();
    }

    void YouLose()//p1 has died and therefore loses the battle
    {
        resultTxt.text = "You died. And therefore you lost.";
        //turnStarted = false; 
        EndMatch();
    }

    void YouWin()//p2 has died and therefore p1 wins the battle
    {
        resultTxt.text = "You have slain your foe! You win!";
       // turnStarted = false;
        EndMatch();
    }

    void EndMatch()//ends the battle, possibly loads in another scene?
    {

        turnStarted = false;
        Debug.Log(turnStarted);
    }
}
