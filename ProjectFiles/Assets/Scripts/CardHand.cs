using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    [SerializeField]private List<GameObject> hand;
    private int handsize=0;
    static string[] s={"light_attack","defence","heavy_attack","poison","heal"};
    [SerializeField]private List<Sprite> sprite_list;// как в card_types
    [SerializeField]private List<Sprite> card_text;
    private List<string> card_types= new List<string>(s);
    [SerializeField]private GameObject origcard;
    private void Start() {
        card_types=new List<string>(s);
        for(int i=handsize-1;i>=0;--i){
            DeleteCard(i);
        }
        handsize=0;
        for(int i=0;i<4;++i){
            AddNewCard();
        }
    }
    public void Restart(){
        Start();
    }
    private string Random_Type(){
        if(card_types.Count==0){
            card_types=new List<string>(s);
        }
        int rand_t=Random.Range(0,card_types.Count);
        string type = card_types[rand_t];
        card_types.Remove(type);
        return type;
    }
    public void AddNewCard(){
        if(handsize<=8){ 
            Vector3 pos;
            if(handsize==0){
                pos=origcard.GetComponent<Transform>().position;
                pos.x+=origcard.GetComponent<Transform>().localScale.x+0.3f;
            }
            else{
                pos=hand[handsize-1].GetComponent<Transform>().position;
                pos.x+=hand[handsize-1].GetComponent<Transform>().localScale.x+0.3f;
            }
            GameObject card = Instantiate(origcard, pos,Quaternion.identity,transform) as GameObject;
            card.SetActive(true);
            string card_type =Random_Type();
            card.GetComponent<Card>().Card_type= card_type;
            var dict= new Dictionary<string,int>(){
                [s[0]]=0,
                [s[1]]=1,
                [s[2]]=2,
                [s[3]]=3,
                [s[4]]=4,
            };
            card.GetComponent<Card>().SetAttackSprite(sprite_list[dict[card_type]]);
            card.GetComponent<SpriteRenderer>().sprite=card_text[dict[card_type]];
            card.name="card"+handsize.ToString();
            card.GetComponent<Card>().Index_in_hand=handsize;           
            hand.Add(card);
            handsize+=1;
        }
    }
    public void DeleteCard(int index){
        for(int i=index+1;i<handsize;++i){
            Transform card_transform=hand[i].GetComponent<Transform>();
            hand[i].GetComponent<Card>().Index_in_hand=i-1;
            card_transform.Translate(new Vector3(-0.3f-card_transform.localScale.x,0,0));
        }
        Destroy(hand[index]);
        hand.Remove(hand[index]);
        handsize-=1;

    }
}
