using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace JDoddsNAIT.ThemedUI
{
    public class Window : MonoBehaviour, IPointerClickHandler
    {
        enum OpenCloseBehaviour { SetActive, Animation, None, }

        private readonly Animator _animator;

        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private string _title;
        [Space]
        [SerializeField] private bool _isOpen;
        [SerializeField] private bool _isFocused;
        [Space]
        [SerializeField] private OpenCloseBehaviour _openBehaviour;
        [SerializeField] private string _openTrigger;
        [SerializeField] private OpenCloseBehaviour _closeBehaviour;
        [SerializeField] private string _closeTrigger;
        [Space]
        [SerializeField] private UnityEvents _events;

        public bool IsOpen
        {
            get => _isOpen; set
            {
                if (value)
                    Open();
                else
                    Close();
            }
        }
        public bool IsFocused
        {
            get => _isFocused; set
            {
                if (_isFocused != value)
                {
                    _isFocused = value;
                    OnFocus?.Invoke(value);
                }
            }
        }

        public string Title { get => _title; set => _title = value; }

        public event System.Action OnOpen, OnClose;
        public event System.Action<bool> OnFocus;


        private void OnValidate()
        {
            if (_titleText)
            {
                _titleText.text = Title;
            }
        }

        private void Awake()
        {
            OnOpen += () =>
            {
                _events.OnOpened.Invoke();
            };

            OnClose += () =>
            {
                _events.OnClosed.Invoke();
            };

            OnFocus += (f) =>
            {
                if (f)
                    FocusOn(this);

                _events.OnFocused.Invoke(f);
            };
        }

        private void Start()
        {
            if (IsOpen)
            {
                ForceOpen();
            }
            else
            {
                ForceClose();
            }
        }

        public void Open()
        {
            if (!_isOpen)
            {
                ForceOpen();
            }
        }
        /// <summary>
        /// Forcibly opens the window.
        /// </summary>
        /// <param name="sendCallback">Will invoke <see cref="OnOpen"/> if <see langword="true"/>.</param>
        internal void ForceOpen(bool sendCallback = true)
        {
            _isOpen = true;

            switch (_openBehaviour)
            {
                case OpenCloseBehaviour.SetActive:
                    gameObject.SetActive(true);
                    break;
                case OpenCloseBehaviour.Animation:
                    if (TryGetAnimator(_animator))
                        _animator.SetTrigger(_openTrigger);
                    break;
                default:
                    break;
            }

            if (sendCallback)
                OnOpen?.Invoke();

            IsFocused = true;
        }

        public void Close()
        {
            if (_isOpen)
            {
                ForceClose(true);
            }
        }
        /// <summary>
        /// Forcibly close the window.
        /// </summary>
        /// <param name="sendCallback">Will invoke <see cref="OnClose"/> if <see langword="true"/>.</param>
        internal void ForceClose(bool sendCallback = true)
        {
            IsFocused = false;

            if (sendCallback)
                OnClose?.Invoke();

            switch (_closeBehaviour)
            {
                case OpenCloseBehaviour.SetActive:
                    gameObject.SetActive(false);
                    break;
                case OpenCloseBehaviour.Animation:
                    if (TryGetAnimator(_animator))
                        _animator.SetTrigger(_closeTrigger);
                    break;
                default:
                    break;
            }

            _isOpen = false;
        }

        private static void FocusOn(Window window)
        {
            foreach (var win in FindObjectsByType<Window>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                if (win != window)
                    win.IsFocused = false;
                else
                {
                    window.transform.SetSiblingIndex(window.transform.parent.childCount + 1);
                }
            }
        }

        private bool TryGetAnimator(Animator animator)
        {
            if (animator == null)
                animator = GetComponent<Animator>();
            return animator != null;
        }

        public void OpenWindow(Window window)
        {
            OnClose += window.Close;
            window.OnClose += unsubscribe;
            window.Open();

            void unsubscribe()
            {
                OnClose -= window.Close;
                window.OnClose -= unsubscribe;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            IsFocused = true;
        }

        [System.Serializable]
        protected class UnityEvents
        {
            [field: SerializeField] public UnityEvent OnOpened { get; set; }
            [field: SerializeField] public UnityEvent OnClosed { get; set; }
            [field: SerializeField] public UnityEvent<bool> OnFocused { get; set; }
        }
    }
}