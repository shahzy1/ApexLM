namespace T.ApexLM.Shared
{
    public static class AppConstants
    {
        public static class Validation
        {
            public const string PropertyGeneralTitle = "General";
            public const string ValidationErrorTitle = "Validation Errors";
            public const string ValidationModelTitle = "Mode Validation Error";
        }

        public static class Business
        {
            public const string UnsavedDataWarning = "You have unsaved data, do you want to continue?";
            public const string NoteBookAddedSuccessfully = "NoteBook has been added successfully! Charge code number is";
            public const string NoteBookUpdatedSuccessfully = "NoteBook has been updated successfully! Charge code number is";
        }

        public static class Auth
        {
            public const string ClaimTypes_Preferred_UserName = "preferred_username";
            public const string ServerSideHttpClientName = "ServerApi";
            public const string ApexLmAppGroupPolicy = "ApexLmAppGroupPolicy";
            public const string SyncingUserWithDbMessage = "Validating user against web api!!!";
            public const string UserEmailNotFoundInJwtClaims = "User email not found in JWT claims";
            public const string DeniedPermissionEntraMessage = "You don't have access to this application, please contact Azure Entra Administrator!";
            public const string DeniedPermissionUCTAppMessage = "You don't have access to this application, please contact ApexLM Application Administrator!";
        }

        public static class ApiRoute
        {
            public const string ApexLmViewList = "ApexLmViewList";
            public const string ValidateUser = "ValidateUser";
            public const string UserList = "UserList";
            public const string ApexLmRecordById = "ApexLmRecordById";
        }
    }
}
