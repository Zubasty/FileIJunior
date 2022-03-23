public static void CreateNewObject()
{
    //Создание объекта на карте
}

public static void InstallChance()
{
    _chance = Random.Range(0, 100);
}

public static int CalculateSalary(int hoursWorked)
{
    return _hourlyRate * hoursWorked;
}