using UnityEngine;
using UnityEngine.UI;

namespace CalculatorMod
{
    public class CalculatorApp : MonoBehaviour
    {
        private float _currentValue;
        private float _storedValue;
        private string _op = "";
        private bool _resetInput = true;
        private string _display = "0";
        private bool _isOpen = false;

        public void ToggleOpen()
        {
            _isOpen = !_isOpen;
            if (_isOpen)
            {
                _display = "0";
                _currentValue = 0;
                _storedValue = 0;
                _op = "";
                _resetInput = true;
            }
        }

        private void OnGUI()
        {
            if (!_isOpen) return;

            Rect area = new Rect(Screen.width / 2f - 160, Screen.height / 2f - 240, 320, 480);

            GUI.BeginGroup(area);
            GUI.Box(new Rect(0, 0, 320, 480), "");

            GUI.DrawTexture(new Rect(10, 10, 300, 50), Texture2D.whiteTexture);
            GUI.Label(new Rect(10, 10, 300, 50), _display,
                new GUIStyle(GUI.skin.label) { fontSize = 32, alignment = TextAnchor.MiddleRight });

            string[] rows = {
                "7", "8", "9", "/",
                "4", "5", "6", "*",
                "1", "2", "3", "-",
                "0", "C", "=", "+"
            };

            for (int i = 0; i < rows.Length; i++)
            {
                int col = i % 4;
                int row = i / 4;
                Rect btnRect = new Rect(10 + col * 77, 70 + row * 55, 70, 45);

                if (GUI.Button(btnRect, rows[i]))
                {
                    HandleInput(rows[i]);
                }
            }

            if (GUI.Button(new Rect(10, 300, 300, 30), "Cerrar (Tab)"))
            {
                _isOpen = false;
            }

            GUI.EndGroup();
        }

        private void HandleInput(string key)
        {
            switch (key)
            {
                case "C":
                    _currentValue = 0; _storedValue = 0; _op = "";
                    _resetInput = true; _display = "0";
                    break;
                case "+": case "-": case "*": case "/":
                    if (_op != "" && !_resetInput) Calculate();
                    _storedValue = _currentValue;
                    _op = key;
                    _resetInput = true;
                    break;
                case "=":
                    Calculate();
                    _op = "";
                    _resetInput = true;
                    break;
                default:
                    if (_resetInput) { _display = key; _resetInput = false; }
                    else { _display += key; }
                    float.TryParse(_display, out _currentValue);
                    break;
            }
        }

        private void Calculate()
        {
            switch (_op)
            {
                case "+": _currentValue = _storedValue + _currentValue; break;
                case "-": _currentValue = _storedValue - _currentValue; break;
                case "*": _currentValue = _storedValue * _currentValue; break;
                case "/": _currentValue = _currentValue != 0 ? _storedValue / _currentValue : 0; break;
            }
            _display = _currentValue.ToString("F4");
        }
    }
}   
