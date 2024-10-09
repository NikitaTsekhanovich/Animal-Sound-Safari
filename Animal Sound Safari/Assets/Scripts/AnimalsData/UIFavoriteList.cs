using System.Collections.Generic;
using UnityEngine;

namespace AnimalsData
{
    public class UIFavoriteList : UIList
    {
        [SerializeField] private GameObject _favoritePagesBlock;

        public override void LoadGrid()
        {
            _favoritePagesBlock.SetActive(true);

            ClearUIList();

            var favoriteAnimals = GetFavoriteAnimals();
            InstantiatePageAnimals(favoriteAnimals);
            InstantiateAnimalItems(favoriteAnimals);
            InstantiateDotItems();
            InitButtons();
        }

        private List<AnimalData> GetFavoriteAnimals()
        {
            var animalsData = new List<AnimalData>();

            for (var i = 0; i < AnimalDataContainer.AnimalsData.Count; i++)
            {
                var typeChosenItem = PlayerPrefs.GetInt($"{AnimalDataKeys.IsFavoriteAnimalKey}{i}");

                if (typeChosenItem == (int)TypeChosenItem.IsChosen)
                    animalsData.Add(AnimalDataContainer.AnimalsData[i]);
            }

            return animalsData;
        }

        private void ClearUIList()
        {
            while (_parentPages.childCount > 0) 
            {
                DestroyImmediate(_parentPages.GetChild(0).gameObject);
            }
            while (_parentDots.childCount > 0) 
            {
                DestroyImmediate(_parentDots.GetChild(0).gameObject);
            }
            _pagesAnimals.Clear();
            _dots.Clear();
        }
    }
}

