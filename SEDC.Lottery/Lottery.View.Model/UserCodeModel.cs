namespace Lottery.View.Model
{
    public class UserCodeModel
    {
        public UserCodeModel()
        {
            Code = new CodeModel();
        }

        public CodeModel Code { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }
    }
}
