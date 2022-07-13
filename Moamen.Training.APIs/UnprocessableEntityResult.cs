using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Moamen.Training.APIs
{
    public class UnprocessableEntityResult : ObjectResult
    {
        public UnprocessableEntityResult(ModelStateDictionary modelState)
            : base(new SerializableError(modelState))
        {
            StatusCode = 422;
        }
    }
}
