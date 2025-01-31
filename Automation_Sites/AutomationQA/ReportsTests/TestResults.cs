public class TestResult
{
    public string TestName { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public long ExecutionTime { get; set; }

    public TestResult(string testName, string status, string message = "", long executionTime = 0)
    {
        TestName = testName;
        Status = status;
        Message = message;
        ExecutionTime = executionTime;
    }
}