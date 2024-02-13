using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    [SerializeField]private List<Enemy> enemylist;
    [SerializeField]private HealthBar hp_bar;
    [SerializeField]private TurnButton turnButton;
    private int currentindex;
    private void Start() {
        currentindex=-1;
        SelectNewEnemy();
    }
    public void TakeDamage(int damage){
        enemylist[currentindex].TakeDamage(damage,hp_bar);
    }
    public void UsePoison(int poison_damage){
        enemylist[currentindex].UsePoison(poison_damage,hp_bar);
    }
    public void AddEffect(string effect,int turns){
        enemylist[currentindex].AddEffect(effect,turns);
    }
    public void Heal(int hp){
        enemylist[currentindex].Heal(hp,hp_bar);
    }
    public void SelectNewEnemy(){
        turnButton.DisableCanvas();
        if(currentindex==enemylist.Count-1){
            turnButton.Victory();
        }
        else{
            currentindex+=1;
            enemylist[currentindex].Appear(hp_bar);
        }
    }
    public void Restart(){
        Start();
    }
}
