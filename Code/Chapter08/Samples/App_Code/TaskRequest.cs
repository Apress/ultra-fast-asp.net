using System;

[Serializable]
public class TaskRequest
{
    public TaskRequest()
    {
    }

    public TaskTypeEnum TaskType { get; set; }
    public string EmailToAddress { get; set; }
    public string EmailSubject { get; set; }
    public string EmailMesssage { get; set; }
}

public enum TaskTypeEnum
{
    None,
    Email
}

