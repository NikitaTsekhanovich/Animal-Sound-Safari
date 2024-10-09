using AnimalsData;
using UnityEngine;

namespace MainSceneControllers
{
    public class StartUpController : MonoBehaviour
    {
        private void Awake()
        {
            AnimalDataContainer.LoadAnimalData();
        }
    }
}

