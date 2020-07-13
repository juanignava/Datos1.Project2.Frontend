

namespace CookTime.Models
{
    /*
     * Almost every communication with the Apiserver class will need a Response wich
     * is based in if it was succesfull, a message ans the content of the answer (what I am asking for)
     */
    public class Response
    {
       public bool IsSuccess
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public object Result
        {
            get;
            set;
        }
    }
}
