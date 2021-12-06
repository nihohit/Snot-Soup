

public interface IIngredientModel {
    string Name { get; }
    float Size { get; }
    float Calories { get; }
    float Minerals { get; } 
    float Vitamins { get; } 
    float Saltiness { get; }
    float Bitterness { get; }
    float AcidicPhLevel { get; }
    float SourSweetLevel { get; }
    float FreedomForDefects { get; }
    float Viscosity { get; }
}