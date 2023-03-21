
using LinqToDB.Mapping;

namespace BlackLotus.CodeGenerator;

[Table("ej_gql_sample")]
public class Sample
{
    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("checkedOut")]
    public DateTime? CheckedOutOn { get; set; }

    [Association(ThisKey = nameof(CheckedOutById), OtherKey = nameof(UserReference.Id), CanBeNull = true)]
    public UserReference? CheckedOutBy { get; set; } = null!;

    [Column("checkoutUserID")]
    public int CheckedOutById { get; set; }

    [Column("storageLayerID")]
    public int CompartmentId { get; set; }

    [Column("created")]
    public DateTime CreatedOn { get; set; }

    [Association(ThisKey = nameof(CreatedById), OtherKey = nameof(UserReference.Id), CanBeNull = false)]
    public UserReference CreatedBy { get; set; } = null!;

    [Column("creatorID")]
    public int CreatedById { get; set; }

    [Column]
    public string Description { get; set; } = string.Empty;

    [Column("expiration")]
    public DateTime? ExpiresOn { get; set; }

    [Column("altID")]
    public string? ExternalBarcode { get; set; }

    [Column("sampleID")]
    public int Id { get; set; }

    [Column]
    public string Name { get; set; } = string.Empty;

    [Column("note")]
    public string Notes { get; set; } = string.Empty;

    [Association(ThisKey = nameof(OwnedById), OtherKey = nameof(UserReference.Id), CanBeNull = false)]
    public UserReference OwnedBy { get; set; } = null!;

    [Column("userID")]
    public int OwnedById { get; set; }

    [Column("parentSampleID")]
    public int ParentId { get; set; }

    [Column]
    public int Position { get; set; }

    [Column("catalogItemID")]
    public int ProductId { get; set; }

    [Column("sampleSeriesID")]
    public int SeriesId { get; set; }
    [Column("sampleTypeID")]
    public int TypeId { get; set; }
}