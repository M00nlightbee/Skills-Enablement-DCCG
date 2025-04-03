using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.IO;
using SkillBuildGame;
using System.Collections;
using UnityEngine.Networking;

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

	public int selectedRowStart = 1;
	public int selectedRowEnd = 37;
	public List<int> skipRowIndices = new List<int> { 1, 2, 8, 14, 20, 26, 32 };

	void Start()
	{
		StartCoroutine(Read_CSV(selectedRowStart, selectedRowEnd, skipRowIndices));
	}

	void Awake()
	{
		cardDisplay = FindAnyObjectByType<CardDisplay>();
	}

	IEnumerator Read_CSV(int startRow, int endRow, List<int> skipRowIndices)
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, "IBM_AI_Questions.csv");

		using (UnityWebRequest request = UnityWebRequest.Get(filePath))
		{
			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("CSV file not found or failed to load: " + filePath);
				yield break;
			}

			string[] lines = request.downloadHandler.text.Split('\n');
			int rowIndex = 0;

			foreach (string line in lines)
			{
				if (string.IsNullOrWhiteSpace(line) || skipRowIndices.Contains(rowIndex))
				{
					rowIndex++;
					continue;
				}

				if (rowIndex >= startRow && rowIndex <= endRow)
				{
					var data_values = line.Split(',');
					if (data_values.Length >= 7)
					{
						questions_text.Add(data_values);
					}
					else
					{
						Debug.LogWarning("Row does not have the expected number of columns: " + line);
					}
				}
				rowIndex++;
			}
		}

		ShuffleQuestions();
		AskQuestion();
	}

	void ShuffleQuestions()
	{
		for (int i = 0; i < questions_text.Count; i++)
		{
			int randomIndex = Random.Range(i, questions_text.Count);
			(questions_text[i], questions_text[randomIndex]) = (questions_text[randomIndex], questions_text[i]);
		}
	}

	void AskQuestion()
	{
		if (questionsAsked >= totalQuestionsToAsk || questionsAsked >= questions_text.Count)
		{
			Debug.Log("All questions asked. Resuming game.");
			StartCoroutine(DelayedResumeGame());
			return;
		}

		position = questionsAsked;

		if (IsValidQuestion(position))
		{
			Debug.Log("Displaying question at position: " + position);
			DisplayQuestion(position);
			questionsAsked++;
			question_answered = false;
		}
		else
		{
			Debug.LogWarning("Invalid question at position: " + position + ". Skipping...");
			questionsAsked++;
			AskQuestion();
		}
	}

	bool IsValidQuestion(int position)
	{
		if (position >= questions_text.Count || questions_text[position].Length < 7 || string.IsNullOrWhiteSpace(questions_text[position][1]))
		{
			return false;
		}

		for (int i = 2; i <= 5; i++)
		{
			if (string.IsNullOrWhiteSpace(questions_text[position][i]))
			{
				return false;
			}
		}

		return true;
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
		if (question_answered) return;

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

		StartCoroutine(WaitAndProceed());
	}

	IEnumerator WaitAndProceed()
	{
		yield return new WaitForSecondsRealtime(1.0f);
		AskQuestion();
	}

	IEnumerator DelayedResumeGame()
	{
		yield return new WaitForSecondsRealtime(1.0f);
		ResumeGame();
	}

	void ResumeGame()
	{
		Time.timeScale = 1;
		SceneManager.UnloadSceneAsync("Question_Display");
		GameManager.Instance.UpdatePlayerManaWithCorrectAnswers();
	}
}
