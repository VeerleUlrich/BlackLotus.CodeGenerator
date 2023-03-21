using LinqToDB.Mapping;

namespace BlackLotus.CodeGenerator;

[Table("ej_gql_equipment")]
[InheritanceMapping(Code = "INSTRUMENT", Type = typeof(EquipmentInstrument))]
[InheritanceMapping(Code = "STORAGE_DEVICE", Type = typeof(EquipmentStorageDevice))]
public abstract class Equipment
{
    [Column]
    public string Address { get; set; } = string.Empty;

    [Column]
    public string Building { get; set; } = string.Empty;

    [Association(ThisKey = nameof(CreatedById), OtherKey = nameof(UserReference.Id))]
    public UserReference CreatedBy { get; set; } = null!;

    [Column("userID")]
    public int CreatedById { get; set; }

    [Column]
    public string Department { get; set; } = string.Empty;

    [Column(IsDiscriminator = true)]
    public string Discriminator { get; set; } = string.Empty;

    [Column]
    public string Floor { get; set; } = string.Empty;

    [Column("storageID")]
    public int Id { get; set; }

    // [System.ComponentModel.DataAnnotations.Association(ThisKey = nameof(Id), OtherKey = nameof(EquipmentManager.EquipmentId))]
    // public List<EquipmentManager> Managers { get; set; } = new();
    //
    // [Column]
    // [NaturallySorted]
    // public string Name { get; set; } = string.Empty;
    
    [Column]
    public string Notes { get; set; } = string.Empty;
    
    [Column("hasValidation")]
    public bool RequireValidation { get; set; }
    
    [Column]
    public string Room { get; set; } = string.Empty;
    
    // [System.ComponentModel.DataAnnotations.Association(ThisKey = nameof(RootCompartmentId), OtherKey = nameof(EquipmentRootCompartment.Id), CanBeNull = false)]
    // public EquipmentRootCompartment RootCompartment { get; set; } = null!;
    //
    // [Column("storageLayerID")]
    // public int RootCompartmentId { get; set; }
    //
    // [System.ComponentModel.DataAnnotations.Association(ThisKey = nameof(Id), OtherKey = nameof(EquipmentSpecification.EquipmentId))]
    // public List<EquipmentSpecification> Specifications { get; set; } = new();
    //
    // [Column]
    // public int SubgroupId { get; set; }
    //
    // [System.ComponentModel.DataAnnotations.Association(ThisKey = nameof(TypeId), OtherKey = nameof(EquipmentTypeReference.Id), CanBeNull = false)]
    // public EquipmentTypeReference Type { get; set; } = null!;
    //
    // [Column("storageTypeID")]
    // public int TypeId { get; set; }
    //
    // [System.ComponentModel.DataAnnotations.Association(ThisKey = nameof(Id), OtherKey = nameof(EquipmentValidation.EquipmentId))]
    // public List<EquipmentValidation> Validations { get; set; } = new();
    
    [Column]
    public bool VisibleInInventoryBrowser { get; set; }
}

public class EquipmentInstrument : Equipment
{
}

public class EquipmentStorageDevice : Equipment
{
}


[Table("ej_profile")]
public class UserReference
{
    [Column]
    public string FirstName { get; set; } = string.Empty;

    [Column("userID")]
    public int Id { get; set; }

    [Column]
    public string LastName { get; set; } = string.Empty;
}

