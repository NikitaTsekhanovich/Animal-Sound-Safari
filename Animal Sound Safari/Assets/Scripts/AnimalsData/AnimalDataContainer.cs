using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimalsData
{
    public class AnimalDataContainer
    {
        public static List<AnimalData> AnimalsData { get; private set; }

        public static void LoadAnimalData()
        {
            AnimalsData = Resources.LoadAll<AnimalData>("AnimalsData")
                .OrderBy(x => x.Index)
                .ToList();
        }
    }
}

