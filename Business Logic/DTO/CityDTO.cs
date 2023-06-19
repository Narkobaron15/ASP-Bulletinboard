﻿namespace Business_Logic.DTO;

public class CityDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public string CountryName { get; init; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }
}
