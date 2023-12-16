namespace OnlineTravelDiscussionForum.Dtos
{
    public class ApiResponce<T> where T : class
    {

        public bool Success { get; set; } = false;
        public string Status { get; set; } = "";
        public T? Data { get; set; } = null;

    }
}
