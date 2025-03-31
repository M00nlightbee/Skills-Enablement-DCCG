using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.IO;
using SkillBuildGame;
using System.Collections;

public class Questions : MonoBehaviour
{
	public TMP_Text[] questions = new TMP_Text[2];
	public Button[] answer_buttons = new Button[4];
	public TMP_Text[] answer_text = new TMP_Text[4];

	List<string[]> questions_text = new List<string[]>();

	private int position = 2;
	private int questionsAsked = 0;
	public static int totalQuestionsToAsk;
	bool question_answered;

	public static int correctAnswers = 0;
	public CardDisplay cardDisplay;

	void Start()
	{
		//SceneManager.sceneLoaded += OnGameSceneLoaded;
		Read_CSV();
		ShuffleQuestions();
		SetTotalQuestionsToAsk();
		AskQuestion();
	}

	void Awake()
	{
		cardDisplay = FindAnyObjectByType<CardDisplay>();
	}

	void Read_CSV()
	{
		using (StreamReader reader = new StreamReader("Assets/IBM_AI_Questions.csv"))
		{
			string string_data;
			while ((string_data = reader.ReadLine()) != null)
			{
				if (string.IsNullOrWhiteSpace(string_data))
					continue;

				var data_values = string_data.Split(',');
				if (data_values.Length >= 7)
				{
					questions_text.Add(data_values);
				}
				else
				{
					Debug.LogWarning("Row does not have the expected number of columns: " + string_data);
				}
			}
		}
	}

	void ShuffleQuestions()
	{
		for (int i = 0; i < questions_text.Count; i++)
		{
			int randomIndex = Random.Range(i, questions_text.Count);
			(questions_text[i], questions_text[randomIndex]) = (questions_text[randomIndex], questions_text[i]);
		}
	}

	void SetTotalQuestionsToAsk()
	{
		if (cardDisplay != null && cardDisplay.cardData.cardType.Contains(Card.CardType.question))
		{
			totalQuestionsToAsk = cardDisplay.cardData.effect;
		}
		else
		{
			totalQuestionsToAsk = 2;
		}
	}

	void AskQuestion()
	{
		Debug.Log($"questionsAsked: {questionsAsked}, totalQuestionsToAsk: {totalQuestionsToAsk}, questions_text.Count: {questions_text.Count}");

		if (questionsAsked >= totalQuestionsToAsk || questionsAsked >= questions_text.Count)
		{
			Debug.Log("All questions asked. Delaying scene transition.");
			StartCoroutine(DelayedResumeGame());
			return;
		}

		position = questionsAsked;

		if (IsValidQuestion(position))
		{
			DisplayQuestion(position);
			questionsAsked++;
			question_answered = false;
		}
		else
		{
			Debug.LogWarning("Invalid question position or insufficient columns: " + position);
			questionsAsked++;
			AskQuestion();
		}
	}

	bool IsValidQuestion(int position)
	{
		return position < questions_text.Count && questions_text[position].Length >= 7 &&
			   !string.IsNullOrWhiteSpace(questions_text[position][1]);
	}

	void DisplayQuestion(int position)
	{
		questions[1].text = questions_text[position][1];

		for (int i = 0; i < 4; i++)
		{
			answer_buttons[i].GetComponent<Image>().color = Color.white;
			answer_text[i] = answer_buttons[i].GetComponentInChildren<TMP_Text>();
			answer_text[i].text = questions_text[position][i + 2];
			SetAnswerButtonListener(position, i);
		}
	}

	void SetAnswerButtonListener(int position, int index)
	{
		answer_buttons[index].onClick.RemoveAllListeners();
		answer_buttons[index].onClick.AddListener(() => HandleAnswer(index, questions_text[position][index + 2] == questions_text[position][6]));
	}

	void HandleAnswer(int index, bool isCorrect)
	{
		Debug.Log($"HandleAnswer called: index={index}, isCorrect={isCorrect}, question_answered={question_answered}");

		if (question_answered) return; // Prevent multiple clicks

		question_answered = true;

		if (isCorrect)
		{
			answer_buttons[index].GetComponent<Image>().color = Color.green;
			correctAnswers++;
		}
		else
		{
			answer_buttons[index].GetComponent<Image>().color = Color.red;
		}

		foreach (var button in answer_buttons)
		{
			button.onClick.RemoveAllListeners();
		}

		Debug.Log("Starting coroutine WaitAndProceed()");
		StartCoroutine(WaitAndProceed());
	}

	IEnumerator WaitAndProceed()
	{
		Debug.Log("Waiting before proceeding to the next question...");
		yield return new WaitForSecondsRealtime(1.0f);
		Debug.Log("Proceeding to the next question...");
		AskQuestion();
	}

	IEnumerator DelayedResumeGame()
	{
		yield return new WaitForSecondsRealtime(1.0f);
		ResumeGame();
	}
	void ResumeGame()
	{
		Debug.Log("Loading GameScene...");
		Time.timeScale = 1;
		SceneManager.UnloadSceneAsync("Question_Display");
	}
}

