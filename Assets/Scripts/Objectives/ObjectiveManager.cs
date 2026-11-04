// using System;
// using UnityEngine;

// public enum Objective
// {
//     ExploreTheLobby,
//     EnterTheTransformerSubstation1,
//     EnterTheTransformerSubstation2,
//     RestartTheMainPowerSource,
//     FindAWayToRestoreTheProtectionChip,
//     RestoreTheProtectionChip,
//     EnterTheMainBuilding,
//     RestoreTheDoorControlPanel,
// }

// public class ObjectiveData
// {
//     public Objective Objective_;
//     public string Title;
//     public string Description;

//     public ObjectiveData(Objective objective, string title, string description)
//     {
//         Objective_ = objective;
//         Title = title;
//         Description = description;
//     }
// }

// public class ObjectiveManager : MonoBehaviour
// {
//     public static event Action<ObjectiveData> OnObjectiveChanged;

//     // private ObjectiveData[] _objectives = new ObjectiveData[]
//     // {
//     //     new(Objective.ExploreTheLobby, "Исследуйте лобби", "Найдите способ включить электричество"),
//     //     new(Objective.EnterTheTransformerSubstation1, "Проникните в щитовую", "Найдите ключ от двери в щитовую основного питания"),
//     //     new(Objective.EnterTheTransformerSubstation2, "Проникните в щитовую", "Откройте дверь в трансформаторную будку"),
//     //     new(Objective.RestartTheMainPowerSource, "Перезагрузите основной источник питания", "Используйте рычаг в трансформаторной будку для перезагрузки основного источника питания"),
//     //     new(Objective.FindAWayToRestoreTheProtectionChip, "Найдите способ восстановить защитную микросхему", "Найдите элементы необходимые для восстановления защитной микросхемы"),
//     //     new(Objective.RestoreTheProtectionChip, "Восстановите защитную микросхему", "Восстановите логическую функцию защитной микросхемы из найденных элементов"),
//     //     new(Objective.EnterTheMainBuilding, "Восстановите логическую функцию защитной микросхемы", "Откройте дверь в главный корпус"),
//     //     new(Objective.RestoreTheDoorControlPanel, "Восстановите панель управления дверью", "Востановите логическую функцию панели управления дверью"),
//     // };

//     public ObjectiveData CurrentObjective { get; private set; }
//     public ObjectiveManager Instance;

//     private void Awake()
//     {
//         if (Instance == null) Instance = this;
//         Destroy(gameObject);


//     }

//     private void SetObjective(Objective objective)
//     {

//     }
// }


using System;
using UnityEngine;

public enum Objective
{
    ExploreTheLobby,
    EnterTheTransformerSubstation1,
    EnterTheTransformerSubstation2,
    RestartTheMainPowerSource,
    FindAWayToRestoreTheProtectionChip,
    RestoreTheProtectionChip,
    EnterTheMainBuilding,
    RestoreTheDoorControlPanel,
}

public class ObjectiveData
{
    public Objective Objective_;
    public string Title;
    public string Description;

    public ObjectiveData(Objective objective, string title, string description)
    {
        Objective_ = objective;
        Title = title;
        Description = description;
    }
}

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; }
    public event Action OnObjectiveChanged;

    private readonly ObjectiveData[] _objectives = new ObjectiveData[]
    {
        new(Objective.ExploreTheLobby, "Исследуйте лобби", "Найдите способ включить электричество"),
        new(Objective.EnterTheTransformerSubstation1, "Проникните в щитовую", "Найдите ключ от двери в щитовую"),
        new(Objective.EnterTheTransformerSubstation2, "Проникните в щитовую", "Откройте дверь в щитовую с помощью ключа"),
        new(Objective.RestartTheMainPowerSource, "Перезагрузите основной источник питания", "Используйте рычаг в щитовой для перезагрузки основного источника питания"),
        new(Objective.FindAWayToRestoreTheProtectionChip, "Найдите способ восстановить защитную микросхему", "Найдите элементы, необходимые для восстановления защитной микросхемы"),
        new(Objective.RestoreTheProtectionChip, "Восстановите защитную микросхему", "Восстановите логическую функцию защитной микросхемы из найденных элементов"),
        new(Objective.EnterTheMainBuilding, "Восстановите доступ", "Откройте дверь в главный корпус"),
        new(Objective.RestoreTheDoorControlPanel, "Восстановите панель управления дверью", "Восстановите логическую функцию панели управления дверью"),
    };

    public ObjectiveData CurrentObjective { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetObjective(Objective.ExploreTheLobby);
    }

    public void SetObjective(Objective objectiveEnum)
    {
        ObjectiveData foundData = Array.Find(_objectives, o => o.Objective_ == objectiveEnum);

        if (foundData != null)
        {
            CurrentObjective = foundData;
            OnObjectiveChanged?.Invoke();
        }
    }
}
