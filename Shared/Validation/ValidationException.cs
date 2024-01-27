using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Shared.Validation;

public class ValidationException : CustomException
{
    public ValidationException()

    {

    }
    public ValidationException(ValidationResultModel validationResultModel)
    {
        ValidationResultModel = validationResultModel;
        Data = validationResultModel.Errors;
    }
    public IEnumerable<ValidationError> Data { get; set; }
    public ValidationResultModel ValidationResultModel { get; }
}
