using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScript : MonoBehaviour
{
    private bool fightActive = false;
    private bool intermission = false;
    private bool playerDefender = true;
    public static int playerHP,bearHP;
    public int startingLives = 5;
    public float waitTime;
    public float intermissionTime;
    private float timer = 0;
    private bool debug = false;
    private float debugtimer;
    public GameObject[] player = new GameObject[3];
    public GameObject[] bear = new GameObject[3];
    public FightSliderScript[] slider;
    public GameObject[] attacks;
    public AudioSource[] sounds;
    private int bearTarget = 0;
    private int playerTarget = 0;
    public GameObject winScreen, loseScreen;
    // Start is called before the first frame update
    void Start(){
        playerHP=startingLives;
        bearHP=startingLives;
        firstStart();
    }

    // Update is called once per frame
    void Update(){
        if(debug)debugtimer += Time.deltaTime;
        if(fightActive&&!intermission){
            if(playerDefender){
                timer += Time.deltaTime;
                slider[0].setCurrentValue(timer);
                if(timer<waitTime) return;
                timer=0;
                bearAttack();
            }
            else{
                timer += Time.deltaTime;
                slider[1].setCurrentValue(timer);
                if(timer<waitTime) return;
                timer=0;
                playerAttack();
            }
        }
        else if(intermission){
            timer += Time.deltaTime;
            if(timer > intermissionTime){
                switchDef();
                bearChoose();
                intermission = false;
                timer = 0;
            }
        }
    }

    void bearChoose(){
        int n = Random.Range(0,3);
        bearTarget = n;
        if(debug) Debug.Log("Bear sucht sich "+n+" aus");
        if(playerDefender) turnHead(1,n);
        else def(1,n);
    }
    void bearAttack(){
        if(debug) Debug.Log("Bear attacks pos "+ bearTarget);
        attacks[1].GetComponent<AttackScript>().setT(new Vector3(-9,(-4*bearTarget)+2,0));
        attacks[1].SetActive(true);
        sounds[Random.Range(0,2)].Play(0);
        if(bearTarget!=playerTarget){
            playerHit();
        }
        else if(debug) Debug.Log("bear misses");
        playerDefender = false;
        intermission = true;
    }
    void playerAttack(){
        if(debug) Debug.Log("Player attacks pos "+playerTarget);
        attacks[0].GetComponent<AttackScript>().setT(new Vector3(9,(-4*playerTarget)+2,0));
        attacks[0].SetActive(true);
        if(playerTarget!=bearTarget){
            bearHit();
        }
        else if(debug) Debug.Log("player misses");
        playerDefender = true;
        def(0,playerTarget);
        intermission = true;
    }
    void playerHit(){
        if(debug) Debug.Log("Player hit");
        playerHP--;
        slider[2].setCurrentValue(playerHP);
        if(playerHP==0){
            gameOver(1);
        }
    }
    void bearHit(){
        if(debug) Debug.Log("Bear hit");
        sounds[Random.Range(2,4)].Play(0);
        bearHP--;
        slider[3].setCurrentValue(bearHP);
        if(bearHP==0){
            gameOver(0);
        }
    }
    void gameOver(int winner){
        sounds[4+winner].Play(0);
        if(winner==0) Instantiate(winScreen);
        else Instantiate(loseScreen);
        fightActive = false;
        if(debug) Debug.Log("Player "+winner+" wins");
    }
    public void setPlayerTarget(int n){
        playerTarget = n;
        turnHead(0,n);
    }
    public bool getPlayerDefender(){
        return playerDefender;
    }
    void turnHead(int player, int pos){     
        
        if(debug) Debug.Log("Player "+player+" turns head to "+pos);   
        Quaternion q = new Quaternion();
        float rot;
        switch(pos){
            case 0: rot = 0;
            break;
            case 1: rot = -10;
            break;
            case 2: rot = -20;
            break;
            default: rot = 180;
            Debug.Log("Falsche Rotation von player "+ player);
            break;
        }
        if(player == 1) {
            rot*=-1;
            q = Quaternion.Euler(0,0,rot);
            bear[0].transform.rotation = q;
            }

        else{
            q = Quaternion.Euler(0,0,rot);
            this.player[0].transform.rotation = q;
            }

    }
    public void enableDebug(){
        debug = true;
        debugtimer = 0f;
        Debug.Log("Debug start");
        firstStart();
    }
    void firstStart(){
        fightActive = true;
        bearChoose();
        switchDef();
        playerDefender = true;
        setPlayerTarget(1);
        attacks[0].GetComponent<AttackScript>().setS(new Vector3(-7,-4,0));
        attacks[1].GetComponent<AttackScript>().setS(new Vector3(7,-4,0));
    }
    public void def(int player, int pos){
        int y= 0;
        switch(pos){
            case 0:
            y = 2;
            break;
            case 1:
            y = -2;
            break;
            case 2:
            y = -6;
            break;
            default:
            y = 4;
            break;
        }
        if(player == 0){
            this.player[1].transform.position = new Vector3(-10,y,0);
            if(debug) Debug.Log("Spieler verteidigt pos "+pos);
        }
        else {
            bear[1].transform.position = new Vector3(10,y,0);
            if(debug) Debug.Log("Bär verteidigt pos "+pos);
        }
    }
    void switchDef(){
        if(playerDefender){
            player[1].SetActive(true);
            bear[1].SetActive(false);
            slider[0].setActive(true);
        }
        else{
            player[1].SetActive(false);
            bear[1].SetActive(true);
            slider[1].setActive(true);
        }
    }
    public void clickedBearHead(){
        if(!playerDefender){
            setPlayerTarget(0);
        }
    }
    public void clickedBearBody(){
        if(!playerDefender){
            setPlayerTarget(1);
        }

    }
    public void clickedBearFeet(){
        if(!playerDefender){
            setPlayerTarget(2);
        }

    }
    public void clickedPlayerHead(){
        if(playerDefender){
            playerTarget = 0;
            def(0,0);
        }
    }
    public void clickedPlayerBody(){
        if(playerDefender){
            playerTarget = 1;
            def(0,1);
        }
    }
    public void clickedPlayerFeet(){
        if(playerDefender){
            playerTarget = 2;
            def(0,2);
        }
    }


}
