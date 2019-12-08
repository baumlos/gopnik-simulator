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
    private int bearTarget = 0;
    private int playerTarget = 0;
    // Start is called before the first frame update
    void Start(){
        playerHP=startingLives;
        bearHP=startingLives;
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
        attacks[1].GetComponent<AttackScript>().setT(new Vector3(-7,(-4*bearTarget),0));
        attacks[1].SetActive(true);
        if(bearTarget!=playerTarget){
            playerHit();
        }
        else if(debug) Debug.Log("bear misses");
        playerDefender = false;
        intermission = true;
    }
    void playerAttack(){
        if(debug) Debug.Log("Player attacks pos "+playerTarget);
        attacks[0].GetComponent<AttackScript>().setT(new Vector3(7,(-4*playerTarget),0));
        attacks[0].SetActive(true);
        if(playerTarget!=bearTarget){
            bearHit();
        }
        else if(debug) Debug.Log("player misses");
        playerDefender = true;
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
        bearHP--;
        slider[3].setCurrentValue(bearHP);
        if(bearHP==0){
            gameOver(0);
        }
    }
    void gameOver(int winner){
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
            q = Quaternion.Euler(0,0,180+rot);
            GameObject.FindGameObjectWithTag("Bear Head").transform.rotation = q;
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
        attacks[0].GetComponent<AttackScript>().setS(new Vector3(-7,-4,0));
        attacks[1].GetComponent<AttackScript>().setS(new Vector3(7,-4,0));
    }
    public void def(int player, int pos){
        int y= 0;
        switch(pos){
            case 0:
            y = 0;
            break;
            case 1:
            y = -4;
            break;
            case 2:
            y = -8;
            break;
            default:
            y = 4;
            break;
        }
        if(player == 0){
            this.player[3].transform.position = new Vector3(-6,y,0);
            if(debug) Debug.Log("Spieler verteidigt pos "+pos);
        }
        else {
            bear[3].transform.position = new Vector3(6,y,0);
            if(debug) Debug.Log("Bär verteidigt pos "+pos);
        }
    }
    void switchDef(){
        if(playerDefender){
            player[3].SetActive(true);
            bear[3].SetActive(false);
            slider[0].setActive(true);
        }
        else{
            player[3].SetActive(false);
            bear[3].SetActive(true);
            slider[1].setActive(true);
        }
    }
    public Vector3 genTarget(){
        int x,y;
        if(playerDefender) {
            x = -9;
            y = (-4 * bearTarget) +4;
        }
        else {
            x = 9;
            y = (-4 * playerTarget) + 4;
        }
        return new Vector3(x,y,0);

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
            def(0,0);
        }
    }
    public void clickedPlayerBody(){
        if(playerDefender){
            def(0,1);
        }
    }
    public void clickedPlayerFeet(){
        if(playerDefender){
            def(0,2);
        }
    }


}
