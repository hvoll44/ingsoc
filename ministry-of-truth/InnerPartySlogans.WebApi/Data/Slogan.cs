namespace InnerPartySlogans.WebApi.Data
{
    public class Slogan
    {
        public int SloganId { get; set; }
        public string Phrase { get; set; }
        public string Author { get; set; }
        public DateTime DateCoined { get; set; }
    }
}