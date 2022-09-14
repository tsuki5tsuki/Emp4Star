//Reference
//https://github.com/jun112561/Discord-Stream-Bot-Backend/blob/master/Discord%20Stream%20Bot%20Backend/Utility.cs
using System.ComponentModel;

namespace Common
{
  public class APIResponse
  {
    public APIResponse(ResultStatusCode code, object data = null)
    {
      this.code = (int)code;
      this.message = code.GetEnumDescription();
      this.data = data;
    }

    public int code { get; set; }
    public string message { get; set; }
    public object data { get; set; }

    
  }

  public static class EnumExtensionMethods
  {
    public static string GetEnumDescription(this Enum enumValue)
    {
      var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

      var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

      return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
    }
  }
  public enum ResultStatusCode
  {
    [Description("Request succeeded. Resources have been fetched.")]
    OK = 200,
    [Description("Data has been created.")]
    Created = 201,
    [Description("No data is found.")]
    NoContent = 204,
    [Description("The request cannot be processed.")]
    BadRequest = 400,
    [Description("Unauthorized request.")]
    Unauthorized = 401,
    [Description("Too many requests have been made. Please try again later.")]
    TooManyRequests = 429,
    [Description("There is an unexpected error. Please contact administrator if you encounter this issue again.")]
    InternalServerError = 500
  }

}
