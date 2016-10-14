using System.ServiceModel;
using System.ServiceModel.Activation;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class LoginService
{
    [OperationContract]
    public string Login(string userName)
    {
        return "Welcome back, " + userName;
    }
}
