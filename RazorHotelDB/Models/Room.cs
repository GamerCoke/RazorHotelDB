namespace RazorHotelDB.Models
{
    public class Room
    {
        public int VærelseNr { get; set; }
        public int HotelNr { get; set; }
        public string VærelseType { get; set; }
        public double Pris { get; set; }

        public Room() { }

        public Room(int værNr, int hotNr, string værTyp, double pris)
        {
            VærelseNr = værNr;
            HotelNr = hotNr;
            VærelseType = værTyp;
            Pris = pris;
        }

        public override string ToString()
        {
            return $"{nameof(HotelNr)}: {HotelNr}, {nameof(VærelseNr)}: {VærelseNr}, {nameof(VærelseType)}: {VærelseType}, {nameof(Pris)}: {Pris}";
        }
    }
}
