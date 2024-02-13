using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffect : MonoBehaviour
{
    [SerializeField]private Image icon;
    [SerializeField]private Text status_effect_text;
    [SerializeField]private int turns;
    [SerializeField]private string effect;
    [SerializeField]private List<Sprite> sprites; //1-poison 2-defence 3-brokendefence
    public string Effect{
        get{
            return effect;
        }
    }
    public int Turns{
        set{
            setEffect(effect,value);
        }
        get{
            return turns;
        }
    }
    public void setEffect(string effect,int turns){
        this.turns=turns;
        this.effect=effect;
        if(effect=="poison"){
            icon.sprite=sprites[0];
            status_effect_text.text="Яд - наносит 7 единиц урона за ход. Осталось ходов: "+turns.ToString();
        }
        else if(effect=="defence"){
            icon.sprite=sprites[1];
            status_effect_text.text="Броня - уменьшает полученный урон в 2 раза. Осталось ударов: "+turns.ToString();
        }
        else if(effect=="broken_defence"){
            icon.sprite=sprites[2];
            status_effect_text.text="Сломанная броня - увеличивает полученный урон в 2 раза. Осталось ударов: "+turns.ToString();
        }
    }
}
