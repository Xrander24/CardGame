using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]private int damage;
    [SerializeField]private EnemyList enemylist;
    [SerializeField]private int index_in_hand=0;
    [SerializeField]private string card_type;
    [SerializeField]private SpriteRenderer AttackSprite;
    [SerializeField]private TurnButton turnButton;
    [SerializeField]private Player player;
    [SerializeField]private bool isChosen=false;
    [SerializeField]private int light_damage,heavy_damage,heal_power;
    Card(int damage){
        this.damage=damage;
    }
    public int Index_in_hand{
        set{
            index_in_hand=value;
        }
    }
    public string Card_type{
        set{
            card_type=value;
        }
    }
    public void SetAttackSprite(Sprite sprite){
        AttackSprite.sprite=sprite;
    }
    public bool IsChosen{
        get{
            return isChosen;
        }
        set{
            isChosen=value;
        }
    }
    private void Light_attack(bool success){
        if(success){
            enemylist.TakeDamage(light_damage);
        }
        //else miss animation mb?
    }
    private void Heavy_attack(bool success){
        if(success){
            enemylist.TakeDamage(heavy_damage);
        }
        else{
            player.TakeDamage(heavy_damage);
        }
    }
    private void Defence(bool success){
        if(success){
            player.AddEffect("defence",1);// получить бафф брони на снижение следующего урона в n раз или типа того
        }
        else{
            player.AddEffect("broken_defence",1);//дебафф на увеличение след урона в n раз или типа того
        }
    }
    private void Poison(bool success){
        if(success){
            enemylist.AddEffect("poison",4);//наложить яд на врага на n ходов наносит x урона за ход
        }
        else{
            player.AddEffect("poison",4);
        }
    }
    private void Heal(bool success){
        if(success){
            player.Heal(heal_power);
        }
        else{
            enemylist.Heal(heal_power);
        }
    }
    private void UseAbility(string type,bool success){
        if(type=="light_attack"){
            Light_attack(success);
        }
        else if(type=="heavy_attack"){
            Heavy_attack(success);
        }
        else if(type=="heal"){
            Heal(success);
        }
        else if(type=="poison"){
            Poison(success);
        }
        else if(type=="defence"){
            Defence(success);
        }
    }
    public void UseCard(bool success){
        Debug.Log(success);
        UseAbility(card_type,success);
        gameObject.GetComponentInParent<CardHand>().DeleteCard(index_in_hand);
    }
    private void Choose(bool isChosen){
        //Debug.Log(isChosen);
        if(isChosen){
            gameObject.GetComponent<SpriteRenderer>().color=Color.white;
            turnButton.Chosencard=null;
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().color=Color.red;
            turnButton.Chosencard=this;
        }
        this.isChosen=(!isChosen);
    }
    private void OnMouseDown() {
        if(!turnButton.Is_pause){
            if(turnButton.Chosencard!=null&&turnButton.Chosencard!=this){
                turnButton.Chosencard.Choose(turnButton.Chosencard.isChosen);
            }
            Choose(isChosen);
        }
    }
}
