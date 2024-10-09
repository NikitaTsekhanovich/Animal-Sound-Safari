using System;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsData
{
    public class AnimalItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        private int _index;

        public static Action<AnimalData> OnOpenInfoAnimal;

        public void InitItem(Sprite spriteIcon, int index)
        {
            _icon.sprite =  spriteIcon;
            _index = index;
        }

        public void OpenInfoAnimal()
        {
            OnOpenInfoAnimal?.Invoke(AnimalDataContainer.AnimalsData[_index]);
        }
    }
}

