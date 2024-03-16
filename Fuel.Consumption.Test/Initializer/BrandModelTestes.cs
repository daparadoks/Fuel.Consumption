using System;
using System.Collections.Generic;
using System.Linq;
using Fuel.Consumption.Domain;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Fuel.Consumption.Test.Initializer;

public class BrandModelTestes : IClassFixture<FuelAppTestStartup>
{
    private readonly IBrandReadService _brandReadService;
    private readonly IModelGroupReadService _modelGroupReadService;
    private readonly IModelReadService _modelReadService;
    
    private readonly IBrandWriteService _brandWriteService;
    private readonly IModelGroupWriteService _modelGroupWriteService;
    private readonly IModelWriteService _modelWriteService;

    public BrandModelTestes(FuelAppTestStartup fixture)
    {
        var serviceProvider = fixture.ServiceProvider;
        _brandReadService = serviceProvider.GetRequiredService<IBrandReadService>();
        _modelGroupReadService = serviceProvider.GetRequiredService<IModelGroupReadService>();
        _modelReadService = serviceProvider.GetRequiredService<IModelReadService>();
        
        _brandWriteService = serviceProvider.GetRequiredService<IBrandWriteService>();
        _modelGroupWriteService = serviceProvider.GetRequiredService<IModelGroupWriteService>();
        _modelWriteService = serviceProvider.GetRequiredService<IModelWriteService>();
    }

    [Fact]
    public async void Create_Brands()
    {
        var brandNames = new List<string> { "Fiat", "Hyundai", "Renault", "Honda", "Yamaha", "Bajaj", "Sym" };

        foreach (var brandName in brandNames)
        {
            var existsBrand = await _brandReadService.GetAnyByName(brandName);
            var brand = new Brand(brandName,
                existsBrand?.LogoUrl ?? "",
                true);

            if (existsBrand != null)
                brand.SetId(existsBrand.Id);
            
            if (existsBrand == null)
                await _brandWriteService.Add(brand);
            else
                await _brandWriteService.Update(brand);
        }


        var modelGroupAndBrands = new List<ModelGroupTestDto>
        {
            new("Fiat", "Egea"),
            new("Hyundai", "i20"),
            new("Renault", "Megane"),
            new("Honda", "Forza"),
            new("Yamaha", "X-Max"),
            new("Sym", "Joymax"),
            new("Sym", "Jet14X"),
            new("Bajaj", "Pulsar"),
        };

        foreach (var modelGroupAndBrand in modelGroupAndBrands)
        {
            var brand = await _brandReadService.GetByName(modelGroupAndBrand.Brand);
            if (brand == null)
                continue;

            var existsModelGroup = await _modelGroupReadService.GetAnyByName(modelGroupAndBrand.ModelGroup);
            var modelGroup = new ModelGroup(existsModelGroup?.Name ?? modelGroupAndBrand.ModelGroup,
                true,
                brand);

            if (existsModelGroup != null)
                modelGroup.SetId(existsModelGroup.Id);

            if (existsModelGroup == null)
                await _modelGroupWriteService.Add(modelGroup);
            else
                await _modelGroupWriteService.Update(modelGroup);
        }

        var modelsDto = new List<ModelTestDto>
        {
            new("1.3 Multijet Easy", "Egea", (int)FuelType.Diesel, 2016, null),
            new("1.4 Mpi Elite Plus", "i20", (int)FuelType.Gasoline, 2016, null),
            new("1.3 Tce Icon", "Megane", (int)FuelType.Gasoline, 2016, null),
            new("300 ABS", "Forza", (int)FuelType.Gasoline, 2016, null),
            new("400 ABS", "X-Max", (int)FuelType.Gasoline, 2013, 2019),
            new("250in", "Joymax", (int)FuelType.Gasoline, 2010, 2019),
            new("EU5", "Jet 14X", (int)FuelType.Gasoline, 2023, null),
            new("200 NS", "Pulsar", (int)FuelType.Gasoline, 2014, null),
        };

        foreach (var modelTestDto in modelsDto)
        {
            var modelGroup = await _modelGroupReadService.GetByName(modelTestDto.ModelGroupName);
            if (modelGroup == null)
                continue;

            var existsModel = await _modelReadService.GetAnyByName(modelTestDto.Name);
            var model = modelTestDto.ToEntity(modelGroup, existsModel);

            if (existsModel != null)
                model.SetId(existsModel.Id);
            
            if (existsModel == null)
                await _modelWriteService.Add(model);
            else
                await _modelWriteService.Update(model);
        }
    }
}

public class ModelTestDto
{
    public ModelTestDto(string name, string modelGroupName, int fuelType, int productionStart, int? productionEnd)
    {
        Name = name;
        ModelGroupName = modelGroupName;
        FuelType = fuelType;
        ProductionStart = productionStart;
        ProductionEnd = productionEnd;
    }

    public string Name { get; }
    public string ModelGroupName { get; }
    public int FuelType { get; }
    public int ProductionStart { get; }
    public int? ProductionEnd { get; }

    public Model ToEntity(ModelGroup modelGroup, Model? existsModel) => new(existsModel?.Name ?? Name,
        existsModel?.FuelType ?? FuelType,
        existsModel?.ProductionStart ?? ProductionStart,
        existsModel?.ProductionEnd ?? ProductionEnd,
        true,
        modelGroup);
}

public class ModelGroupTestDto
{
    public ModelGroupTestDto(string brand, string modelGroup)
    {
        Brand = brand;
        ModelGroup = modelGroup;
    }

    public string Brand { get; }
    public string ModelGroup { get; }
}