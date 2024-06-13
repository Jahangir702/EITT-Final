/*
 * Created by   : Jahangir
 * Date created : 18.04.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Utilities.Constants
{
    /// <summary>
    /// RouteConstants
    /// </summary>
    public static class RouteConstants
    {
        public const string BaseRoute = "eitt-api";

        #region Project
        public const string CreateProject = "project";

        public const string ReadProjects = "projects";

        public const string ReadProjectByKey = "project/key/{key}";

        public const string UpdateProject = "project/{key}";

        public const string DeleteProject = "project/{key}";
        #endregion

        #region Module
        public const string CreateModule = "module";

        public const string ReadModules = "modules";

        public const string ReadModuleByProjectId = "modules/project/{projectId}";

        public const string ReadModuleByKey = "module/key/{key}";

        public const string UpdateModule = "module/{key}";

        public const string DeleteModule = "module/{key}";
        #endregion

        #region IssueLog
        public const string CreateIssueLog = "issueLog";

        public const string ReadIssueLogs = "issueLogs";

        public const string ReadIssueLogByKey = "issueLog/key/{key}";

        public const string UpdateissueLog = "issueLog/{key}";

        public const string DeleteissueLog = "issueLog/{key}";
        #endregion

        #region SolveSheet
        public const string CreateSolveSheet = "solvesheet";

        public const string ReadSolveSheets = "solvesheets";

        public const string ReadSolveSheetByKey = "solvesheet/key/{key}";

        public const string UpdateSolveSheet = "solvesheet/{key}";

        public const string DeleteSolveSheet = "solvesheet/{key}";
        #endregion

        #region UserAccount
        public const string CreateUserAccount = "user-account";

        public const string ReadUserAccounts = "user-accounts";

        public const string ReadUserAccountByKey = "user-account/key/{key}";

        public const string ReadUserAccountByFirstname = "user-account/firstname/{firstName}";

        public const string UpdateUserAccount = "user-account/{key}";

        public const string DeleteUserAccount = "user-account/{key}";

        public const string UserLogin = "user-account/login";

        public const string CheckUserName = "user-account/user-check/{userName}";

        public const string GetUserAccessByUserName = "user-account/User-access-by-username/{userName}";

        public const string ReadUserAccessByUserName = "user-account/user-access-by-username/{userName}";

        public const string VerifyPassword = "user-account/verify-password";

        public const string UpdatePassword = "user-account/update-password";

        public const string RecoveryRequest = "user-account/recovery-request";

        #endregion
    }
}