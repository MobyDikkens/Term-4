namespace HouseRoads.Helpers.Input
{
    /// <summary>
    /// Contains 2 methods
    /// Get houses count
    /// Get road
    /// </summary>
    interface IInput
    {
        int GetHousesCount();
        System.Collections.Generic.List<HouseRoads.Entities.Road> GetRoads(int houseCount);
    }
}
