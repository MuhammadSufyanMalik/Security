namespace Security
{
    public interface ITokenHelper
    {
        //  AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
        AccessToken CreateToken(string userId, string userName, string email, string fullName, string userTypeId, string refId, List<string> roles);
    }
}
