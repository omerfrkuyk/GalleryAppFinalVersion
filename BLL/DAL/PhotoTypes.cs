namespace BLL.DAL
{
    public class PhotoTypes

        //species on example
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Photo> Photos  { get; set; } = new List<Photo>();

    }
}