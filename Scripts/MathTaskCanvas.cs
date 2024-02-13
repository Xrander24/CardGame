using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathTaskCanvas : MonoBehaviour
{
    [SerializeField]private Text math_task_text;
    [SerializeField]private InputField input_answer;
    [SerializeField]private Button accept_answer_button;
    [SerializeField]private Button information_button;
    [SerializeField]private TurnButton turnButton;
    private string correct_ans;
    public void Disable(){
        information_button.GetComponent<Button>().interactable=true;
        turnButton.Is_pause=false;
        input_answer.text="";
        gameObject.SetActive(false);
    }
    public void onButtonClick(){
        bool success;
        if(input_answer.text==correct_ans){
            success=true;
        }
        else{
            success=false;
        }
        information_button.GetComponent<Button>().interactable=true;
        turnButton.Is_pause=false;
        turnButton.Chosencard.UseCard(success);
        input_answer.text="";
        gameObject.SetActive(false);
    }
    public void Appear(){
        GenerateTask();
        information_button.GetComponent<Button>().interactable=false;
    }
    private void GenerateTask(){
        string new_text="";
        int operation_type= Random.Range(1,5);
        int num1=Random.Range(-10000,10001),num2=Random.Range(-10000,10001),num3=0;
        string num2str;
        switch(operation_type){
            case 1:
                num3=num1+num2;
                if(num2<=0){
                    num2str="( "+num2.ToString()+" )";
                }
                else{
                    num2str=num2.ToString();
                }
                new_text=num1.ToString()+" + "+num2str+" = ?";
                break;
            case 2:
                num3=num1-num2;
                if(num2<=0){
                    num2str="( "+num2.ToString()+" )";
                }
                else{
                    num2str=num2.ToString();
                }                
                new_text=num1.ToString()+" - "+num2str+" = ?" ;
                break;
            case 3:
                if(num1*num2>100000||num1*num2<-100000){
                    num1=Random.Range(-1000,1001);
                    num2=Random.Range(-100,101);
                }
                if(num2<=0){
                    num2str="( "+num2.ToString()+" )";
                }
                else{
                    num2str=num2.ToString();
                }                
                num3=num1*num2;
                new_text=num1.ToString()+" * "+num2str+" = ?";
                break;
            case 4:
                if(num2==0||(num2)>1000){
                    num2=Random.Range(1,1001);
                }
                if(num1<num2&&num1>-num2){
                    num1=Random.Range(Mathf.Abs(num2),10001);
                }
                num3=num1/num2;
                num1=num3*num2;
                if(num2<=0){
                    num2str="( "+num2.ToString()+" )";
                }
                else{
                    num2str=num2.ToString();
                }                
                new_text=num1.ToString()+" / "+num2str+" = ?";
                break;        
        }
        math_task_text.text=new_text;
        correct_ans=num3.ToString();
    }
}
