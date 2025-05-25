using System;
using UnityEngine;
using UnityEngine.UI;


public class ComboTimer : MonoBehaviour
    {
        public float comboTime = 5f;

        public Image comboTimeProgress;

        private float _timer;

        public void ResetCombo()
        {
            _timer = comboTime;
        }
        
        private void Update()
        {
            if (!(_timer > 0)) return;
            
            _timer -= Time.deltaTime;
            comboTimeProgress.fillAmount = _timer / comboTime;
        }

        public bool IsCombo()
        {
            return _timer > 0;
        }
    }
