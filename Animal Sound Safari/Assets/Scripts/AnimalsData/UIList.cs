using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsData
{
    public abstract class UIList : MonoBehaviour
    {
        [SerializeField] protected Transform _parentPages;
        [SerializeField] protected Transform _parentDots;
        [SerializeField] private PageAnimalsData _pageItem;
        [SerializeField] private AnimalItem _animalItem;
        [SerializeField] private DotItem _dotItem;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;

        protected List<PageAnimalsData> _pagesAnimals = new();
        protected List<DotItem> _dots = new();
        private int _currentPageIndex;
        protected bool _isLoaded;

        public abstract void LoadGrid();

        protected void InstantiatePageAnimals(List<AnimalData> animalsData)
        {
            var firstPage = Instantiate(_pageItem, _parentPages);
            var countPages = Math.Ceiling((float)animalsData.Count / (float)firstPage.CounterSizePage);
            _pagesAnimals.Add(firstPage);

            for (var i = 1; i < countPages; i++)
            {
                var newPage = Instantiate(_pageItem, _parentPages);
                _pagesAnimals.Add(newPage);
                newPage.gameObject.SetActive(false);
            }
        }

        protected void InstantiateAnimalItems(List<AnimalData> animalsData)
        {
            var indexPage = 0;
            var currentPage = _pagesAnimals[indexPage];

            foreach (var animalData in animalsData)
            {
                if (currentPage.IsOverflowPage())
                {
                    indexPage++;
                    currentPage = _pagesAnimals[indexPage];
                    currentPage.IsOverflowPage();
                    var newLevelItem = Instantiate(_animalItem, currentPage.transform);
                    newLevelItem.InitItem(animalData.SpriteIcon, animalData.Index);
                }
                else
                {
                    var newLevelItem = Instantiate(_animalItem, currentPage.transform);
                    newLevelItem.InitItem(animalData.SpriteIcon, animalData.Index);
                }
            }
        }

        protected void InstantiateDotItems()
        {
            var firstDot = Instantiate(_dotItem, _parentDots);
            firstDot.ChooseDot(true);
            _dots.Add(firstDot);

            for (var i = 1; i < _pagesAnimals.Count; i++)
            {
                var newDot = Instantiate(_dotItem, _parentDots);
                newDot.ChooseDot(false);
                _dots.Add(newDot);
            }
        }

        protected void InitButtons()
        {
            _previousButton.gameObject.SetActive(false);

            if (_pagesAnimals.Count >= 1)
                _nextButton.gameObject.SetActive(true);
        }

        public void SwitchNextPage()
        {
            if (_currentPageIndex < _pagesAnimals.Count - 1)
            {
                _currentPageIndex++;
                SwitchPage(_currentPageIndex - 1, _currentPageIndex);

                _previousButton.gameObject.SetActive(true);

                if (_currentPageIndex >= _pagesAnimals.Count - 1)
                    _nextButton.gameObject.SetActive(false);
            }
        }

        public void SwitchPreviousPage()
        {
            if (_currentPageIndex > 0)
            {
                _currentPageIndex--;
                SwitchPage(_currentPageIndex + 1, _currentPageIndex);

                _nextButton.gameObject.SetActive(true);

                if (_currentPageIndex <= 0)
                    _previousButton.gameObject.SetActive(false);
            }              
        }  

        private void SwitchPage(int previousIndex, int currentIndex)
        {
            _pagesAnimals[previousIndex].gameObject.SetActive(false);
            _pagesAnimals[currentIndex].gameObject.SetActive(true);
            _dots[previousIndex].ChooseDot(false);
            _dots[currentIndex].ChooseDot(true);
        }
    }
}

