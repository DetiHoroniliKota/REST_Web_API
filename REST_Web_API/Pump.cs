namespace REST_Web_API
{
    public class Pump
    {
        public int Id { get; set; }//артикул
        public string Name { get; set; } = ""; //название
        public int H { get; set; } // напор
        public int Q { get; set; } // расход
    }
}
