using System;

public class RecordNotFoundException : Exception
{
    private const string _message = "Record Not Found";
    public RecordNotFoundException() : base(_message)
    {

    }

    public RecordNotFoundException(string message)
        : base(message)
    {
    }

    public RecordNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}