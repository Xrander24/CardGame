using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnButton : MonoBehaviour
{
    [SerializeField] private CardHand cardhand;
    private bool is_pause=false;
    [SerializeField] private int poison_damage;
    [SerializeField] private Player player;
    [SerializeField] private EnemyList enemyList;
    [SerializeField] private GameObject information_panel;
    [SerializeField] private GameObject game_over_panel;
    [SerializeField] private GameObject victory_panel;
    private bool inf_is_active=false;
    [SerializeField] private GameObject math_canvas;
    private Card chosencard;
    public bool Is_pause{
        set{
            is_pause=value;
        }
        get{
            return is_pause;
        }
    }
    public Card Chosencard{
        set{
            chosencard =value;
        }
        get{
            return chosencard;
        }
    }
    public void Victory(){
        is_pause=true;
        victory_panel.SetActive(true);
    }
    public void Defeat(){
        math_canvas.GetComponent<MathTaskCanvas>().Disable();
        is_pause=true;
        game_over_panel.SetActive(true);
    }
    public void DisableCanvas(){
        math_canvas.GetComponent<MathTaskCanvas>().Disable();
    }
    public void RestartGame(){
        is_pause=false;
        game_over_panel.SetActive(false);
        victory_panel.SetActive(false);
        cardhand.Restart();
        player.Restart();
        enemyList.Restart();
    }
    public void Information(){
        if(is_pause){
            is_pause=false;
            information_panel.SetActive(false);
        }
        else{
            is_pause=true;
            information_panel.SetActive(true);
        }
    }

    private void OnMouseDown() {
        if(!is_pause){
            if(chosencard!=null){
                is_pause=true;
                math_canvas.SetActive(true);
                math_canvas.GetComponent<MathTaskCanvas>().Appear();
            }
            else{
                cardhand.AddNewCard();
            }
            player.UsePoison(poison_damage);
            enemyList.UsePoison(poison_damage);
        }
    }
}
