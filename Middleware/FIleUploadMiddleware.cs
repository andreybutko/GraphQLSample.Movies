using GraphQL.Server.Transports.AspNetCore.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace GraphQLSample.Movies.Middleware
{
  public class FileUploadMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly string FileVariable = "file";

    public FileUploadMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
      if (ctx.Request.ContentType != null && ctx.Request.ContentType != "application/json")
      {
        var file = ctx.Request.Form.Files[0];
        var newPath = CreateDirectory();
        if (file.Length > 0)
        {
          var fullPath = SaveFile(file, newPath);
          var body = ReplaceVariable(ctx, FileVariable, fullPath);

          var requestData = Encoding.UTF8.GetBytes(body);
          ctx.Request.Body = new MemoryStream(requestData);
          ctx.Request.ContentType = "application/json";
        }
      }
      await _next(ctx);
    }

    private string ReplaceVariable(HttpContext ctx, string variableName, string variableValue)
    {
      var encoder = JavaScriptEncoder.Create();
      var query = JsonConvert.DeserializeObject<GraphQLRequest>(ctx.Request.Form["operations"]);
      query.Variables.Remove(variableName);
      query.Variables.Add(variableName, variableValue);
      return JsonConvert.SerializeObject(query);
    }

    private string SaveFile(IFormFile file, string newPath)
    {
      var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
      var fullPath = Path.Combine(newPath, fileName);
      using (var stream = new FileStream(fullPath, FileMode.Create))
      {
        file.CopyTo(stream);
      }

      return fullPath;
    }

    private string CreateDirectory()
    {
      var newPath = Path.Combine(Environment.CurrentDirectory, "Upload");
      if (!Directory.Exists(newPath))
      {
        Directory.CreateDirectory(newPath);
      }
      return newPath;
    }
  }
}
