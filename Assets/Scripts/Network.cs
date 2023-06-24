using System.Text;
using NativeWebSocket;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviour
{
    [SerializeField] private Config _config;
    
    public static Network Instance;

    public int Id { get; private set; }

    private WebSocket _websocket;

    private async void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        _websocket = new WebSocket(_config.Url);
        _websocket.OnOpen += () => SendDataSetup(_config.CreateGame);
        _websocket.OnMessage += bytes =>
        {
            var json = Encoding.UTF8.GetString(bytes);
            var response = JsonConvert.DeserializeObject<GetData>(json);
            if (response?.Payload != null)
            {
                if (response.Type == _config.GameCreated)
                {
                    Id = response.Payload.Id;
                    DontDestroyOnLoad(this);
                    SceneManager.LoadScene("Game");
                }
                if (response.Type == _config.GameEnded)
                {
                    
                }
            }
        };
        await _websocket.Connect();
    }

    private void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _websocket.DispatchMessageQueue();
#endif
    }
    
    private async void SendWebSocketMessage(SendData sendData)
    {
        var json = JsonConvert.SerializeObject(sendData,
            new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
        var bytes = Encoding.UTF8.GetBytes(json);
        await _websocket.Send(bytes);
    }
    
    private void SendDataSetup(string type)
    {
        var sendData = new SendData {type = type};
        SendWebSocketMessage(sendData);
    }
    
    private void SendDataSetup(string type, int id)
    {
        var sendData = new SendData {type = type, payload = new PayloadSend
            {game_id = id, enemyCount = null, direction = null}};
        SendWebSocketMessage(sendData);
    }
    
    public void SendDataSetup(string type, int id, int enemyCount)
    {
        var sendData = new SendData {type = type, payload = new PayloadSend
            {game_id = id, enemyCount = enemyCount, direction = null}};
        SendWebSocketMessage(sendData);
    }
    
    public void SendDataSetup(string type, int id, string direction)
    {
        var sendData = new SendData {type = type, payload = new PayloadSend
            {game_id = id, enemyCount = null, direction = direction}};
        SendWebSocketMessage(sendData);
    }

    private async void OnApplicationQuit()
    {
        SendDataSetup(_config.EndGame, Id);
        await _websocket.Close();
    }
}
