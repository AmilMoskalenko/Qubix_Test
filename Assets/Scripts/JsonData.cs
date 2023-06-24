using System;

[Serializable]
public class SendData
{
    public string type;
    public PayloadSend payload;
}

public class PayloadSend
{
    public int game_id;
    public int? enemyCount;
    public string direction;
}

public class GetData
{
    public string Type { get; set; }
    public PayloadGet Payload { get; set; }
}

public class PayloadGet
{
    public int Id { get; set; }
}
