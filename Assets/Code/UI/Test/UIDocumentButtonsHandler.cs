using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIDocumentButtonsHandler : MonoBehaviour
{
    [HideInInspector] public UIDocument uiDocument;
    [SerializeField] private List<UnityEvent> buttonsEvents;
    private List<Button> buttons;

    private void OnEnable()
    {
        AssignMethodsToButonEvent();
    }

    public void SetButtonsEvents()
    {
        uiDocument = GetComponent<UIDocument>();
        buttons = uiDocument.rootVisualElement.Query<Button>().ToList();

        buttonsEvents.Clear();
        for (int i = 0; i < buttons.Count; i++)
        {
            UnityEvent buttoneEvent = new UnityEvent();
            buttonsEvents.Add(buttoneEvent);
        }
    }

    private void AssignMethodsToButonEvent()
    {
        buttons = uiDocument.rootVisualElement.Query<Button>().ToList();
        foreach (var buttoneEvent in buttonsEvents)
        {
            buttons[buttonsEvents.IndexOf(buttoneEvent)].clicked += () => buttoneEvent.Invoke();
        }
    }
}
