namespace Employee_CRUD.Utils
{
    public static class Resources
    {
        // Secret key
        public const string SecretKey = "onlykeytoaccesstoken";

        // API url
        public const string ResetPasswordUrl = "https://localhost:44355/api/Auth/ResetPasswordRequest?UserEmail=";
        public const string SignUpUrl = "https://localhost:44355/api/Auth/RegisterUser";
        public const string LoginUrl = "https://localhost:44355/api/Auth/Login";
    }
}
