using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class SpeechRecognition : MonoBehaviour
{
    public string keyword;
    public string sceneName;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> actions = new Dictionary<string, System.Action>();

    void Start()
    {
        // Add keyword and corresponding action to the dictionary
        actions.Add(keyword, ChangeScence);

        // Initialize the keyword recognizer
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action action;
        if (actions.TryGetValue(args.text, out action))
        {
            action.Invoke();
        }
    }

    void ChangeScence()
    {
        SceneManager.LoadScene(sceneName);
    }
}
