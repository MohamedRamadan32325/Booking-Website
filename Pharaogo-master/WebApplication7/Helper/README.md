# AutoMapper Implementation Guide

This folder contains classes for AutoMapper implementation in the PharaoGo booking website project. This guide explains how to use AutoMapper with view models.

## Files Overview

1. **MappingProfile.cs**: Contains all entity-to-viewmodel mapping configurations
2. **MapperHelper.cs**: Helper class with common mapping methods for easy use in controllers and repositories

## How to Use AutoMapper in Your Code

### 1. Direct Injection in Controllers or Repositories

```csharp
public class YourController : Controller
{
    private readonly IMapper _mapper;

    public YourController(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IActionResult YourAction()
    {
        // Example: Map entity to view model
        var entity = GetEntityFromSomewhere();
        var viewModel = _mapper.Map<YourViewModel>(entity);
        
        return View(viewModel);
    }
}
```

### 2. Using the MapperHelper Class

First, register the MapperHelper in Program.cs if it's not already registered:

```csharp
builder.Services.AddScoped<MapperHelper>();
```

Then use it in your controllers or repositories:

```csharp
public class YourController : Controller
{
    private readonly MapperHelper _mapper;

    public YourController(MapperHelper mapper)
    {
        _mapper = mapper;
    }

    public IActionResult YourAction()
    {
        // Example: Map entity to view model using helper methods
        var entity = GetEntityFromSomewhere();
        var viewModel = _mapper.MapToYourViewModel(entity);
        
        // Or use the generic method
        var viewModel = _mapper.Map<Entity, ViewModel>(entity);
        
        return View(viewModel);
    }
}
```

## Adding New Mappings

To add a new mapping between an entity and a view model:

1. Open `MappingProfile.cs`
2. Add your mapping in the constructor:

```csharp
// Simple mapping
CreateMap<YourEntity, YourViewModel>();
CreateMap<YourViewModel, YourEntity>();

// For complex mappings, use ForMember
CreateMap<YourEntity, YourViewModel>()
    .ForMember(dest => dest.ViewModelProperty, opt => opt.MapFrom(src => src.EntityProperty))
    .ForMember(dest => dest.AnotherProperty, opt => opt.Ignore());
```

3. If you're using MapperHelper, add new helper methods in the appropriate region.

## Examples

### Mapping Entity to ViewModel

```csharp
// Direct mapping
var place = _placeRepository.GetById(id);
var placeViewModel = _mapper.Map<PlaceViewModel>(place);

// Or using helper
var placeViewModel = _mapperHelper.MapToPlaceViewModel(place);
```

### Mapping ViewModel to Entity

```csharp
// Direct mapping
var viewModel = GetViewModelFromForm();
var entity = _mapper.Map<Place>(viewModel);

// Or using helper
var entity = _mapperHelper.MapToPlace(viewModel);
```

### Updating an Entity from ViewModel

```csharp
// Direct mapping
var entity = _repository.GetById(id);
_mapper.Map(viewModel, entity);

// Or using helper
_mapperHelper.Map(viewModel, entity);
// Or
_mapperHelper.UpdateUserFromViewModel(viewModel, user);
```

### Mapping Collections

```csharp
// Direct mapping
var entities = _repository.GetAll();
var viewModels = _mapper.Map<List<YourViewModel>>(entities);

// Or using helper
var viewModels = _mapperHelper.MapList<Entity, ViewModel>(entities);
``` 