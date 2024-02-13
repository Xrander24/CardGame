using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private int health;
    [SerializeField]private const int max_health=100;
    [SerializeField]private HealthBar hp_bar;
    [SerializeField]private StatusEffectCreator stat_eff_creator;
    [SerializeField]private TurnButton turnButton;
    [SerializeField]private Dictionary<string,(int,StatusEffect)> effects= new Dictionary<string,(int,StatusEffect)>();
    private void Start() {
        health=max_health;
        hp_bar.ChangeHealth(health,max_health);
    }
    public void RemoveEffect(string effect){
        Destroy(effects[effect].Item2.gameObject);
        effects.Remove(effect);
    }
    private void DecreaceEffect(string effect,int stacks){
        if(effects.ContainsKey(effect)){
            (int,StatusEffect) ise=effects[effect];
            ise.Item1-=stacks;
            ise.Item2.setEffect(effect,ise.Item1);
            effects[effect]=ise;
            if(effects[effect].Item1<=0){
                RemoveEffect(effect);
            }
        }
    }
    public void AddEffect(string effect,int turns){
        if(effects.Count!=0){
            if(effect=="defence"){
                if(effects.ContainsKey("broken_defence")){
                    (int,StatusEffect) ise= effects["broken_defence"];
                    if(turns>=ise.Item1){
                        turns-=ise.Item1;
                        RemoveEffect("broken_defence");
                    }
                    else{
                        ise.Item1-=turns;
                        ise.Item2.setEffect("broken_defence",ise.Item1);
                        effects["broken_defence"]=ise;
                        turns=0;
                    }
                }
            }
            else if(effect=="broken_defence"){
             if(effects.ContainsKey("defence")){
                (int,StatusEffect) ise= effects["defence"];
                    if(turns>=ise.Item1){
                        turns-=ise.Item1;
                        RemoveEffect("defence");
                    }
                    else{
                        ise.Item1-=turns;
                        ise.Item2.setEffect("defence",ise.Item1);
                        effects["defence"]=ise;
                        turns=0;
                    }
                }
            }
        }
        if(turns!=0){
            if(effects.ContainsKey(effect)){
                (int,StatusEffect) x= effects[effect];
                x.Item1+=turns;
                effects[effect]=x;
                effects[effect].Item2.setEffect(effect,x.Item1);
            }
            else{
                effects[effect]=(turns, stat_eff_creator.AddEffect(effect,turns));
            }
        }
    }
    public void UsePoison(int poison_damage){
        if(effects.ContainsKey("poison")){
            health-=poison_damage;
            DecreaceEffect("poison",1);
            hp_bar.ChangeHealth(health,max_health);
            if(health<=0){
                health=0;
                Death();
            }
        }    
    }
    public void TakeDamage(int damage){
        if(effects.ContainsKey("broken_defence")){
            damage*=2;
            DecreaceEffect("broken_defence",1);
        }
        else if(effects.ContainsKey("defence")){
            damage/=2;
            DecreaceEffect("defence",1);
        }
        health-=damage;
        hp_bar.ChangeHealth(health,max_health);
        if(health<=0){
            health=0;
            Death();
        }
    }
    public void Heal(int hp){
        health=Mathf.Min(max_health,health+hp);
        hp_bar.ChangeHealth(health,max_health);
        if(effects.ContainsKey("poison")){
            RemoveEffect("poison");
        }
    }
    private void Death(){
        Debug.Log("Player is dead. You lose!");
        turnButton.Defeat();
    }
    public void Restart(){
        if(effects.ContainsKey("poison")){
            RemoveEffect("poison");
        }
        if(effects.ContainsKey("broken_defence")){
            RemoveEffect("broken_defence");
        }
        if(effects.ContainsKey("defence")){
            RemoveEffect("defence");
        }
        Start();
    }
}
