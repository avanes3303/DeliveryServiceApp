namespace DeliveryService.Results;

public abstract record OperationResult
{
    private OperationResult() { }

    public record Success : OperationResult;
    
    public record InvalidData : OperationResult;
    
    public record NotFound : OperationResult;
    
    public record WritingFailure(string FileName) : OperationResult;
    
    
    
    
}