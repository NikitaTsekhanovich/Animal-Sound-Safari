using UnityEngine;

namespace AnimalsData
{
    public class UIAnimalsList : UIList
    {
        [SerializeField] private GameObject _animalsPagesBlock;

        public override void LoadGrid()
        {
            _animalsPagesBlock.SetActive(true);

            if (!_isLoaded)
            {
                InstantiatePageAnimals(AnimalDataContainer.AnimalsData);
                InstantiateAnimalItems(AnimalDataContainer.AnimalsData);
                InstantiateDotItems();
                InitButtons();

                _isLoaded = true;
            }
        }
    }
}

