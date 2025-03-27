//using System.Collections.Generic;
//using System.IO;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class Questions : MonoBehaviour
//{
//	public TMP_Text[] questions = new TMP_Text[2];
//	public Button[] answer_buttons = new Button[4];
//	public TMP_Text[] answer_text = new TMP_Text[4];

//	List<string[]> questions_text = new List<string[]>();

//	private int position = 2;
//	private int questionsAsked = 0;
//	public static int totalQuestionsToAsk;
//	bool question_answered = false;

//	// store number of correct answers by player
//	public static int correctAnswers = 0;

//	public CardDisplay cardDisplay;

//	// Start is called once before the first execution of Update after the MonoBehaviour is created
//	void Start()
//	{
//		Read_CSV();
//		ShuffleQuestions();
//		SetTotalQuestionsToAsk();
//		AskQuestion();
//	}

//	void Awake()
//	{
//		cardDisplay = FindAnyObjectByType<CardDisplay>();
//	}

//	void Update()
//	{
//		if (question_answered && Input.GetKeyDown(KeyCode.Mouse0))
//		{
//			//HandleAnswer(-1, false); // Call HandleAnswer with dummy parameters
//		}
//	}
//	void Read_CSV()
//	{
//		using (StreamReader reader = new StreamReader("Assets/IBM_AI_Questions.csv"))
//		{
//			string string_data;
//			while ((string_data = reader.ReadLine()) != null)
//			{
//				var data_values = string_data.Split(',');
//				if (data_values.Length >= 7)
//				{
//					questions_text.Add(data_values);
//				}
//				else
//				{
//					Debug.LogWarning("Row does not have the expected number of columns: " + string_data);
//				}
//			}
//		}
//	}

//	void ShuffleQuestions()
//	{
//		for (int i = 0; i < questions_text.Count; i++)
//		{
//			string[] temp = questions_text[i];
//			int randomIndex = Random.Range(i, questions_text.Count);
//			questions_text[i] = questions_text[randomIndex];
//			questions_text[randomIndex] = temp;
//		}
//	}

//	void SetTotalQuestionsToAsk()
//	{
//		if (cardDisplay != null)
//		{
//			totalQuestionsToAsk = cardDisplay.cardData.manaCost;
//		}
//		else
//		{
//			totalQuestionsToAsk = 2;
//		}
//	}
//	void AskQuestion()
//	{
//		while (questionsAsked < questions_text.Count)
//		{
//			position = questionsAsked;

//			if (IsValidQuestion(position))
//			{
//				DisplayQuestion(position);
//				questionsAsked++;
//				question_answered = false;
//				return;
//			}
//			else
//			{
//				Debug.LogWarning("Invalid question position or insufficient columns: " + position);
//				questionsAsked++;
//			}
//		}

//		ResumeGame();
//	}

//	bool IsValidQuestion(int position)
//	{
//		return position < questions_text.Count && questions_text[position].Length >= 7 &&
//!string.IsNullOrWhiteSpace(questions_text[position][1]) &&
//!string.IsNullOrWhiteSpace(questions_text[position][2]) &&
//!string.IsNullOrWhiteSpace(questions_text[position][3]) &&
//!string.IsNullOrWhiteSpace(questions_text[position][4]) &&
//!string.IsNullOrWhiteSpace(questions_text[position][5]) &&
//!string.IsNullOrWhiteSpace(questions_text[position][6]);
//	}

//	void DisplayQuestion(int position)
//	{
//		questions[1].text = questions_text[position][1];

//		for (int i = 0; i < 4; i++)
//		{
//			// Reset button colors
//			answer_buttons[i].GetComponent<Image>().color = Color.white;

//			answer_text[i] = answer_buttons[i].GetComponentInChildren<TMP_Text>();
//			answer_text[i].text = questions_text[position][i + 2];

//			SetAnswerButtonListener(position, i);
//		}
//	}

//	void SetAnswerButtonListener(int position, int index)
//	{
//		answer_buttons[index].onClick.RemoveAllListeners();

//		if (questions_text[position][index + 2] == questions_text[position][6])
//		{
//			answer_buttons[index].onClick.AddListener(() => HandleAnswer(index, true));
//		}
//		else
//		{
//			answer_buttons[index].onClick.AddListener(() => HandleAnswer(index, false));
//		}
//	}

//	void HandleAnswer(int index, bool isCorrect)
//	{
//		if (isCorrect)
//		{
//			Debug.Log("Correct");
//			if (index >= 0)
//			{
//				answer_buttons[index].GetComponent<Image>().color = Color.green;
//			}
//			correctAnswers++;
//		}
//		else
//		{
//			Debug.Log("Wrong");
//			if (index >= 0)
//			{
//				answer_buttons[index].GetComponent<Image>().color = Color.red;
//			}
//		}

//		for (int i = 0; i < 4; i++)
//		{
//			answer_buttons[i].onClick.RemoveAllListeners();
//		}
//		question_answered = true;

//		// Trigger the next question or resume the game
//		if (questionsAsked >= totalQuestionsToAsk)
//		{
//			ResumeGame();
//		}
//		else
//		{
//			AskQuestion();
//		}
//	}

//	void ResumeGame()
//	{
//		// Unload the question scene
//		SceneManager.UnloadSceneAsync("Question_Display");

//		// Load the game scene and update only player mana
//		SceneManager.LoadScene("GameScene");

//		// if gamescene is loaded, update player mana
//		if (SceneManager.GetActiveScene().name == "GameScene")
//		{
//			ManaUI manaUI = FindAnyObjectByType<ManaUI>();
//			if (manaUI != null)
//			{
//				manaUI.UpdateManaUI(correctAnswers);
//			}
//		}

//		// Resume the game
//		Time.timeScale = 1;
//	}
//}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.IO;

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
			string[] temp = questions_text[i];
			int randomIndex = Random.Range(i, questions_text.Count);
			questions_text[i] = questions_text[randomIndex];
			questions_text[randomIndex] = temp;
		}
	}

	void SetTotalQuestionsToAsk()
	{
		totalQuestionsToAsk = cardDisplay != null ? cardDisplay.cardData.manaCost : 2;
	}

	void AskQuestion()
	{
		while (questionsAsked < questions_text.Count)
		{
			position = questionsAsked;

			if (IsValidQuestion(position))
			{
				DisplayQuestion(position);
				questionsAsked++;
				question_answered = false;
				return;
			}
			else
			{
				Debug.LogWarning("Invalid question position or insufficient columns: " + position);
				questionsAsked++;
			}
		}

		ResumeGame();
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

		question_answered = true;

		if (questionsAsked >= totalQuestionsToAsk)
		{
			ResumeGame();
		}
		else
		{
			AskQuestion();
		}
	}

	void ResumeGame()
	{
		SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
		SceneManager.sceneLoaded += OnGameSceneLoaded;
	}

	void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "GameScene")
		{
			ManaUI manaUI = FindAnyObjectByType<ManaUI>();
			if (manaUI != null)
			{
				manaUI.UpdateManaUI(correctAnswers);
			}
		}
		SceneManager.sceneLoaded -= OnGameSceneLoaded;
	}
}


