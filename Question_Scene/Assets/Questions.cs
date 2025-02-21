using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
   
    public TMP_Text[] questions = new TMP_Text[2];
    public Button[] answer_buttons = new Button[4];
    public TMP_Text[] answer_text = new TMP_Text[4];
    List<string[]> questions_text = new List<string[]>();

    public Button Next_Button;

    private int position = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Read_CSV();
        questions[1].text = questions_text[2][1];
        for(int i = 0; i <4; i++)
        {
            answer_text[i] = answer_buttons[i].GetComponentInChildren<TMP_Text>();
            answer_text[i].text = questions_text[2][i+2];
            Set_Correct_Answer(2);
        }
        Next_Button.onClick.AddListener(Next_Click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Read_CSV()
    {
        StreamReader reader = new StreamReader("Assets/IBM_AI_Questions.csv");
        bool file_end = false;
        while(!file_end)
        {
            string string_data = reader.ReadLine();
            if(string_data == null)
            {
                file_end = true;
                break;
            }
            
            var data_values = string_data.Split(',');
            data_values.ToArray();

           questions_text.Add(data_values);
        }

    }

    void Next_Click()
    {
        position++;
        //ignore spaces without questions 
        if(position == 7 || position == 13 || position == 19|| position == 25|| position == 31){ position++; }
        questions[1].text = questions_text[position][1];

        for(int i = 0; i <4; i++)
        {
            answer_text[i] = answer_buttons[i].GetComponentInChildren<TMP_Text>();
            answer_text[i].text = questions_text[position][i+2];
            
            answer_buttons[i].onClick.RemoveAllListeners();
           Set_Correct_Answer(position);
        }
    }

    void Set_Correct_Answer(int position)
    {

        for(int i = 0; i <4; i++)
        {
            int index =i;
            answer_buttons[index].onClick.RemoveAllListeners();

            if(questions_text[position][i+2] == questions_text[position][6])
            {
                answer_buttons[index].onClick.AddListener(Correct_Answer);
                
            }
            else
            {
                answer_buttons[index].onClick.AddListener(Wrong_Answer);
            }
            
        }
        
        

    }

    void Correct_Answer()
    {
        Debug.Log("Correct");

    }
    void Wrong_Answer()
    {
        Debug.Log("Wrong");

    }




}
