using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [SerializeField] TextAsset file;
    [SerializeField] public TMP_Text questionText;
    [SerializeField] public Button[] optionButtons;
    [SerializeField] public TMP_Text scoreBoard;
    public GameObject quizCanvasInfo;

    
    private QuizDataContainer quizDataContainer;
    private Question[] quizData;
    private int currentQuestionIndex = 0;
    private Question currentQuestion;
    private int correctAnswers = 0;

    private void Start()
    {
        LoadQuizData();
        DisplayQuestion();
    }

    private void LoadQuizData()
    {
        if (file != null)
        {
            string json = file.text;
            ParseQuizData(json);
        }
        else
        {
            Debug.LogError("No file assigned!");
        }
    }

    private void ParseQuizData(string quizDataText)
    {
        // Parse the JSON string into an array of Question objects
        quizDataContainer = JsonUtility.FromJson<QuizDataContainer>(quizDataText);
        quizData = quizDataContainer.quizData;
    }

    private void DisplayQuestion()
    {
        currentQuestion = quizData[currentQuestionIndex];
        questionText.text = currentQuestion.question;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].gameObject.SetActive(true);
            optionButtons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.options[i];
        }
    }

    public void CheckAnswer(Button clickedButton)
    {
        // Find the index of the clicked button in the optionButtons array
        int optionIndex = System.Array.IndexOf(optionButtons, clickedButton);

        // Get the text of the clicked button
        string selectedOptionText = clickedButton.GetComponentInChildren<TMP_Text>().text;

        if (selectedOptionText == currentQuestion.answer)
        {
            Debug.Log("Correct answer!");
            // Update button color to green
            Color greenColor = Color.green;
            ColorBlock colorBlock = clickedButton.colors;
            colorBlock.pressedColor = greenColor;
            clickedButton.colors = colorBlock;

            correctAnswers++;
            UpdateScoreboard();
        }
        else
        {
            Debug.Log("Incorrect answer!");
            Color redColor = Color.red;
            ColorBlock colorBlock = clickedButton.colors;
            colorBlock.pressedColor = redColor;
            clickedButton.colors = colorBlock;

            UpdateScoreboard();
        }

        currentQuestionIndex++;
        if (currentQuestionIndex < quizData.Length)
        {
            DisplayQuestion();
        }
        else
        {
            Debug.Log("Quiz completed!");
            GameObject quizCanvas = GameObject.Find("QuizCanvas");
            quizCanvas.SetActive(false);
            quizCanvasInfo.SetActive(true);
            ResetQuiz();
            // Handle quiz completion logic here
        }
    }

    public void UpdateScoreboard()
    {
        scoreBoard.text = string.Format("Total Score: {0}", correctAnswers);
    }

    public void ResetQuiz()
    {
        TMP_Text quizInfoText = quizCanvasInfo.transform.Find("QuizInfoText").GetComponent<TMP_Text>();

        if (quizInfoText != null)
        {
            quizInfoText.text = string.Format("Total Score: {0}", correctAnswers);
        }

        currentQuestionIndex = 0;
        correctAnswers = 0;
        DisplayQuestion();
        UpdateScoreboard();
    }

}

[System.Serializable]
public class QuizDataContainer
{
    public Question[] quizData;
}
