using Volo.Abp;

namespace CaDaDora.Utils
{
    public static class CaDaDoraUtils
    {
        public static string IsValidString(string str, string parameterName)
        {

            if (string.IsNullOrWhiteSpace(str))
            {
                throw new BusinessException(CaDaDoraDomainErrorCodes.ParametroObbligatorio).WithData("parametro", parameterName);
            }
            return str;
        }

    }
}
