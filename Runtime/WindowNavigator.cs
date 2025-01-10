using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JDoddsNAIT.ThemedUI
{
    public class WindowNavigator : MonoBehaviour
    {
        private Window _current;

        [SerializeField] private Button _forwardButton, _backButton;
        [SerializeField] private Window _next, _previous;

        public Window NextWindow
        {
            get => _next; set
            {
                _next = value;
                UpdateButtons();
            }
        }
        public Window PreviousWindow
        {
            get => _previous; set
            {
                _previous = value;
                UpdateButtons();
            }
        }

        private void OnEnable()
        {
            _current = GetComponentInParent<Window>();

            if (_forwardButton != null)
            {
                _forwardButton.onClick.AddListener(Next);
            }

            if (_backButton != null)
            {
                _backButton.onClick.AddListener(Back);
            }
        }

        private void OnDisable()
        {
            if (_forwardButton != null)
            {
                _forwardButton.onClick.RemoveListener(Next);
            }

            if (_backButton != null)
            {
                _backButton.onClick.RemoveListener(Back);
            }
        }

        public void Next()
        {
            if (_current != null)
            {
                _current.Close();
            }
            _next.Open();
        }

        public void Back()
        {
            if (_current != null)
            {
                _current.Close();
            }
            _previous.Open();
        }

        private void UpdateButtons()
        {
            if (_forwardButton != null)
            {
                _forwardButton.interactable = _next != null;
            }

            if (_backButton != null)
            {
                _backButton.interactable = _previous != null;
            }
        }
    }
}
