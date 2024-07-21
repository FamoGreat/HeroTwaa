namespace HeroTwaa.Models
{
    public class BoqItem
    {
        public int Id { get; set; } // Primary Key
        public int BoqId { get; set; } // Foreign Key
        public string Description { get; set; }
        public UnitOfMeasurement Unit { get; set; } // e.g., m3, kg, hour
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
        public string Category { get; set; } // e.g., Material, Tools, Labor
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public BillOfQuantities Boq { get; set; }
    }

    public enum UnitOfMeasurement
    {
        Feet,
        Inches,
        Meters,
        Centimeters,
        Millimeters,
        SquareFeet,
        SquareMeters,
        SquareYards,
        CubicFeet,
        CubicMeters,
        Gallons,
        Liters,
        CubicYards,
        Pounds,
        Kilograms,
        Tons,
        Pieces,
        Units,
        Bundles,
        Sheets,
        Hours,
        Minutes,
        Seconds,
        Joules
    }

}
