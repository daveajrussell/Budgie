using System;
using System.Runtime.Serialization;
using Budgie.Core.Enums;

[DataContract]
public class ApiCategory
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public bool Recurring { get; set; }

    [DataMember]
    public DateTime? RecurringDate { get; set; }

    [DataMember]
    public decimal? RecurringValue { get; set; }

    [DataMember]
    public string Colour { get; set; }

    [DataMember]
    public CategoryType Type { get; set; }
}